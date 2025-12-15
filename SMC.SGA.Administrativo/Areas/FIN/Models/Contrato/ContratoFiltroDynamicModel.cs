using System;
using System.Linq;
using System.Web;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class ContratoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region Data Sources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IHierarquiaEntidadeService), nameof(IHierarquiaEntidadeService.BuscarEntidadesHierarquiaSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarLocalidadesAtivasSelect))]
        public List<SMCDatasourceItem> Localidades { get; set; }

        #endregion Data Sources

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCOrder(0)]
        [SMCFilter]
        public string NumeroRegistro { get; set; }

        [SMCOrder(1)]
        [SMCFilter] 
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; } 

        [SMCOrder(2)]
        [SMCFilter]
        [SMCSelect(nameof(NiveisEnsino), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public long? SeqNivelEnsino { get; set; } 
        
        [SMCFilter]
        [SMCOrder(4)]
        [CursoLookup]
        [SMCHidden(SMCViewMode.List)] 
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid8_24)] 
        public CursoLookupViewModel SeqCurso { get; set; }
         
    }
}