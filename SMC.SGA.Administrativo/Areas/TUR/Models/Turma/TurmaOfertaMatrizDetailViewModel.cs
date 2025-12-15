using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOfertaMatrizDetailViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqTurmaConfiguracaoComponente { get; set; }

        [SMCHidden]
        public long? SeqComponenteCurricularAssunto { get; set; }

        [SMCHidden]
        public short? QuantidadeVagasReservadas { get; set; }

        [SMCHidden]
        public short? QuantidadeVagasOcupadas { get; set; }

        [SMCConditionalReadonly(nameof(TurmaOfertaMatrizViewModel.DesabilitarOfertasMatrizConfiguracaoPrincipal), SMCConditionalOperation.Equals, "true", PersistentValue = true)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid4_24)]
        public bool OfertaMatrizPrincipal { get; set; }

        [SMCConditionalReadonly(nameof(DesabilitarMatrizCurricularOferta), SMCConditionalOperation.Equals, "true", PersistentValue = true)]
        [OfertaMatrizCurricularLookup(ModalWindowSize = SMCModalWindowSize.Largest)]
        //[SMCSelect(UseCustomSelect = true)]
        [SMCDependency(nameof(TurmaOfertaMatrizViewModel.SeqConfiguracaoComponente))]
        [SMCDependency(nameof(TurmaOfertaMatrizViewModel.SeqCicloLetivo))]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid13_24, SMCSize.Grid14_24)]
        [SMCUnique]
        public OfertaMatrizCurricularLookupSelectDescricaoFixBinderViewModel SeqMatrizCurricularOferta { get; set; }
        //public SMCLookupViewModel SeqMatrizCurricularOferta { get; set; }

        [SMCConditionalDisplay("ExibirReservas")]
        [SMCMask("9999")]
        [SMCMinValue(0)]
        [SMCHidden]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public short? ReservaVagas { get; set; }

        [SMCHidden]
        public bool DesabilitarMatrizCurricularOferta { get; set; }

        [SMCHidden]
        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        [SMCHidden]
        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }
    }
}