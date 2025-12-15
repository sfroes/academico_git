using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class InstituicaoNivelBeneficioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource(dataSource: "Beneficio")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IFINDynamicService))]
        public List<SMCDatasourceItem> Beneficios { get; set; }

        #endregion [ DataSource ]
        
        [SMCFilter(true, true)]
        [SMCSelect(nameof(InstituicaoNiveis), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long? SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(Beneficios), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long? SeqBeneficio { get; set; }
    }
}