using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DispensaMatrizExcecaoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCRequired]
        [SMCHidden]
        public long Seq { get; set; }

        [OfertaMatrizCurricularLookup]
        [SMCDependency("SeqDispensa")]
        [SMCKey]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        public OfertaMatrizCurricularLookupSelectViewModel SeqMatrizCurricularOferta { get; set; }

        [SMCMaxDate(nameof(DataFimExcecao))]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicioExcecao { get; set; }

        [SMCMinDate(nameof(DataInicioExcecao))]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataFimExcecao { get; set; }
    }
}