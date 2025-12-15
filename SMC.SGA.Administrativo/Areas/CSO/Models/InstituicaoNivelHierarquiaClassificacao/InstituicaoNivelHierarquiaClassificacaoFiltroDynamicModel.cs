using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class InstituicaoNivelHierarquiaClassificacaoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCSelect("InstituicaoNiveis")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        public long? SeqInstituicaoNivel { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveis { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCSelect("HierarquiasClassificacao", "Seq", "Descricao")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid9_24)]
        public long? SeqHierarquiaClassificacao { get; set; }

        [SMCDataSource("HierarquiaClassificacao")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> HierarquiasClassificacao { get; set; }
    }
}