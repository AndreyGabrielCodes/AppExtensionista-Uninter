using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace AppExtensionista.Extensions
{
    public static class EnumExtensions
    {
        // Retorna o Name do [Display] (Ex: "Quilograma")
        public static string ObterNomeExibicao(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName() ?? enumValue.ToString();
        }

        // Retorna o ShortName do [Display] (Ex: "kg")
        public static string ObterSigla(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetShortName() ?? enumValue.ToString();
        }
    }
}