using AppExtensionista.Services;
using AppExtensionista.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppExtensionista.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly IEstoqueService _estoqueService;

        public EstoqueController(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? termoPesquisa)
        {
            try
            {
                var model = await _estoqueService.ObterEstoqueAgrupadoAsync(termoPesquisa);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar o estoque: {ex.Message}";

                return View(new EstoqueIndexViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarItem(AlimentoCadastroViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, message = "Dados inválidos. Verifique os campos preenchidos." });

                await _estoqueService.SalvarOuAtualizarItemEstoqueAsync(model);

                return Json(new { success = true, message = "Item salvo no estoque com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterParaEdicao(int id)
        {
            try
            {
                var item = await _estoqueService.ObterParaEdicaoAsync(id);

                if (item == null)
                    return Json(new { success = false, message = "Item do estoque não foi encontrado ou está zerado." });

                return Json(new { success = true, data = item });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObterAlertasVencimento()
        {
            try
            {
                var alertas = await _estoqueService.ObterAlertasVencimentoAsync();

                return Json(new { success = true, data = alertas });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}