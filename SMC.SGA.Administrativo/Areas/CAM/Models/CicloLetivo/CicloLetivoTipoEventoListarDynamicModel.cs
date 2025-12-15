using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoTipoEventoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        public string DescricaoTipoEvento { get; set; }

        [SMCHidden]
        public SMCMasterDetailList<CicloLetivoTipoEventoNivelEnsinoListarViewModel> NiveisEnsino { get; set; }

        public List<string> NiveisEnsinoGrid => NiveisEnsino?.Select(s => s.Descricao).ToList();

        [SMCHidden]
        public SMCMasterDetailList<CicloLetivoTipoEventoParametroListarViewModel> Parametros { get; set; }

        public List<string> ParametrosGrid => Parametros?.Select(s => SMCEnumHelper.GetDescription(s.TipoParametroEvento)).ToList();
    }
}