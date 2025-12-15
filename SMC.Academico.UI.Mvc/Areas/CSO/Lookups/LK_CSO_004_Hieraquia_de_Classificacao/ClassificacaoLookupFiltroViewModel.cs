using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class ClassificacaoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        /// <summary>
        /// Sequencial da hierarquia
        /// </summary>
        [SMCHidden]
        public long? SeqHierarquiaClassificacao { get; set; }

        /// <summary>
        /// Sequencial do tipo de hierarquia que pode ser selecionado. Quando não informado, todos estarão disponíveis para seleção
        /// </summary>
        [SMCHidden]
        public long? SeqTipoClassificacao { get; set; }

        /// <summary>
        /// Sequenciais das hierarquias entidade itens
        /// Quando informado, serão retornados os ramos dos intens de classificação informados nas entidades responsáveis.
        /// Caso contrário restá retornada a hierarquia inteira.
        /// </summary>
        [SMCHidden]
        public long[] SeqsHierarquiaEntidadeItem { get; set; }
    }
}