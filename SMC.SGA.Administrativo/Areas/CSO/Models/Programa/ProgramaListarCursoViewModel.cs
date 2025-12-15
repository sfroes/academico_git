using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ProgramaListarCursoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCDescription]
        public string Nome { get; set; }

        [SMCHidden]
        public string DescricaoInstituicaoNivelEnsino { get; set; }

        [SMCHidden]
        public string DescricaoFormatada { get { return string.Format("{0} em {1}", this.DescricaoInstituicaoNivelEnsino, this.Nome); } }
    }
}