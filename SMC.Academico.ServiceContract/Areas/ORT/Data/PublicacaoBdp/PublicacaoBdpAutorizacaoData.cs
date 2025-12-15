using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class PublicacaoBdpAutorizacaoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string NomeAluno { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string CidadeLocalidade { get; set; }

        public string Estado { get; set; }

        public string CEP { get; set; }

        public string RG { get; set; }

        public string OrgaoEmissor { get; set; }

        public string CPF { get; set; }

        public string TipoTrabalho { get; set; }

        public string NivelEnsino { get; set; }

        public string TituloTrabalho { get; set; }

        public string TipoAutorizacao { get; set; }

        public string DataAutorizacao { get; set; }

        public string DataHoraAutorizacao { get; set; }

        public string CodigoAutorizacao { get; set; }

        public string PossuiCodigoAutorizacao { get; set; }

        public string DataEmissao { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string Curso { get; set; }

        public string EnderecoFormatado
        {
            get
            {
                return string.Format("{0}, {1}{2} - {3}, {4} - {5}, CEP: {6}",
                    string.IsNullOrEmpty(this.Logradouro) ? string.Empty : this.Logradouro.Trim(),
                    string.IsNullOrEmpty(this.Numero) ? string.Empty : this.Numero.Trim(),
                    string.IsNullOrEmpty(this.Complemento) ? string.Empty : string.Format(" / {0}", this.Complemento.Trim()),
                    string.IsNullOrEmpty(this.Bairro) ? string.Empty : this.Bairro.Trim(),
                    string.IsNullOrEmpty(this.Cidade) ? string.Empty : this.Cidade.Trim(),
                    string.IsNullOrEmpty(this.Estado) ? string.Empty : this.Estado.Trim(),
                    string.IsNullOrEmpty(this.CEP) ? string.Empty : this.CEP.Trim());
            }
        }

        public short? NumDiasAutorizacaoParcial { get; set; }
    }
}