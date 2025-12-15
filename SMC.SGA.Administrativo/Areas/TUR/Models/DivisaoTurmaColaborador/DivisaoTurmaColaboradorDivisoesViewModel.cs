using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class DivisaoTurmaColaboradorDivisoesViewModel : SMCViewModelBase
    { 
        [SMCHidden]
        public long SeqDivisao { get; set; }

        [SMCHidden]
        public string DescricaoFormatada { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string GrupoFormatado { get; set; }
        
        [SMCSize(SMCSize.Grid6_24)]
        [SMCValueEmpty("-")]
        public string DescricaoLocalidade { get; set; }

        [SMCSize(SMCSize.Grid2_24)]
        [SMCValueEmpty("-")]
        public short? QuantidadeVagas { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCValueEmpty("-")]
        public string InformacoesAdicionais { get; set; }
    }
}