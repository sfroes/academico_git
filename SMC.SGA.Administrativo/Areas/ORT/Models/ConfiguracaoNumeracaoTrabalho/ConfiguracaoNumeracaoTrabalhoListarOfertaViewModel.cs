using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class ConfiguracaoNumeracaoTrabalhoListarOfertaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid24_24)]
        public string Descricao { get; set; }

    }
}