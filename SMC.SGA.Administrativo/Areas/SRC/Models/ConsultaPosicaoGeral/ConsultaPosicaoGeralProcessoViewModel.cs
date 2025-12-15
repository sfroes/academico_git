using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConsultaPosicaoGeralProcessoViewModel : SMCViewModelBase
    {
        public string DescricaoProcesso { get; set; }

        public int QuantidadeSolicitacoes { get; set; }

        public List<PosicaoConsolidadaEtapaViewModel> Etapas { get; set; }
    }
}