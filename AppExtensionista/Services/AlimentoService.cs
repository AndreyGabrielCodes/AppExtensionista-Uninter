using AppExtensionista.Data;

namespace AppExtensionista.Services
{
    public interface IAlimentoService
    {

    }

    public class AlimentoService : IAlimentoService
    {
        private readonly AppDbContext _context;

        public AlimentoService(AppDbContext context)
        {
            _context = context;
        }

    }
}
