using System.Text;

namespace SMC.Academico.Common.Areas.Shared.Helpers
{
    public class StringBuilderHelper
    {
        /// <summary>
        /// Concatena o valor ao StringBuilder se o valor estiver preenchido, adicionando o prefixo antes do valor
        /// </summary>
        /// <param name="sb">StringBuilder a ser manipulado</param>
        /// <param name="value">valor que será concatenado</param>
        /// <param name="prefix">prefixo concatenado antes do valor</param>
        public static void AppendIfNotEmpty(StringBuilder sb, string value, string prefix = null)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!string.IsNullOrEmpty(prefix))
                    sb.Append(prefix);

                sb.Append(value);
            }
        }

        /// <summary>
        /// Concatena o valor ao StringBuilder numa nova linha, se o valor estiver preenchido
        /// </summary>
        /// <param name="sb">StringBuilder a ser manipulado</param>
        /// <param name="value">valor que será concatenado</param>
        public static void AppendLineIfNotEmpty(StringBuilder sb, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                sb.AppendLine(value);
        }
    }
}
