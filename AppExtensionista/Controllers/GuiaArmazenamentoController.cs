using AppExtensionista.Services;
using AppExtensionista.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppExtensionista.Controllers
{
    public class GuiaArmazenamentoController : Controller
    {
        private readonly IGuiaArmazenamentoService _guiaService;

        public GuiaArmazenamentoController(IGuiaArmazenamentoService guiaService)
        {
            _guiaService = guiaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterPorAlimento(int idAlimento)
        {
            try
            {
                var guia = await _guiaService.ObterPorAlimentoIdAsync(idAlimento);
                if (guia == null)
                    return Json(new { success = false, message = "Guia de armazenamento não encontrada para este alimento." });

                return Json(new { success = true, data = guia });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salvar(GuiaArmazenamentoFormViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Formulário da guia possui campos pendentes." });
                }

                await _guiaService.SalvarOuAtualizarGuiaAsync(model);
                return Json(new { success = true, message = "Guia de armazenamento salva com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}