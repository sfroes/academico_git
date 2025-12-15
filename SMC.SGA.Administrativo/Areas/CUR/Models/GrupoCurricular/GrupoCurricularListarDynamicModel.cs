using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class GrupoCurricularListarDynamicModel : SMCDynamicViewModel, ISMCTreeNode, ISMCSeq
    {
        #region [ Comum ]

        /// <summary>
        /// Será substituído por um "index" da tree para possibilitar listar GruposCurriculares e ComponentesCurriculares na mesma tree
        /// </summary>
        [SMCKey]
        public override long Seq { get; set; }

        /// <summary>
        /// Quando preenchido o registro representa um GrupoCurricular
        /// </summary>
        [SMCHidden]
        public long? SeqGrupoCurricular { get; set; }

        /// <summary>
        /// Quando preenchido o registro representa um GrupoCurricularComponente
        /// </summary>
        [SMCHidden]
        public long? SeqGrupoComponenteCurricular { get; set; }

        /// <summary>
        /// Quando preenchido o registro representa um ComponenteCurricular para visualizar detalhes
        /// </summary>
        [SMCHidden]
        public long? SeqComponenteCurricular { get; set; }

        /// <summary>
        /// Sequencial do GrupoCurricular no domínio
        /// </summary>
        [SMCHidden]
        public long? SeqGrupoCurricularSuperior { get; set; }

        /// <summary>
        /// "Index" do nó pai
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long? SeqPai { get; set; }

        /// <summary>
        /// Sequencial do currículo pai da hierarquia
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculo { get; set; }

        /// <summary>
        /// Sequencial do nível de ensino do currículo
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long SeqNivelEnsino { get; set; }

        /// <summary>
        /// Descrição do GrupoCurricular quando for grupo ou do Componente quando for GrupoCurricularComponente
        /// </summary>
        [SMCHidden]
        public string Descricao { get; set; }

        /// <summary>
        /// Descrição do componente formatada segundo a regra RN_CUR_40 ou do grupo segundo a regra RN_CUR_45
        /// </summary>
        [SMCDescription]
        [SMCHidden]
        public string DescricaoFormatada { get; set; }

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

        #endregion [ Comum ]

        #region [ Visibilidade ]

        /// <summary>
        /// Indica se o GrupoCurricular têm Grupos Curriculares filhos
        /// </summary>
        [SMCHidden]
        public bool ContemGrupos { get; set; }

        /// <summary>
        /// Indica se o GrupoCurricular têm Componentes filhos
        /// </summary>
        [SMCHidden]
        public bool ContemComponentes { get; set; }

        #endregion [ Visibilidade ]

        #region [ Componente ]

        /// <summary>
        /// Sequencia do tipo do componente curricular
        /// </summary>
        public long SeqTipoComponenteCurricular { get; set; }

        /// <summary>
        /// Código do Componente
        /// </summary>
        [SMCHidden]
        public string Codigo { get; set; }

        /// <summary>
        /// Carga Horária do Componente
        /// </summary>
        [SMCHidden]
        public short? CargaHoraria { get; set; }

        /// <summary>
        /// Descrição do formato do componente
        /// </summary>
        [SMCHidden]
        public FormatoCargaHoraria? Formato { get; set; }

        /// <summary>
        /// Créditos do componente
        /// </summary>
        [SMCHidden]
        public short? Credito { get; set; }

        ///<summary>
        /// Verifica se no tipo de configuração do grupo tem a opção de permitir subgrupos
        ///</summary>
        [SMCHidden]
        public bool PermiteGrupos { get; set; }

        [SMCHidden]
        public SituacaoConfiguracaoComponenteCurricular SituacaoComponente { get; set; }

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

        public SituacaoConfiguracaoGrupoCurricular SituacaoConfiguracaoGrupoCurricular { get; set; }

        #endregion [ GrupoCurricular ]
    }
}