using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoHierarquiaEntidadeCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public TipoVisao TipoVisao { get; set; }
    }
}