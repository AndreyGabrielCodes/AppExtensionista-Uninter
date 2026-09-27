using AppExtensionista.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppExtensionista.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        private int ObterIdUsuarioLogado()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : 1;
        }

        [HttpGet]
        public async Task<IActionResult> Perfil()
        {
            try
            {
                int idUsuario = ObterIdUsuarioLogado();
                var usuario = await _usuarioService.ObterPorIdAsync(idUsuario);
                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index", "Estoque");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarDiasAlerta(int diasAlerta)
        {
            try
            {
                int idUsuario = ObterIdUsuarioLogado();
                await _usuarioService.AtualizarDiasAlertaAsync(idUsuario, diasAlerta);
                return Json(new { success = true, message = "Configuração de alertas atualizada com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}