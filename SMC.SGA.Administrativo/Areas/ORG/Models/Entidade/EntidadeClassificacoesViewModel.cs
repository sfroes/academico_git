using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeClassificacoesViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCParameter]
        [SMCMapProperty("Seq")]
        public long SeqHierarquiaClassificacao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqTipoClassificacao { get; set; }

        [SMCMapProperty("Descricao")]
        [SMCDescription]
        public string Descricao { get; set; }

        [SMCHidden]
        public short? QuantidadeMinima { get; set; }

        [SMCHidden]
        public int QuantidadeMinimaLookup { get { return QuantidadeMinima.HasValue ? (int)QuantidadeMinima.Value : 0; } }

        [SMCHidden]
        public short? QuantidadeMaxima { get; set; }

        [SMCHidden]
        public int QuantidadeMaximaLookup { get { return QuantidadeMaxima.HasValue ? (int)QuantidadeMaxima.Value : 0; } }

        [ClassificacaoLookup(MinItemsProperty = nameof(QuantidadeMinimaLookup), MaxItemsProperty = nameof(QuantidadeMaximaLookup))]
        [SMCCssClass("smc-sga-remove-bordas")]
        [SMCDependency(nameof(EntidadeViewModel.SeqsHierarquiaEntidadeItem))]
        [SMCDependency(nameof(SeqHierarquiaClassificacao))]
        [SMCDependency(nameof(SeqTipoClassificacao))]
        [SMCMapProperty("Classificacoes")]
        [SMCSize(SMCSize.Grid24_24)]
        public List<ClassificacaoLookupViewModel> Classificacoes { get; set; }
    }
}