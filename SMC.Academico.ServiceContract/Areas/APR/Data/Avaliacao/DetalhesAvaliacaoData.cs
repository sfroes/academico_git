using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class DetalhesAvaliacaoData : ISMCMappable
    {
        public long SeqAvaliacao { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public SituacaoEntregaOnline Situacao { get; set; }

        public string Sigla { get; set; }

        public string Descricao { get; set; }

        public string Local { get; set; }

        public string Valor { get; set; }

        public string Data { get; set; }

        public string Nota { get; set; }

        public bool EntregaWeb { get; set; }

        public string ComentarioApuracao { get; set; }
    }
}
