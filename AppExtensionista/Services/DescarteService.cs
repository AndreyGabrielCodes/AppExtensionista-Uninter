using AppExtensionista.Data;

namespace AppExtensionista.Services
{
    public interface IDescarteService
    {

    }

    public class DescarteService : IDescarteService
    {
        private readonly AppDbContext _context;

        public DescarteService(AppDbContext context)
        {
            _context = context;
        }
    }
}
