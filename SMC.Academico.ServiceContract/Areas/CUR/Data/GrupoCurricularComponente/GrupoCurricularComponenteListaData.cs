using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularComponenteListaData : ISMCMappable, ISMCSeq
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
        /// Quando o registro representa um grupo currícular preenchido com o sequencial do CurriculoCursoOfertaGrupo que amarra este grupo a uma oferta
        /// </summary>
        public long SeqCurriculoCursoOfertaGrupo { get; set; }

        /// <summary>
        /// Sequencial do GrupoCurricular no domínio
        /// </summary>
        public long? SeqGrupoCurricularSuperior { get; set; }

        /// <summary>
        /// "Index" do nó pai
        /// </summary>
        public long? SeqPai { get; set; }

        /// <summary>
        /// Sequencial do currículo pai da hierarquia
        /// </summary>
        public long SeqCurriculo { get; set; }

        /// <summary>
        /// Sequencial do nível de ensino do currículo
        /// </summary>
        public long SeqNivelEnsino { get; set; }

        /// <summary>
        /// Descrição do GrupoCurricular quando for grupo ou do Componente quando for GrupoCurricularComponente
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Descrição do componente formatada segundo a regra RN_CUR_40 ou do grupo segundo a regra RN_CUR_45
        /// </summary>
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
        public bool ContemGrupos { get; set; }

        /// <summary>
        /// Indica se o GrupoCurricular têm Componentes filhos
        /// </summary>
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

        ///<summary>
        /// Verifica se no tipo de configuração do grupo tem a opção de permitir subgrupos
        ///</summary>
        public bool PermiteGrupos { get; set; }

        public SituacaoConfiguracaoComponenteCurricular SituacaoComponente { get; set; }

        #endregion [ Componente ]
    }
}