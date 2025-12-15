using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class InstituicaoNivelCondicaoObrigatoriedadeFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region DataSource

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCDataSource("CondicaoObrigatoriedade")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICURDynamicService))]
        public List<SMCDatasourceItem> CondicoesObrigatoriedade { get; set; }

        #endregion DataSource

        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCSelect(nameof(InstituicaoNiveis), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCSelect("CondicaoObrigatoriedade", SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid17_24, SMCSize.Grid24_24, SMCSize.Grid17_24, SMCSize.Grid17_24)]
        public long SeqCondicaoObrigatoriedade { get; set; }
    }
}