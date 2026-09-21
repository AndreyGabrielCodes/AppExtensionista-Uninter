using AppExtensionista.Data;
using AppExtensionista.Models;
using AppExtensionista.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppExtensionista.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioModel?> AutenticarAsync(string login, string senha);
        Task<UsuarioModel> CriarUsuarioAsync(UsuarioModalViewModel model);
        Task AtualizarDiasAlertaAsync(int idUsuario, int diasAlerta);
        Task<UsuarioModel?> ObterPorIdAsync(int idUsuario);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioModel?> AutenticarAsync(string login, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Login.ToLower() == login.ToLower() && u.Senha == senha);

            if (usuario == null)
                throw new InvalidOperationException("Usuário ou senha estão incorretos.");

            return usuario;
        }

        public async Task<UsuarioModel> CriarUsuarioAsync(UsuarioModalViewModel model)
        {
            var existe = await _context.Usuarios.AnyAsync(u => u.Login.ToLower() == model.Login.ToLower());

            if (existe)
                throw new InvalidOperationException("Login já cadastrado.");

            var usuario = new UsuarioModel
            {
                Nome = model.Nome ?? string.Empty,
                Login = model.Login,
                Senha = model.Senha,
                DiasAlertaPadrao = model.DiasAlertaVencimento ?? 7
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task AtualizarDiasAlertaAsync(int idUsuario, int diasAlerta)
        {
            var usuario = await ObterPorIdAsync(idUsuario);

            if (usuario != null)
            {
                usuario.DiasAlertaPadrao = diasAlerta;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UsuarioModel?> ObterPorIdAsync(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario == null)
                throw new InvalidOperationException("Não foi possível encontrar o usuário.");

            return usuario;
        }
    }
}