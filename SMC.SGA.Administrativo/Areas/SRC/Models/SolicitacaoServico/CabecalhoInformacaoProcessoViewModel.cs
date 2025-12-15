using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class CabecalhoInformacaoProcessoViewModel : ISMCMappable
    {
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long? SeqGrupoEscalonamento { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoGrupoEscalonamento { get; set; }
    }
}