using System.ComponentModel.DataAnnotations;

namespace AppExtensionista.ViewModels
{
    public class UsuarioModalViewModel
    {
        // Define se exibe Nome e DiasAlerta vão ser exibidos
        public bool IsCriarUsuario { get; set; } 

        [Required(ErrorMessage = "O login/e-mail é obrigatório.")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Display(Name = "Nome")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Dias Alerta Vencimento")]
        [Range(1, 30, ErrorMessage = "Informe um valor entre 1 e 30 dias.")]
        public int? DiasAlertaVencimento { get; set; } = 7;
    }

    public class AlterarDiasAlertaViewModel
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de dias.")]
        [Range(1, 30, ErrorMessage = "O intervalo de dias deve ser entre 1 e 30.")]
        [Display(Name = "Número de dias de aviso de vencimento")]
        public int DiasAlertaVencimento { get; set; }
    }
}