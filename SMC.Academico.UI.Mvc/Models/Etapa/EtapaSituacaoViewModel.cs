using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Models
{
    public class EtapaSituacaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqSituacao { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public bool SituacaoFinalEtapa { get; set; }

        public bool SituacaoFinalProcesso { get; set; }

        public bool SituacaoInicialEtapa { get; set; }

        public bool SituacaoSolicitante { get; set; }
    }
}