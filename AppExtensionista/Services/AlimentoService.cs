using AppExtensionista.Data;
using AppExtensionista.Models;
using AppExtensionista.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppExtensionista.Services
{
    public interface IAlimentoService
    {
        Task<List<AlimentoOpcaoSelectViewModel>> ObterAlimentosExistentesOptionsAsync();
        Task<AlimentoModel> ObterOuCriarAlimentoAsync(string nome, Enums.UnidadeMedidaEnum unidadeMedida, int idUsuarioCriador);
        Task<AlimentoModel?> ObterPorIdAsync(int idAlimento);
    }

    public class AlimentoService : IAlimentoService
    {
        private readonly AppDbContext _context;

        public AlimentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlimentoOpcaoSelectViewModel>> ObterAlimentosExistentesOptionsAsync()
        {
            return await _context.Alimentos
                .Select(a => new AlimentoOpcaoSelectViewModel
                {
                    IdAlimento = a.IdAlimento,
                    Nome = a.Nome,
                    UnidadeMedida = a.UnidadeMedida
                })
                .ToListAsync();
        }

        public async Task<AlimentoModel> ObterOuCriarAlimentoAsync(string nome, Enums.UnidadeMedidaEnum unidadeMedida, int idUsuarioCriador)
        {
            var alimentoExistente = await _context.Alimentos
                .FirstOrDefaultAsync(a => a.Nome.ToLower() == nome.ToLower() && a.UnidadeMedida == unidadeMedida);

            if (alimentoExistente != null)
                return alimentoExistente;

            var novoAlimento = new AlimentoModel
            {
                Nome = nome,
                UnidadeMedida = unidadeMedida,
                IdUsuarioCriador = idUsuarioCriador
            };

            _context.Alimentos.Add(novoAlimento);
            await _context.SaveChangesAsync();
            return novoAlimento;
        }

        public async Task<AlimentoModel?> ObterPorIdAsync(int idAlimento)
        {
            return await _context.Alimentos.FindAsync(idAlimento);
        }
    }
}