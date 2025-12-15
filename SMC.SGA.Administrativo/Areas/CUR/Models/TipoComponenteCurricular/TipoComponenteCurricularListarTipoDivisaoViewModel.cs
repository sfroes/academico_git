using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoComponenteCurricularListarTipoDivisaoViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public string Descricao { get; set; }

        [SMCHidden]
        [SMCMapProperty("Modalidade.Descricao")]
        public string DescricaoModalidade { get; set; }

        [SMCDescription]
        public string DescricaoFormatada { get { return string.IsNullOrEmpty(DescricaoModalidade) ? this.Descricao : $"{this.Descricao} - {this.DescricaoModalidade}"; } }
    }
}