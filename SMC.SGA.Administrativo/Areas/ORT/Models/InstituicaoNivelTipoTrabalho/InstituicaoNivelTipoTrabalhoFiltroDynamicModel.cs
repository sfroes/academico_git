using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class InstituicaoNivelTipoTrabalhoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        [SMCHidden(SMCViewMode.List)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource()]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        [SMCSelect("TiposTrabalho")]
        [SMCHidden(SMCViewMode.List)]
        public long SeqTipoTrabalho { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource("TipoTrabalho")]
        [SMCServiceReference(typeof(IORTDynamicService))]
        public List<SMCDatasourceItem> TiposTrabalho { get; set; }
    }
}