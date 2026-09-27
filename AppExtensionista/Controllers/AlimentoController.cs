using AppExtensionista.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppExtensionista.Controllers
{
    public class AlimentoController : Controller
    {
        private readonly IAlimentoService _alimentoService;

        public AlimentoController(IAlimentoService alimentoService)
        {
            _alimentoService = alimentoService;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarSugestoes(string? termo)
        {
            try
            {
                var alimentos = await _alimentoService.ObterAlimentosExistentesOptionsAsync();

                if (!string.IsNullOrWhiteSpace(termo))
                {
                    var termoLower = termo.ToLower();
                    alimentos = alimentos
                        .Where(a => a.Nome.ToLower().Contains(termoLower))
                        .ToList();
                }

                return Json(alimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}