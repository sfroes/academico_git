using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect))]

        public List<SMCDatasourceItem> UnidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICampanhaService), nameof(ICampanhaService.BuscarCampanhasSelect))]
        public List<SMCDatasourceItem> Campanhas { get; set; }

        #endregion [ DataSources ]

        [SMCFilter]
        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid4_24,SMCSize.Grid24_24, SMCSize.Grid4_24,SMCSize.Grid4_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid10_24,SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCSelect(nameof(UnidadesResponsaveis))]
        public long? SeqEntidadeResponsavel { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid10_24)]
        [SMCSelect(nameof(Campanhas), UseCustomSelect = true)]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(ConvocacaoController.BuscarCampanhasSelect), "Convocacao", false, new string[] { nameof(SeqEntidadeResponsavel) })]
        [SMCDependency(nameof(SeqEntidadeResponsavel), nameof(ConvocacaoController.BuscarCampanhasSelect), "Convocacao", false, new string[] { nameof(SeqCicloLetivo) })]
        public long? SeqCampanha { get; set; }

        [SMCFilter]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24,SMCSize.Grid24_24,SMCSize.Grid8_24,SMCSize.Grid6_24)]
        [SMCDependency(nameof(SeqCampanha), nameof(ConvocacaoController.BuscarProcessosSeletivosPorCampanhaSelect), "Convocacao", true)]
        public long? SeqProcessoSeletivo { get; set; }
    }
}