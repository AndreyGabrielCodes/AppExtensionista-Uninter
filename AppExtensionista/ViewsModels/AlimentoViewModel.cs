using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppExtensionista.Enums;

namespace AppExtensionista.ViewModels
{
    public class AlimentoCadastroViewModel
    {
        // Preenchido se for edição
        public int? IdEstoque { get; set; } 
        public bool IsEdicao { get; set; }

        // Dropdown de "Alimentos existentes" para autopreencher
        [Display(Name = "Alimentos existentes:")]
        public int? IdAlimentoSelecionado { get; set; }

        // Lista auxiliar para popular a modal/dropdown de seleção dos alimentos existentes
        public List<AlimentoOpcaoSelectViewModel> AlimentosExistentesOptions { get; set; } = new();

        [Required(ErrorMessage = "O nome do alimento é obrigatório.")]
        [Display(Name = "Nome Alimento:")]
        public string NomeAlimento { get; set; }

        [Required(ErrorMessage = "A unidade de medida é obrigatória.")]
        [Display(Name = "Unidade Medida:")]
        public UnidadeMedidaEnum UnidadeMedida { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(0.001, 999999, ErrorMessage = "A quantidade deve ser maior que zero.")]
        [Display(Name = "Quantidade:")]
        public decimal Quantidade { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [Display(Name = "Marca:")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "A data de validade é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data validade:")]
        public DateTime DataValidade { get; set; }
    }

    public class AlimentoOpcaoSelectViewModel
    {
        public int IdAlimento { get; set; }
        public string Nome { get; set; }
        public UnidadeMedidaEnum UnidadeMedida { get; set; }
    }
}