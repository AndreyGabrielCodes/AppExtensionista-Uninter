using AppExtensionista.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppExtensionista.ViewModels
{
    // Modal acionado pelo botão de exclusão [X] da tela de estoque
    public class RegistrarDescarteModalViewModel
    {
        [Required]
        public int IdEstoque { get; set; }

        public string NomeAlimento { get; set; }
        public string Marca { get; set; }
        public decimal QuantidadeDisponivel { get; set; }

        [Required(ErrorMessage = "Informe a quantidade a descartar.")]
        [Range(0.001, 999999, ErrorMessage = "A quantidade deve ser maior que zero.")]
        [Display(Name = "Quantidade a Descartar")]
        public decimal QuantidadeDescartar { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de descarte.")]
        [Display(Name = "Tipo de Descarte")]
        public DescarteEnum TipoDescarte { get; set; }

        [Required(ErrorMessage = "Informe o motivo do descarte.")]
        [Display(Name = "Descrição do Motivo")]
        public string DescricaoMotivo { get; set; }
    }

    // Listagem da Tela "Descartados"
    public class DescartadosIndexViewModel
    {
        public int TotalDescartadosConsumidos { get; set; }
        public string? TermoPesquisa { get; set; }
        public List<DescartadoAgrupadoViewModel> Agrupamentos { get; set; } = new();
    }

    public class DescartadoAgrupadoViewModel
    {
        public string NomeAlimento { get; set; }
        public decimal QuantidadeTotalDescartada { get; set; }
        public string UnidadeMedidaSigla { get; set; }
        public List<DescartadoItemDetalheViewModel> Itens { get; set; } = new();
    }

    public class DescartadoItemDetalheViewModel
    {
        public int IdRegistroDescarte { get; set; }
        public DateTime DataDescarte { get; set; }
        public decimal Quantidade { get; set; }
        public string Marca { get; set; }
        public DescarteEnum TipoDescarte { get; set; }
        public string DescricaoMotivo { get; set; }
    }

    // Modal para exibição do motivo na tela de Descartados
    public class MotivoDescarteModalViewModel
    {
        public DescarteEnum TipoDescarte { get; set; }
        public string DescricaoMotivo { get; set; }
    }
}