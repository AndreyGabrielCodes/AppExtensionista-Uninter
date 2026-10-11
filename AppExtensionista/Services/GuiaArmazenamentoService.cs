using AppExtensionista.Data;
using AppExtensionista.Models;
using AppExtensionista.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppExtensionista.Services
{
    public interface IGuiaArmazenamentoService
    {
        Task<GuiaArmazenamentoDetalhesViewModel?> ObterPorAlimentoIdAsync(int idAlimento);
        Task SalvarOuAtualizarGuiaAsync(GuiaArmazenamentoFormViewModel model);
    }

    public class GuiaArmazenamentoService : IGuiaArmazenamentoService
    {
        private readonly AppDbContext _context;

        public GuiaArmazenamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GuiaArmazenamentoDetalhesViewModel?> ObterPorAlimentoIdAsync(int idAlimento)
        {
            var guia = await _context.GuiasArmazenamento
                .Include(g => g.Alimento)
                .FirstOrDefaultAsync(g => g.IdAlimento == idAlimento);

            if (guia == null) return null;

            return new GuiaArmazenamentoDetalhesViewModel
            {
                IdGuiaArmazenamento = guia.IdGuiaArmazenamento,
                IdAlimento = guia.IdAlimento,
                NomeAlimento = guia.Alimento.Nome,
                Titulo = guia.Titulo,
                Descricao = guia.Descricao,
                DataCriacao = guia.DataCriacao,
                DataUltimaAlteracao = guia.DataUltimaAlteracao
            };
        }

        public async Task SalvarOuAtualizarGuiaAsync(GuiaArmazenamentoFormViewModel model)
        {
            var guia = await _context.GuiasArmazenamento
                .FirstOrDefaultAsync(g => g.IdAlimento == model.IdAlimento);

            if (guia != null)
            {
                guia.Titulo = model.Titulo;
                guia.Descricao = model.Descricao;
                guia.DataUltimaAlteracao = DateTime.Now;
            }
            else
            {
                guia = new GuiaArmazenamentoModel
                {
                    IdAlimento = model.IdAlimento,
                    Titulo = model.Titulo,
                    Descricao = model.Descricao,
                    DataCriacao = DateTime.Now
                };
                _context.GuiasArmazenamento.Add(guia);
            }

            await _context.SaveChangesAsync();
        }
    }
}