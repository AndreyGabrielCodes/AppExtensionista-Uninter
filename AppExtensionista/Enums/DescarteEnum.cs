using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppExtensionista.Enums
{
    public enum DescarteEnum
    {
        [Display(Name = "Descarte")]
        Descarte = 1,

        [Display(Name = "Consumo")]
        Consumo = 2,

        [Display(Name = "Criado Incorretamente")]
        CriadoIncorretamente = 3
    }
}
