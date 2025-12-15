using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaDivisoesViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public string DivisaoComponenteDescricao { get; set; }

        public bool PermitirGrupo { get; set; }

        public bool GerarOrientacao { get; set; }

        public bool CriterioAprovacaoFrequencia { get; set; }

        public bool TurmaDiarioAberto { get; set; }

        public bool TurmaCancelada { get; set; }

        public bool TurmaVigente { get; set; }

        public bool DivisaoTurmaPossuiConfiguracaoesGrade { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        public SMCMasterDetailList<TurmaDivisoesDetailViewModel> DivisoesComponentes { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        public SMCMasterDetailList<TurmaDivisoesDetailDisplayViewModel> DivisoesComponentesDisplay { get; set; }

        public short Numero { get; set; }
    }
}