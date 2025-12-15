using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    /// <summary>
    /// Representa um GrupoCurricular ou GrupoCurricularComponente sendo definido pelos seqs preenchidos
    /// </summary>
    public class GrupoCurricularListaVO : ISMCMappable, ISMCSeq, ISMCTreeNode
    {
        #region [ Comum ]

        /// <summary>
        /// Será substituído por um "index" da tree para possibilitar listar GruposCurriculares e ComponentesCurriculares na mesma tree
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// Quando preenchido o registro representa um GrupoCurricular
        /// </summary>
        public long? SeqGrupoCurricular { get; set; }

        /// <summary>
        /// Quando preenchido o registro representa um GrupoCurricularComponente
        /// </summary>
        public long? SeqGrupoComponenteCurricular { get; set; }

        /// <summary>
        /// Quando preenchido o registro representa um ComponenteCurricular para visualizar detalhes
        /// </summary>
        public long? SeqComponenteCurricular { get; set; }

        /// <summary>
        /// Quando preenchido o registro associado ao curriculo curso oferta grupo
        /// </summary>
        public long SeqCurriculoCursoOferta { get; set; }

        /// <summary>
        /// Sequencial do GrupoCurricular no domínio
        /// </summary>
        public long? SeqGrupoCurricularSuperior { get; set; }

        /// <summary>
        /// "Index" do nó pai
        /// </summary>
        public long? SeqPai { get; set; }

        /// <summary>
        /// Descrição do GrupoCurricular quando for grupo ou do Componente quando for GrupoCurricularComponente
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Descrição do componente formatada segundo a regra RN_CUR_40 ou do grupo segundo a regra RN_CUR_45
        /// </summary>
        public string DescricaoFormatada { get; set; }

        /// <summary>
        /// Sequencial do tipo de configuração GrupoCurricular quando for grupo ou do Componente quando for GrupoCurricularComponente
        /// </summary>
        public long SeqTipoConfiguracao { get; set; }

        /// <summary>
        /// Descrição do tipo de configuração GrupoCurricular quando for grupo ou do Componente quando for GrupoCurricularComponente
        /// </summary>
        public string TipoConfiguracaoDescricao { get; set; }

        /// <summary>
        /// Enumerador com o formato para o grupo curricular
        /// </summary>
        public FormatoConfiguracaoGrupo? FormatoConfiguracaoGrupo { get; set; }

        /// <summary>
        /// Quantidade de itens para o grupo quando o formato selecionado é itens
        /// </summary>
        public short? QuantidadeItens { get; set; }

        /// <summary>
        /// Quantidade de hora aula para o grupo quando o formato selecionado é hora
        /// </summary>
        public short? QuantidadeHoraAula { get; set; }

        /// <summary>
        /// Quantidade de hora relogio para o grupo quando o formato selecionado é hora
        /// </summary>
        public short? QuantidadeHoraRelogio { get; set; }

        /// <summary>
        /// Quantidade de crédito para o grupo quando o formato selecionado é crédito
        /// </summary>
        public short? QuantidadeCreditos { get; set; }

        public long SeqCurriculo { get; set; }

        public long SeqNivelEnsino { get; set; }

        /// <summary>
        /// Subgrupos ou componentes do grupo curricular
        /// </summary>
        public List<GrupoCurricularListaVO> Itens { get; set; }

        #endregion [ Comum ]

        #region [ Visibilidade ]

        /// <summary>
        /// Indica se o GrupoCurricular têm Grupos Curriculares filhos
        /// </summary>
        public bool ContemGrupos { get; set; }

        /// <summary>
        /// Indica se o GrupoCurricular têm Componentes filhos
        /// </summary>
        public bool ContemComponentes { get; set; }

        #endregion [ Visibilidade ]

        #region [ Componente ]

        /// <summary>
        /// Código do Componente
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Carga Horária do Componente
        /// </summary>
        public short? CargaHoraria { get; set; }

        /// <summary>
        /// Descrição do formato do componente
        /// </summary>
        public FormatoCargaHoraria? Formato { get; set; }

        /// <summary>
        /// Créditos do componente
        /// </summary>
        public short? Credito { get; set; }

        /// <summary>
        /// O Componente tem comportamento de nó folha sem poder ser selecionado
        /// </summary>
        public bool Folha { get; set; }

        /// <summary>
        /// Sequencia do tipo do componente curricular
        /// </summary>
        public long SeqTipoComponenteCurricular { get; set; }

        ///<summary>
        /// Verifica se no tipo de configuração do grupo tem a opção de permitir subgrupos
        ///</summary>
        public bool PermiteGrupos { get; set; }

        ///<summary>
        /// Verifica se o componente é de um grupo obrigatório
        ///</summary>
        public bool Obrigatorio { get; set; }

        public SituacaoConfiguracaoComponenteCurricular SituacaoComponente { get; set; }

        public List<ConfiguracaoComponenteVO> ConfiguracoesComponentes { get; set; }

        #endregion [ Componente ]

        #region [ GrupoCurricular ]

        /// <summary>
        /// Define se o grupo curricular tem ou não formação específica, caso seja um componente será nulo
        /// </summary>
        public bool? ContemFormacaoEspecifica { get; set; }

        /// <summary>
        /// Define se o grupo curricular tem ou não beneficios, caso seja um componente será nulo
        /// </summary>
        public bool? ContemBeneficios { get; set; }

        /// <summary>
        /// Define se o grupo curricular tem ou não condições de obrigatoriedade, caso seja um componente será nulo
        /// </summary>
        public bool? ContemCondicoesObrigatoriedade { get; set; }

        #endregion [ GrupoCurricular ]
    }
}