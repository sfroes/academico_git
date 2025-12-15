using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Academico.UI.Mvc.Areas.TUR.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class GradeHorariaCompartilhadaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSource

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        [SMCDataSource]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        #endregion DataSource

        [SMCHidden]
        [SMCParameter]
        public long? Seq { get; set; }

        [CicloLetivoLookup]
        [SMCSize(SMCSize.Grid6_24)]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCSelect(nameof(EntidadesResponsaveis), ForceMultiSelect = true)]
        [SMCSize(SMCSize.Grid18_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [TurmaLookup]
        [SMCDependency(nameof(SeqCicloLetivo))]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid10_24)]
        public TurmaLookupViewModel SeqTurma { get; set; }
        
        [SMCSize(SMCSize.Grid6_24)]
        public string Descricao { get; set; }
    }
}