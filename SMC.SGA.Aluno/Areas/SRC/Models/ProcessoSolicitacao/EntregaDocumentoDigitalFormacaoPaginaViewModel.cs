using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class EntregaDocumentoDigitalFormacaoPaginaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long SeqFormacaoEspecifica { get; set; }

        public string DescricaoFormacaoEspefica { get; set; }
    }
}