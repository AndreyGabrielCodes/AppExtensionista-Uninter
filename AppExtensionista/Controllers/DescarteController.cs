using AppExtensionista.Services;
using AppExtensionista.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppExtensionista.Controllers
{
    public class DescarteController : Controller
    {
        private readonly IDescarteService _descarteService;

        public DescarteController(IDescarteService descarteService)
        {
            _descarteService = descarteService;
        }

        private int ObterIdUsuarioLogado()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : 1;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? termoPesquisa)
        {
            try
            {
                var model = await _descarteService.ObterDescartadosAgrupadosAsync(termoPesquisa);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar registros de descarte: {ex.Message}";

                return View(new DescartadosIndexViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistrarDescarteModalViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Preencha todos os campos obrigatórios para o descarte." });
                }

                int idUsuario = ObterIdUsuarioLogado();

                await _descarteService.RegistrarDescarteAsync(model, idUsuario);

                return Json(new { success = true, message = "Registro de descarte/consumo efetuado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterMotivo(int id)
        {
            try
            {
                var motivo = await _descarteService.ObterMotivoDescarteAsync(id);

                if (motivo == null)
                    return Json(new { success = false, message = "Motivo do descarte não foi localizado." });

                return Json(new { success = true, data = motivo });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}