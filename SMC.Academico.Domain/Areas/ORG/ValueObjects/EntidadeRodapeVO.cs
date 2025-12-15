using System.Text;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class EntidadeRodapeVO : ISMCMappable
    {
        public long Seq {  get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string NumeroLogradouro { get; set; }
        public string ComplementoLogradouro { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string NomeCidade { get; set; }
        public string SiglaUf { get; set; }
        public string DescricaoUf { get; set; }
        public int CodigoPais { get; set; }
        public string NomePais { get; set; }
        public int CodigoAreaTelefone { get; set; }
        public string NumeroTelefone { get; set; }
        public string Email { get; set; }

        private const string SEPARADOR_PIPE = " | ";

        public string EnderecoFormatado
        {
            get
            {
                var sb = new StringBuilder();
                StringBuilderHelper.AppendIfNotEmpty(sb, $"{Logradouro}");
                StringBuilderHelper.AppendIfNotEmpty(sb, NumeroLogradouro, ", ");
                StringBuilderHelper.AppendIfNotEmpty(sb, ComplementoLogradouro, SEPARADOR_PIPE);
                StringBuilderHelper.AppendIfNotEmpty(sb, Bairro, SEPARADOR_PIPE);
                StringBuilderHelper.AppendIfNotEmpty(sb, Cep, SEPARADOR_PIPE);
                StringBuilderHelper.AppendIfNotEmpty(sb, NomeCidade, SEPARADOR_PIPE);
                StringBuilderHelper.AppendIfNotEmpty(sb, DescricaoUf, SEPARADOR_PIPE);
                StringBuilderHelper.AppendIfNotEmpty(sb, NomePais, SEPARADOR_PIPE);

                return sb.ToString();
            }
        }

        public string TelefoneFormatado
        {
            get
            {
                if (CodigoAreaTelefone > 0 && !string.IsNullOrWhiteSpace(NumeroTelefone))
                {
                    var sb = new StringBuilder();
                    sb.Append("Tel.: ")
                      .Append('(').Append(CodigoAreaTelefone).Append(')')
                      .Append(NumeroTelefone);
                    return sb.ToString();
                }

                return string.Empty;
            }
        }


        public void TrimCamposTexto()
        {
            Logradouro = Logradouro?.Trim();
            NumeroLogradouro = NumeroLogradouro?.Trim();
            ComplementoLogradouro = ComplementoLogradouro?.Trim();
            Bairro = Bairro?.Trim();
            Cep = Cep?.Trim();
            NomeCidade = NomeCidade?.Trim();
            SiglaUf = SiglaUf?.Trim();
            NumeroTelefone = NumeroTelefone?.Trim();
            Email = Email?.Trim();
        }
    }
}
