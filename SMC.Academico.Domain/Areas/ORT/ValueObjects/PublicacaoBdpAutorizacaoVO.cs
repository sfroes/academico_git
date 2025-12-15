using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class PublicacaoBdpAutorizacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string NomeAluno { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

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

        public string CidadeLocalidade { get; set; }

        public string DataEmissao { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string Curso { get; set; }

        public short? NumDiasAutorizacaoParcial { get; set; }
    }
}
