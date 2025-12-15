using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "filtroVigencia", Size = SMCSize.Grid7_24, CssClass = "smc-size-md-12 smc-size-xs-24 smc-size-sm-10 smc-size-lg-7 smc-sga-fieldset-inline smc-sga-fieldset-inline-recuo-topo")]
    public class DispensaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCHidden]
        public bool TipoComponenteDispensa => true;

        [ComponenteCurricularLookup]
        [SMCDependency(nameof(TipoComponenteDispensa))]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        public ComponenteCurricularLookupViewModel SeqComponenteCurricular { get; set; }

        [SMCCheckBoxList]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid8_24)]
        public TipoDispensa TipoFiltro { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCFilter(true, true)]
        [SMCGroupedProperty("filtroVigencia")]
        [SMCSize(SMCSize.Grid12_24)]
        public DateTime? DataInicioVigencia { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        [SMCFilter(true, true)]
        [SMCGroupedProperty("filtroVigencia")]
        [SMCMinDate(nameof(DataInicioVigencia))]
        [SMCSize(SMCSize.Grid12_24)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        public MatrizExcecaoDispensa? MatrizAssociada { get; set; }

        [OfertaMatrizCurricularLookup]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        public OfertaMatrizCurricularLookupSelectViewModel MatrizExcecao { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public ModoExibicaoHistoricoEscolar? ModoExibicaoHistoricoEscolar { get; set; }
    }
}