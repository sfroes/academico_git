using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoOfertaCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        [SMCMapProperty("NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }

        public string Nome { get; set; }
    }
}