using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOfertaMatrizViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqConfiguracaoComponente { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public LegendaPrincipal ConfiguracaoPrincipal { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        public SMCMasterDetailList<TurmaOfertaMatrizDetailViewModel> OfertasMatriz { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        public SMCMasterDetailList<TurmaOfertaMatrizDetailDisplayViewModel> OfertasMatrizDisplay { get; set; }

        [SMCHidden]
        public bool PermitirApenasInserirOfertas { get; set; }

        [SMCHidden]
        public bool DesabilitarAlteracaoOfertas { get; set; }

        [SMCHidden]
        public bool DesabilitarOfertasMatrizConfiguracaoPrincipal { get; set; }

        [SMCHidden]
        public bool DesabilitarMatrizCurricularOferta { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        [SMCHidden]
        public long? SeqCicloLetivo { get; set; }

    }
}