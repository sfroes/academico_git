using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaGrupoConfiguracaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        [SMCKey]
        public long SeqConfiguracaoComponente { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        public long SeqComponenteCurricular { get; set; }

        [SMCSize(SMCSize.Grid2_24)]
        public bool Selecionado { get; set; }

        public bool SelecionadoGrid { get; set; }

        public bool Principal { get; set; }

        public bool SomenteLeitura { get; set; }
    }
}