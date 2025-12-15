using System.Linq;
using System.Text;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeRodapeData : ISMCMappable
    {
        public long Seq { get; set; }
        public string Nome { get; set; }
        public string EnderecoFormatado { get; set; }
        public string TelefoneFormatado { get; set; }
        public string Email { get; set; }
        
        public override string ToString()
        {
            var sb = new StringBuilder();

            StringBuilderHelper.AppendLineIfNotEmpty(sb, Nome);
            StringBuilderHelper.AppendLineIfNotEmpty(sb, EnderecoFormatado);

            string contato = string.Join(" | ", new[] { TelefoneFormatado, Email }.Where(v => !string.IsNullOrWhiteSpace(v)));

            StringBuilderHelper.AppendLineIfNotEmpty(sb, contato);

            return sb.ToString();
        }

    }
}
