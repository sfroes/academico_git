using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class FormacaoEspecificaCabecalhoViewModel : SMCViewModelBase
    {
        [SMCMapProperty("Seq")]
        public long SeqPrograma { get; set; }

        [SMCMapProperty("Nome")]
        public string NomePrograma { get; set; }
    }
}