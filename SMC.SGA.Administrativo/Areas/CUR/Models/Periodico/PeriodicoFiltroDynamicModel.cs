using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class PeriodicoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCInclude(Ignore = true)]
        [SMCServiceReference(typeof(IClassificacaoPeriodicoService), nameof(IClassificacaoPeriodicoService.BuscarClassificacaoPeriodicoSelect))]
        public List<SMCDatasourceItem> ClassificacoesPeriodico { get; set; }

        #endregion DataSource

        [SMCHidden]
        public long? Seq { get; set; }

        [SMCIgnoreProp(SMCViewMode.Insert)]
        [SMCSelect(nameof(ClassificacoesPeriodico), SortBy = SMCSortBy.Description)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter(true, true)]
        public long SeqClassificacaoPeriodico { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter(true, true)]
        public string Descricao { get; set; }

        [SMCIgnoreProp(SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCFilter(true, true)]
        public string CodigoISSN { get; set; }

        [SMCIgnoreProp(SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCFilter(true, true)]
        public string DescAreaAvaliacao { get; set; }

        [SMCDependency(nameof(SeqClassificacaoPeriodico), nameof(PeriodicoController.BuscarQualisCapesSelect), "Periodico", true)]
        [SMCIgnoreProp(SMCViewMode.Insert)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        [SMCFilter(true, true)]
        public QualisCapes? QualisCapes { get; set; }
    }
}