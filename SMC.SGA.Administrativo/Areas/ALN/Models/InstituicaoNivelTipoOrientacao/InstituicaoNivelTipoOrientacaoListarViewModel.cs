using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelTipoOrientacaoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; } 

        [SMCDescription]
        [SMCValueEmpty("-")]
        public string TipoTermoIntercambio { get; set; }

        [SMCDescription]
        public string TipoOrientacao { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        [SMCMapForceFromTo]
        [SMCOrder(10)]
        [SMCSize(SMCSize.Grid24_24)]
        public List<InstituicaoNivelTipoOrientacaoParticipacaoViewModel> TiposParticipacao { get; set; }
    }
}