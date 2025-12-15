using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class MatrizCurricularOfertaExcecaoLocalidadeViewModel : SMCViewModelBase, ISMCMappable
    {
        #region [ DataSource ]

        [SMCDataSource("LocalidadesExcessao", storageType: SMCStorageType.Session)]
        [SMCIgnoreProp]
        [SMCHidden]
        [SMCServiceReference(typeof(IHierarquiaEntidadeService), nameof(IHierarquiaEntidadeService.BuscarEntidadesHierarquiaSelect))]
        public List<SMCDatasourceItem> LocalidadesExcessao { get; set; }

        #endregion [ DataSource ]

        [SMCOrder(1)]
        [SMCSelect("LocalidadesExcessao")]
        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid18_24)]
        public long SeqEntidadeLocalidade { get; set; }

        
    }
}