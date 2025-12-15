using System.Text.RegularExpressions;

namespace SMC.Academico.Common.Areas.Shared.Helpers
{
    public class FormatarString
    {
        public static string Truncate(string texto, int tamanho)
        {
            return texto.Length > tamanho ? texto.Trim().Substring(0, tamanho) : texto.Trim();
        }

        public static string ObterSomenteNumeros(string texto)
        {
            return Regex.Replace(texto, "[^0-9]", "").Trim();
        }

        public static string ObterNumerosComVirgula(string texto)
        {
            return Regex.Replace(texto, "[^0-9,]", "").Trim();
        }
    }
}
