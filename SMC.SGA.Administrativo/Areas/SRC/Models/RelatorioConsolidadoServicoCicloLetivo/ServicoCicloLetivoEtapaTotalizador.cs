using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoCicloLetivoEtapaTotalizador : SMCViewModelBase, ISMCMappable
    {
        public long? SeqEtapa { get; set; }

        public string DescricaoEtapa { get; set; }

        public int? QuantidadeNaoIniciada { get; set; }

        public int? QuantidadeEmAndamento { get; set; }

        public int? QuantidadeFimComSucesso { get; set; }

        public int? QuantidadeFimSemSucesso { get; set; }

        public int? QuantidadeCancelada { get; set; }

    }
}