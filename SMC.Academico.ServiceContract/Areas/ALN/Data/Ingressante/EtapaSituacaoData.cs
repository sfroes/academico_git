using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class EtapaSituacaoData : ISMCMappable
    {
        public string Seq { get; set; }

        public long SeqSituacao { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public bool SituacaoFinalEtapa { get; set; }

        public bool SituacaoFinalProcesso { get; set; }

        public bool SituacaoInicialEtapa { get; set; }

        public bool SituacaoSolicitante { get; set; }
    }
}