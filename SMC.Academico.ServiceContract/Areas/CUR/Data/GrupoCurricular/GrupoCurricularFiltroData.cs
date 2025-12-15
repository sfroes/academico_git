using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularFiltroData : ISMCMappable
    {
        /// <summary>
        /// Sequencial do curriculo curso oferta
        /// </summary>
        public long SeqCurriculoCursoOferta { get; set; }

        /// <summary>
        /// Desconsiderar itens que possui pelo menos um componente cujo tipo foi parametrizado em
        /// "Tipo de Componente por Instituição e Nível de Ensino" para não permitir cadastro de dispensa.
        /// </summary>
        public bool? DesconsiderarItensQueNaoPermitemCadastroDispensa { get; set; }

        /// <summary>
        /// Desconsiderar itens cujos itens já foram cursados com aprovação pelo aluno ou dispensados para o aluno em questão,
        /// ou seja, a quantidade de itens (carga, crédito ou itens) da configuração do grupo já foi totalmente cumprida pelo aluno.
        /// </summary>
        public bool? DesconsiderarItensCursadosAprovacaoOuDispensadosAluno { get; set; }

        /// <summary>
        /// Desconsiderar itens vinculados ao curriculo curso oferta
        /// </summary>
        public bool? DesconsiderarItensVinculadosAoCurriculoCursoOferta { get; set; }

        /// <summary>
        /// Desconsiderar grupos com tipo de configuração igual a todos os itens obrgatórios, regra Solicitação - Itens a Serem Dispensados
        /// </summary>
        public bool? DesconsiderarGruposTodosItensObrigatorios { get; set; }

        /// <summary>
        /// Sequencial da pessoa atuacao para verificar histórico escolar
        /// </summary>
        public long? SeqPessoaAtuacao { get; set; }

        /// <summary>
        /// Filtra as formações específicas do aluno
        /// </summary>
        public bool FiltrarFormacoesEspecificasAluno { get; set; }

        /// <summary>
        /// Exibi ou não o grupo curricular que não tem compenente curricular para ser selecionado
        /// </summary>
        public bool PermitirSelecionarGruposComComponentes { get; set; }
    }
}