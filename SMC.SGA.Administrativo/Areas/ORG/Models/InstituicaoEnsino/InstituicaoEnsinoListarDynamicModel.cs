using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoEnsinoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCOrder(0)]
        [SMCSortable]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        [SMCSortable(true, true)]
        public string Nome { get; set; }

        [SMCOrder(2)]
        public string Sigla { get; set; }

        [SMCOrder(3)]
        public string NomeReduzido { get; set; }

        [SMCOrder(4)]
        [SMCSortable(true, false, "Mantenedora.Nome")]
        [SMCMapProperty("Mantenedora.Nome")]
        public string NomeMantenedora { get; set; }
    }
}