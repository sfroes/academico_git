using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class InstituicaoTipoEventoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public string DescricaoInstituicaoEnsino { get; set; }

        [SMCDescription]
        public string DescricaoTipoEvento { get; set; }

        public string DescricaoTipoAgenda { get; set; }

        public AbrangenciaEvento AbrangenciaEvento { get; set; }

        public List<InstituicaoTipoEventoParametroViewModel> Parametros { get; set; }
    }
}