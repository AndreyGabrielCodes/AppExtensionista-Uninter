using System;
using System.ComponentModel.DataAnnotations;

namespace AppExtensionista.ViewModels
{
    public class GuiaArmazenamentoFormViewModel
    {
        public int? IdGuiaArmazenamento { get; set; }

        [Required(ErrorMessage = "Selecione o alimento que receberá este guia.")]
        [Display(Name = "Alimento Vinculado")]
        public int IdAlimento { get; set; }

        [Required(ErrorMessage = "O título do guia é obrigatório.")]
        [StringLength(150, ErrorMessage = "O título não pode ter mais de 150 caracteres.")]
        [Display(Name = "Título da Guia")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição detalhada de armazenamento é obrigatória.")]
        [Display(Name = "Instruções de Armazenamento")]
        public string Descricao { get; set; }
    }

    public class GuiaArmazenamentoDetalhesViewModel
    {
        public int IdGuiaArmazenamento { get; set; }
        public int IdAlimento { get; set; }
        public string NomeAlimento { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ResponsavelNome { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}