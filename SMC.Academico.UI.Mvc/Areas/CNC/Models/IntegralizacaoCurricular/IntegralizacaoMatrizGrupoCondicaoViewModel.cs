using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizGrupoCondicaoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long SeqCondicaoObrigatoriedade { get; set; }

        [SMCDescription]
        public string DescricaoCondicaoObrigatoriedade { get; set; }     
    }
}
