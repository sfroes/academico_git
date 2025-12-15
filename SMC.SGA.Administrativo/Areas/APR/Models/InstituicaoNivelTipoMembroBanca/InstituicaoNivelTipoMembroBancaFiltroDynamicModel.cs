using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class InstituicaoNivelTipoMembroBancaFiltroDynamicModel : SMCDynamicFilterViewModel
    {

        #region [ DataSources ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        #endregion [ DataSources ]

        [SMCFilter(true, true)]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect()]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        [SMCSortable(true, true, "TipoMembroBanca")]
        public TipoMembroBanca? TipoMembroBanca { get; set; }
    }
}