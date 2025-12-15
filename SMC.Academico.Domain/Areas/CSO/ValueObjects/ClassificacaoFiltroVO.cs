using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ClassificacaoFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        /// <summary>
        /// Sequencial da classificacao
        /// </summary>
        public long? Seq { get; set; }

        /// <summary>
        /// Sequencial da classificacao
        /// </summary>
        public long[] Seqs { get; set; }

        /// <summary>
        /// Sequencial da hierarquia
        /// </summary>
        public long? SeqHierarquiaClassificacao { get; set; }

        /// <summary>
        /// Sequencial do tipo de hierarquia que pode ser selecionado. Quando não informado, todos estarão disponíveis para seleção
        /// </summary>
        public long? SeqTipoClassificacao { get; set; }

        /// <summary>
        /// Sequenciais das hierarquias entidade itens
        /// Quando informado, serão retornados os ramos dos intens de classificação informados nas entidades responsáveis.
        /// Caso contrário restá retornada a hierarquia inteira.
        /// </summary>
        public long[] SeqsHierarquiaEntidadeItem { get; set; }
    }
}