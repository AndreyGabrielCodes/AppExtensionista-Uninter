using System;
using System.Collections.Generic;

namespace AppExtensionista.ViewModels
{
    // ViewModel para o Pop-up exibido apenas uma vez logo após o login
    public class AlertaVencimentoPopUpViewModel
    {
        public int DiasConfiguradosUsuario { get; set; }

        // Lista de produtos que vencem até o limite de dias do alerta do usuário
        public List<ItemAlertaVencimentoViewModel> ItensAlerta { get; set; } = new();

        // Propriedade auxiliar para saber se o modal deve ser aberto no frontend
        public bool ExibirPopUp => ItensAlerta.Count > 0;
    }

    public class ItemAlertaVencimentoViewModel
    {
        public int IdEstoque { get; set; }
        public string NomeAlimento { get; set; }
        public decimal Quantidade { get; set; }
        public string UnidadeMedidaSigla { get; set; }
        public string Marca { get; set; }
        public DateTime DataValidade { get; set; }

        // Indica quantos dias faltam ou se já venceu
        public int DiasAteVencimento => (DataValidade.Date - DateTime.Today).Days;

        public bool IsVencido => DateTime.Today > DataValidade.Date;
    }
}