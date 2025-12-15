using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class ConfiguracaoNumeracaoTrabalhoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCSize(SMCSize.Grid3_24)]
        public override long Seq { get; set; }
        
        [SMCSize(SMCSize.Grid6_24)]
        public  string EntidadeResponsavel { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public int NumeroUltimaNumeracao { get; set; }

        [SMCDetail(SMCDetailType.Block)]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ConfiguracaoNumeracaoTrabalhoListarOfertaViewModel> Ofertas { get; set; }

    }
}