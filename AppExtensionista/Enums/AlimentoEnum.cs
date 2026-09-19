using System.ComponentModel.DataAnnotations;

namespace AppExtensionista.Enums
{
    public enum UnidadeMedidaEnum
    {
        [Display(Name = "Quilograma", ShortName = "kg")]
        Quilograma = 1,

        [Display(Name = "Grama", ShortName = "g")]
        Grama = 2,

        [Display(Name = "Litragem / Litro", ShortName = "L")]
        Litro = 3,

        [Display(Name = "Mililitro", ShortName = "ml")]
        Mililitro = 4,

        [Display(Name = "Unidade / Peça", ShortName = "un")]
        Unidade = 5,

        [Display(Name = "Pacote / Saco", ShortName = "pct")]
        Pacote = 6
    }
}