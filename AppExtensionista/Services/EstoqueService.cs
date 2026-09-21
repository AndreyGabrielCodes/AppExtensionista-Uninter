using AppExtensionista.Data;
using AppExtensionista.Extensions;
using AppExtensionista.Models;
using AppExtensionista.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppExtensionista.Services
{
    public interface IEstoqueService
    {
        Task<EstoqueIndexViewModel> ObterEstoqueAgrupadoAsync(string? termoPesquisa);
        Task SalvarOuAtualizarItemEstoqueAsync(AlimentoCadastroViewModel model, int idUsuarioLogado);
        Task<AlimentoCadastroViewModel?> ObterParaEdicaoAsync(int idEstoque);
        Task<AlertaVencimentoPopUpViewModel> ObterAlertasVencimentoAsync(int idUsuarioLogado);
    }

    public class EstoqueService : IEstoqueService
    {
        private readonly AppDbContext _context;
        private readonly IAlimentoService _alimentoService;

        public EstoqueService(AppDbContext context, IAlimentoService alimentoService)
        {
            _context = context;
            _alimentoService = alimentoService;
        }

        public async Task<EstoqueIndexViewModel> ObterEstoqueAgrupadoAsync(string? termoPesquisa)
        {
            // Filtra estritamente apenas itens com quantidade maior que zero
            var query = _context.Estoques
                .Include(e => e.Alimento)
                .Where(e => e.Quantidade > 0);

            if (!string.IsNullOrWhiteSpace(termoPesquisa))
            {
                var termo = termoPesquisa.ToLower();
                query = query.Where(e => e.Alimento.Nome.ToLower().Contains(termo) ||
                                         e.MarcaAlimento.ToLower().Contains(termo));
            }

            var itens = await query.ToListAsync();

            // Agrupamento por Nome do Alimento
            var grupos = itens
                .GroupBy(e => new { e.IdAlimento, e.Alimento.Nome, e.Alimento.UnidadeMedida })
                .Select(g => new EstoqueAgrupadoViewModel
                {
                    IdAlimento = g.Key.IdAlimento,
                    NomeAlimento = g.Key.Nome,
                    UnidadeMedidaSigla = g.Key.UnidadeMedida.ObterSigla(),
                    QuantidadeTotalSomada = g.Sum(x => x.Quantidade),
                    Itens = g.Select(i => new EstoqueItemDetalheViewModel
                    {
                        IdEstoque = i.IdEstoque,
                        Marca = i.MarcaAlimento,
                        Quantidade = i.Quantidade,
                        DataValidade = i.DataValidade
                    }).OrderBy(i => i.DataValidade).ToList()
                }).ToList();

            // Totalizadores
            var totalCadastrados = await _context.Alimentos.CountAsync();
            var totalVencidos = await _context.Estoques.CountAsync(e => e.Quantidade > 0 && e.DataValidade.Date < DateTime.Today);
            var totalDescartados = await _context.RegistrosDescarte.CountAsync();

            return new EstoqueIndexViewModel
            {
                TotalAlimentosCadastrados = totalCadastrados,
                TotalVencidos = totalVencidos,
                TotalDescartados = totalDescartados,
                TermoPesquisa = termoPesquisa,
                Agrupamentos = grupos
            };
        }

        public async Task SalvarOuAtualizarItemEstoqueAsync(AlimentoCadastroViewModel model, int idUsuarioLogado)
        {
            // Obter/Criar o Alimento pai
            var alimento = await _alimentoService.ObterOuCriarAlimentoAsync(model.NomeAlimento, model.UnidadeMedida, idUsuarioLogado);

            if (model.IdEstoque.HasValue && model.IsEdicao)
            {
                // Edição de registro existente
                var estoqueExistente = await _context.Estoques.FindAsync(model.IdEstoque.Value);
                if (estoqueExistente != null)
                {
                    estoqueExistente.Quantidade = model.Quantidade;
                    estoqueExistente.MarcaAlimento = model.Marca;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                // Procura registro igual com quantidade > 0 para somar, ou reativa um zerado
                var duplicado = await _context.Estoques.FirstOrDefaultAsync(e =>
                    e.IdAlimento == alimento.IdAlimento &&
                    e.DataValidade.Date == model.DataValidade.Date &&
                    e.MarcaAlimento.ToLower() == model.Marca.ToLower());

                // Soma a quantidade ao item (caso estivesse zerado, volta a ficar ativo)
                if (duplicado != null)
                {
                    duplicado.Quantidade += model.Quantidade;
                    duplicado.QuantidadeOriginal += model.Quantidade;
                }
                else
                {
                    // Cria uma nova entrada no estoque
                    var novoEstoque = new EstoqueModel
                    {
                        IdUsuarioCriador = idUsuarioLogado,
                        IdAlimento = alimento.IdAlimento,
                        QuantidadeOriginal = model.Quantidade,
                        Quantidade = model.Quantidade,
                        DataValidade = model.DataValidade,
                        MarcaAlimento = model.Marca
                    };
                    _context.Estoques.Add(novoEstoque);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AlimentoCadastroViewModel?> ObterParaEdicaoAsync(int idEstoque)
        {
            var estoque = await _context.Estoques
                .Include(e => e.Alimento)
                .FirstOrDefaultAsync(e => e.IdEstoque == idEstoque && e.Quantidade > 0);

            if (estoque == null) 
                return null;

            return new AlimentoCadastroViewModel
            {
                IdEstoque = estoque.IdEstoque,
                IsEdicao = true,
                IdAlimentoSelecionado = estoque.IdAlimento,
                NomeAlimento = estoque.Alimento.Nome,
                UnidadeMedida = estoque.Alimento.UnidadeMedida,
                Quantidade = estoque.Quantidade,
                Marca = estoque.MarcaAlimento,
                DataValidade = estoque.DataValidade
            };
        }

        public async Task<AlertaVencimentoPopUpViewModel> ObterAlertasVencimentoAsync(int idUsuarioLogado)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuarioLogado);
            int diasAlerta = usuario?.DiasAlertaPadrao ?? 7;

            var dataLimite = DateTime.Today.AddDays(diasAlerta);

            var itensAproximando = await _context.Estoques
                .Include(e => e.Alimento)
                .Where(e => e.Quantidade > 0 && e.DataValidade.Date <= dataLimite.Date)
                .Select(e => new ItemAlertaVencimentoViewModel
                {
                    IdEstoque = e.IdEstoque,
                    NomeAlimento = e.Alimento.Nome,
                    Quantidade = e.Quantidade,
                    UnidadeMedidaSigla = e.Alimento.UnidadeMedida.ObterSigla(),
                    Marca = e.MarcaAlimento,
                    DataValidade = e.DataValidade
                })
                .OrderBy(e => e.DataValidade)
                .ToListAsync();

            return new AlertaVencimentoPopUpViewModel
            {
                DiasConfiguradosUsuario = diasAlerta,
                ItensAlerta = itensAproximando
            };
        }
    }
}