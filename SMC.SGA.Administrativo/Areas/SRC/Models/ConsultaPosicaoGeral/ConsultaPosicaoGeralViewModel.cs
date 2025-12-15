using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConsultaPosicaoGeralViewModel : SMCViewModelBase
    {
        public int QuantidadeSolicitacoesTotal { get; set; }

        public SMCPagerModel<ConsultaPosicaoGeralProcessoViewModel> Processos { get; set; }
    }
}