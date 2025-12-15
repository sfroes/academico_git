using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMC.Framework.Mapper;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelTipoVinculoAlunoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCSize(SMCSize.Grid12_24)] 
        [SMCDescription]
        public string NivelEnsino { get; set; }

        [SMCDescription] 
        [SMCSize(SMCSize.Grid12_24)]
        public string Vinculo { get; set; }

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCMapForceFromTo]
        [SMCOrder(2)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<InstituicaoNivelTipoOrientacaoListarViewModel> TiposOrientacao { get; set; }
         
    }
}