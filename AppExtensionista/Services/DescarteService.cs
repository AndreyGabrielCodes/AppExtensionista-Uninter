using AppExtensionista.Data;
using AppExtensionista.Extensions;
using AppExtensionista.Models;
using AppExtensionista.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppExtensionista.Services
{
    public interface IDescarteService
    {
        Task RegistrarDescarteAsync(RegistrarDescarteModalViewModel model);
        Task<DescartadosIndexViewModel> ObterDescartadosAgrupadosAsync(string? termoPesquisa);
        Task<MotivoDescarteModalViewModel?> ObterMotivoDescarteAsync(int idRegistroDescarte);
    }

    public class DescarteService : IDescarteService
    {
        private readonly AppDbContext _context;
        private readonly ISettingsService _settingsService;

        public DescarteService(AppDbContext context, ISettingsService settingsService)
        {
            _context = context;
            _settingsService = settingsService;
        }

        public async Task RegistrarDescarteAsync(RegistrarDescarteModalViewModel model)
        {
            var estoque = await _context.Estoques.FindAsync(model.IdEstoque);
            if (estoque == null)
                throw new InvalidOperationException("Item do estoque não encontrado!");

            if (model.QuantidadeDescartar <= 0)
                throw new InvalidOperationException("A quantidade a descartar precisa ser maior que zero!");

            if (model.QuantidadeDescartar > estoque.Quantidade)
                throw new InvalidOperationException("A quantidade a descartar é maior do que a disponível em estoque!");

            if (model.TipoDescarte == Enums.DescarteEnum.Descarte && string.IsNullOrEmpty(model.DescricaoMotivo))
                throw new InvalidOperationException("Para descartar é necessário informar a descrição!");


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
            // Busca as configurações globais do dispositivo
            var settings = await _settingsService.GetSettingsAsync();
            int diasLimite = settings.DiasHistoricoDescartados;
            var dataCorte = DateTime.Now.Date.AddDays(-diasLimite);

            var query = _context.RegistrosDescarte
                .Include(d => d.Estoque)
                    .ThenInclude(e => e.Alimento)
                .Where(r => r.DataDescarte.Date >= dataCorte)
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