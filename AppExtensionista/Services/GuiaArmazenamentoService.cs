using AppExtensionista.Data;

namespace AppExtensionista.Services
{
    public interface IGuiaArmazenamentoService
    {

    }

    public class GuiaArmazenamentoService : IGuiaArmazenamentoService
    {
        private readonly AppDbContext _context;

        public GuiaArmazenamentoService(AppDbContext context)
        {
            _context = context;
        }

    }
}
