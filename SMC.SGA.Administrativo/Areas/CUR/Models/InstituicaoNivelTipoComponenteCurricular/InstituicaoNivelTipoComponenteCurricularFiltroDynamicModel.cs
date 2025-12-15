using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class InstituicaoNivelTipoComponenteCurricularFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource("TipoComponenteCurricular")]
        [SMCServiceReference(typeof(ICURDynamicService))]
        public List<SMCDatasourceItem> TiposComponenteCurricular { get; set; }

        #endregion [ DataSource ]

        [SMCOrder(1)]
        [SMCFilter]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        public long? SeqInstituicaoNivel { get; set; }

        [SMCOrder(2)]
        [SMCFilter]
        [SMCSelect("TiposComponenteCurricular", SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public long? SeqTipoComponenteCurricular { get; set; }
    }
}