using System;
using System.Collections.Generic;

namespace AppExtensionista.ViewModels
{
    public class EstoqueIndexViewModel
    {
        // Totalizadores
        public int TotalAlimentosCadastrados { get; set; }
        public int TotalVencidos { get; set; }
        public int TotalDescartados { get; set; }

        // Filtro de pesquisa
        public string? TermoPesquisa { get; set; }

        // Lista agrupada por nome de alimento
        public List<EstoqueAgrupadoViewModel> Agrupamentos { get; set; } = new();
    }

    public class EstoqueAgrupadoViewModel
    {
        public int IdAlimento { get; set; }
        public string NomeAlimento { get; set; }
        public decimal QuantidadeTotalSomada { get; set; }
        public string UnidadeMedidaSigla { get; set; }

        // Itens individuais exibidos ao expandir
        public List<EstoqueItemDetalheViewModel> Itens { get; set; } = new();
    }

    public class EstoqueItemDetalheViewModel
    {
        public int IdEstoque { get; set; }
        public DateTime DataValidade { get; set; }
        public decimal Quantidade { get; set; }
        public string Marca { get; set; }
        public bool IsVencido => DataValidade.Date < DateTime.Today;
    }
}