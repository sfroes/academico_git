using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class InstituicaoNivelCriterioAprovacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource("CriterioAprovacao")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IAPRDynamicService))]
        public List<SMCDatasourceItem> CriteriosAprovacao { get; set; }

        #endregion [ DataSources ]

        [SMCFilter(true, true)]
        [SMCSelect("NiveisEnsino")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect("CriteriosAprovacao", AutoSelectSingleItem = true, SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long SeqCriterioAprovacao { get; set; }
    }
}