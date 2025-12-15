using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class PosicaoConsolidadaListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public long? SeqProcesso { get; set; }

        [SMCHidden]
        public long? SeqGrupoEscalonamento { get; set; }

        public string GrupoEscalonamento { get; set; }

        public int QuantidadeSolicitacoes { get; set; }

        [SMCHidden]
        public string DescricaoProcesso { get; set; }

        public List<PosicaoConsolidadaEtapaViewModel> Etapas { get; set; }
    }
}