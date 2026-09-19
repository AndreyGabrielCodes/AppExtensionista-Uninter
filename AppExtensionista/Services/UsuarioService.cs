using AppExtensionista.Data;

namespace AppExtensionista.Services
{
    public interface IUsuarioService
    {

    }

    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

    }
}
