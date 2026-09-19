using AppExtensionista.Data;

namespace AppExtensionista.Services
{
    public interface IEstoqueService
    {

    }

    public class EstoqueService : IEstoqueService
    {
        private readonly AppDbContext _context;

        public EstoqueService(AppDbContext context)
        {
            _context = context;
        }

    }
}
