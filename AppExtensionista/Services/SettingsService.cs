using AppExtensionista.Data;
using AppExtensionista.Models;
using Microsoft.EntityFrameworkCore;

namespace AppExtensionista.Services
{
    public interface ISettingsService
    {
        Task<AppSettingsModel> GetSettingsAsync();
        Task SaveSettingsAsync(AppSettingsModel settings);
    }

    public class SettingsService : ISettingsService
    {
        private readonly AppDbContext _context;

        public SettingsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AppSettingsModel> GetSettingsAsync()
        {
            // Registro único global do dispositivo, é buscado o primeiro registro ou é criado um se não existir
            var settings = await _context.AppSettings.FirstOrDefaultAsync();

            if (settings == null)
            {
                settings = new AppSettingsModel
                {
                    Id = 1,
                    DiasAlertaVencimento = 7
                };

                _context.AppSettings.Add(settings);
                await _context.SaveChangesAsync();
            }

            return settings;
        }

        public async Task SaveSettingsAsync(AppSettingsModel settings)
        {
            _context.AppSettings.Update(settings);
            await _context.SaveChangesAsync();
        }
    }
}