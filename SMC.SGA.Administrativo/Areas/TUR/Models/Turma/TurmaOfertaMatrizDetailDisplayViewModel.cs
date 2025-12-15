using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOfertaMatrizDetailDisplayViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid14_24)]
        public string OfertaCompleto { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCValueEmpty("-")]
        public short? ReservaVagas { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCValueEmpty("-")]
        public bool OfertaMatrizPrincipal { get; set; }

        public string DescricaoTurmaConfiguracaoComponente { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCValueEmpty("-")]
        public short? QuantidadeVagasOcupadas { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

    }
}