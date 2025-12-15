using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Models
{
    public class EntidadeHistoricoSituacaoCabecalhoViewModel : SMCViewModelBase
    {
        [SMCMapProperty("Seq")]
        public long ID { get; set; }

        [SMCMapProperty("Nome")]
        public string Entidade { get; set; }
    }
}