using AppExtensionista.Data;
using AppExtensionista.Extensions;
using AppExtensionista.Models;
using AppExtensionista.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppExtensionista.Services
{
    public interface IDescarteService
    {
        Task RegistrarDescarteAsync(RegistrarDescarteModalViewModel model, int idUsuarioLogado);
        Task<DescartadosIndexViewModel> ObterDescartadosAgrupadosAsync(string? termoPesquisa);
        Task<MotivoDescarteModalViewModel?> ObterMotivoDescarteAsync(int idRegistroDescarte);
    }

    public class DescarteService : IDescarteService
    {
        private readonly AppDbContext _context;

        public DescarteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarDescarteAsync(RegistrarDescarteModalViewModel model, int idUsuarioLogado)
        {
            var estoque = await _context.Estoques.FindAsync(model.IdEstoque);
            if (estoque == null)
                throw new InvalidOperationException("Item do estoque não encontrado.");

            if (model.QuantidadeDescartar > estoque.Quantidade)
                throw new InvalidOperationException("A quantidade a descartar é maior do que a disponível em estoque.");

            // Reduz a quantidade no estoque
            if (model.QuantidadeDescartar == estoque.Quantidade)
            {
                estoque.Quantidade = 0; // Mantém o registro no BD para histórico, mas zerado
            }
            else
            {
                estoque.Quantidade -= model.QuantidadeDescartar;
            }

            // Cria o registro de descarte utilizando o Enum diretamente
            var descarte = new RegistroDescarteModel
            {
                IdUsuarioResponsavel = idUsuarioLogado,
                IdEstoque = estoque.IdEstoque,
                Quantidade = model.QuantidadeDescartar,
                DataDescarte = DateTime.Now,
                TipoDescarte = model.TipoDescarte, // Passa diretamente sem conversão
                DescricaoMotivo = model.DescricaoMotivo
            };

            _context.RegistrosDescarte.Add(descarte);
            await _context.SaveChangesAsync();
        }

        public async Task<DescartadosIndexViewModel> ObterDescartadosAgrupadosAsync(string? termoPesquisa)
        {
            var query = _context.RegistrosDescarte
                .Include(d => d.Estoque)
                    .ThenInclude(e => e.Alimento)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(termoPesquisa))
            {
                var termo = termoPesquisa.ToLower();
                query = query.Where(d => d.Estoque.Alimento.Nome.ToLower().Contains(termo) ||
                                         d.Estoque.MarcaAlimento.ToLower().Contains(termo));
            }

            var descartes = await query.ToListAsync();

            var agrupamentos = descartes
                .GroupBy(d => new { d.Estoque.IdAlimento, d.Estoque.Alimento.Nome, d.Estoque.Alimento.UnidadeMedida })
                .Select(g => new DescartadoAgrupadoViewModel
                {
                    NomeAlimento = g.Key.Nome,
                    UnidadeMedidaSigla = g.Key.UnidadeMedida.ObterSigla(),
                    QuantidadeTotalDescartada = g.Sum(x => x.Quantidade),
                    Itens = g.Select(i => new DescartadoItemDetalheViewModel
                    {
                        IdRegistroDescarte = i.IdListaDescarte,
                        DataDescarte = i.DataDescarte,
                        Quantidade = i.Quantidade,
                        Marca = i.Estoque.MarcaAlimento,
                        TipoDescarte = i.TipoDescarte.ObterNomeExibicao(),
                        DescricaoMotivo = i.DescricaoMotivo ?? string.Empty
                    }).OrderByDescending(i => i.DataDescarte).ToList()
                }).ToList();

            var totalDescartados = descartes.Count;

            return new DescartadosIndexViewModel
            {
                TotalDescartadosConsumidos = totalDescartados,
                TermoPesquisa = termoPesquisa,
                Agrupamentos = agrupamentos
            };
        }

        public async Task<MotivoDescarteModalViewModel?> ObterMotivoDescarteAsync(int idRegistroDescarte)
        {
            var descarte = await _context.RegistrosDescarte.FindAsync(idRegistroDescarte);
            if (descarte == null) return null;

            return new MotivoDescarteModalViewModel
            {
                TipoDescarte = descarte.TipoDescarte.ObterNomeExibicao(),
                DescricaoMotivo = descarte.DescricaoMotivo ?? string.Empty
            };
        }
    }
}