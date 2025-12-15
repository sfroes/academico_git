using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc
{
    /// <summary>
    /// Classe que mapeia os caminhos virtuais da views de segurança para o caminho do resource no assembly.
    /// </summary>
    public class AcademicoExternalViews : ISMCEmbeddedViewAssembly
    {
        /*
       1) Para fazer funcionar tem que mudar a rota na area no sistema. Procura TODO: DynamicUIMVC
       2) Coloque a View como EmbeddedResource
       3) Crie um PATH (nome do projeto mais o caminha ate chegar na View) e concatene com a view
      */
        public const string MATERIAL_PATH = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/Material/";

        public const string LISTA_FREQUENCIA_PATH = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/ListaFrequencia/";

        public const string DOWNLOAD_PATH = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/DownloadMaterial/";

        public const string LANCAMENTO_HISTORICOESCOLAR_PATH = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/LancamentoHistoricoEscolar/";

        public const string AULA_PATH = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/Aula/";

        public const string SELECAO_ATIVIDADE = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Views/SelecaoAtividade/SelecaoAtividade";

        public const string SELECAO_ATIVIDADE_OFERTA = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Views/SelecaoAtividade/_SelecaoAtividadeOferta";

        public const string VISUALIZAR_ITENS_TURMA_ATIVIDADES = "SMC.Academico.UI.Mvc.dll/Areas/MAT/Views/SolicitacaoMatriculaItem/VisualizarTurmaAtividade";

        public const string VISUALIZAR_ITENS_ATIVIDADE_DETAIL = "SMC.Academico.UI.Mvc.dll/Areas/MAT/Views/SolicitacaoMatriculaItem/_VisualizarAtividadeDetail";

        public const string VISUALIZAR_ITENS_TURMA_DETAIL = "SMC.Academico.UI.Mvc.dll/Areas/MAT/Views/SolicitacaoMatriculaItem/_VisualizarTurmaDetail";

        public const string VISUALIZAR_ITENS_TURMA_DIVISOES = "SMC.Academico.UI.Mvc.dll/Areas/MAT/Views/SolicitacaoMatriculaItem/_VisualizarTurmaDivisoesDetail";

        public const string ENDERECO_EDITAR = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaEndereco/Editar";

        public const string ENDERECO_INCLUIR = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaEndereco/Incluir";

        public const string ENDERECO_PARTIAL = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaEndereco/_EditarEnderecoPessoa";

        public const string ENDERECO_ELETRONICO_EDITAR = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaEnderecoEletronico/Editar";

        public const string ENDERECO_ELETRONICO_INCLUIR = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaEnderecoEletronico/Incluir";

        public const string ENDERECO_ELETRONICO_PARTIAL = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaEnderecoEletronico/_EditarEnderecoEletronicoPessoa";

        public const string TELEFONE_EDITAR = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaTelefone/Editar";

        public const string TELEFONE_INCLUIR = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaTelefone/Incluir";

        public const string TELEFONE_PARTIAL = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/PessoaTelefone/_EditarTelefonePessoa";

        public const string REGISTRAR_DOCUMENTOS = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/RegistroDocumento/RegistrarDocumentos";

        public const string REGISTRAR_DOCUMENTOS_CABECALHO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/RegistroDocumento/_Cabecalho";

        public const string REGISTRAR_DOCUMENTOS_DOCUMENTO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/RegistroDocumento/_Documento";

        public const string UPLOAD_SOLICITACAO_PADRAO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/UploadSolicitacaoPadrao";
        public const string DADOS_MODAL_SOLICITACAO_SERVICO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/ModalConsultaSolicitacaoServico";
        public const string DADOS_MODAL_SOLICITACAO_SERVICO_IDENTIFICACAO_SOLICITANTE = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_DadosIdentificacaoSolicitante";
        public const string DADOS_MODAL_SOLICITACAO_SERVICO_DOCUMENTACAO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_DocumentacaoSolicitacao";
        public const string DADOS_MODAL_SOLICITACAO_SERVICO_TAXAS = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_TaxasSolicitacao";
        public const string DADOS_MODAL_SOLICITACAO_SERVICO_HISTORICO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_HistoricoSolicitacao";
        public const string DADOS_MODAL_SOLICITACAO_SERVICO_HISTORICO_ITEM = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_HistoricoSolicitacaoItem";
        public const string DADOS_MODAL_SOLICITACAO_SERVICO_HISTORICO_SITUACAO_FINAL_ETAPA_ITEM = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SituacaoFinalEtapaLegendItem";
        public const string SOLICITACAO_SELECAO_TURMA_PLANO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SelecaoTurmaPlano";
        public const string SOLICITACAO_SELECAO_TURMA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SelecaoTurma";
        public const string SOLICITACAO_SELECAO_TURMA_EFETIVACAO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SelecaoTurmaEfetivacao";
        public const string SOLICITACAO_SELECAO_ATIVIDADE_ACADEMICA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SelecaoAtividadeAcademica";
        public const string SOLICITACAO_INSTRUCOES_FINAIS_EFETIVACAO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/InstrucoesFinaisEfetivacao";
        public const string REGISTRAR_ENTREGA_DOCUMENTOS = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/RegistrarEntregaDocumentos";
        public const string FORMULARIO_SOLICITACAO_PADRAO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/FormularioSolicitacaoPadrao";
        public const string CONFIRMACAO_TURMA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/ConfirmacaoTurma";
        public const string CHANCELA_PLANO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SelecaoChancelaPlano";
        public const string SOLICITACAO_ATIVIDADE_COMPLEMENTAR = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SolicitacaoAtividadeComplementar";
        public const string IMPRESSAO_SOLICITACAO_PADRAO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_ImpressaoInstrucoesFinaisSolicitacaoPadrao";
        public const string IMPRESSAO_SOLICITACAO_PADRAO_FORMULARIO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_ImpressaoSolicitacaoPadraoFormulario";
        public const string SELECAO_COMPONENTE_CURRICULAR = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_ComponenteCurricularSolicitacao";


        public const string SOLICITACAO_DISPENSA_ITENS_DISPENSADOS = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SolicitacaoDispensaItensDispensados";
        public const string SOLICITACAO_DISPENSA_ITENS_CURSADOS = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SolicitacaoDispensaItensCursados";
        public const string SOLICITACAO_COBRANCA_TAXA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/SolicitacaoCobrancaTaxa";

        public const string DETALHES_DIVISAO_TURMA = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/DetalhesDivisaoTurma/DetalhesDivisaoTurma";
        public const string DETALHES_DIVISAO_TURMA_DEPENDENCY = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/DetalhesDivisaoTurma/_DetalhesDivisaoTurmaDependency";
        public const string DETALHES_DIVISAO_TURMA_CABECALHO = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/DetalhesDivisaoTurma/CabecalhoDetalhesDivisaoTurma";
        public const string DETALHES_DIVISAO_TURMA_COLABORADOR = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/DetalhesDivisaoTurma/_DetalhesDivisaoTurmaColaborador";
        public const string DETALHES_DIVISAO_TURMA_COLABORADOR_ORGANIZACAO = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/DetalhesDivisaoTurma/_DetalhesDivisaoTurmaColaboradorOrganizacao";


        public const string SELECAO_TURMA = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/SelecaoTurma/SelecaoTurma";
        public const string SELECAO_TURMA_OFERTA = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/SelecaoTurma/_SelecaoTurmaOferta";
        public const string SELECAO_TURMA_OFERTA_DIVISAO = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/SelecaoTurma/_SelecaoTurmaOfertaDivisoes";
        public const string SELECAO_TURMA_PESQUISAR = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/SelecaoTurma/_SelecaoTurmaPesquisar";

        public const string _SELECAO_TURMA_OFERTA_DIVISOES = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoTurmaOfertaDivisoes";
        public const string _SELECAO_TURMA_OFERTA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoTurmaOferta";
        public const string _SELECAO_TURMA_OFERTA_DIVISOES_EDITAR = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoTurmaOfertaDivisoesEditar";
        public const string _SELECAO_TURMA_OFERTA_EDITAR = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoTurmaOfertaEditar";
        public const string _SELECAO_TURMA_DEPENDENCY_GRUPO_PROGRAMA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoTurmaDependencyGrupoPrograma";
        public const string _SELECAO_ATIVIDADE_OFERTA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoAtividadeOferta";
        public const string _CONFIRMACAO_TURMA_OFERTA_DIVISOES = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_ConfirmacaoTurmaOfertaDivisoes";
        public const string _CONFIRMACAO_TURMA_OFERTA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_ConfirmacaoTurmaOferta";
        public const string _CONFIRMACAO_ATIVIDADE_OFERTA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_ConfirmacaoAtividadeOferta";
        public const string _CHANCELA_PLANO_TURMA = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoChancelaPlanoTurma";
        public const string _CHANCELA_PLANO_TURMA_DIVISOES = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoChancelaPlanoTurmaDivisoes";
        public const string _CHANCELA_PLANO_ATIVIDADES = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_SelecaoChancelaPlanoAtividade";

        public const string _REGISTRAR_DOCUMENTOS = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_RegistrarDocumentos";
        public const string _REGISTRAR_DOCUMENTOS_DOCUMENTO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_Documento";

        public const string _REGISTRAR_REGISTRAR_DOC_ITEM = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_RegistrarEntregaDocumentosItem";
        public const string _REGISTRAR_REGISTRAR_DOC_ITEM_SOLICITACAO_PADRAO = "SMC.Academico.UI.Mvc.dll/Areas/SRC/Views/SolicitacaoServicoFluxoBase/_RegistrarEntregaDocumentosItemSolicitacaoPadrao";

        //SMC.Academico.UI.Mvc.AcademicoExternalViews._TERMO_INTERCAMBIO_002_DETAIL_LIST
        public const string _MATRIZ_CURRICULAR_005_DETAIL_LIST = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_005_Oferta_de_Matriz_Curricular/_DetailList";
        public const string _MATRIZ_CURRICULAR_005_DETAIL_HEADER = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_005_Oferta_de_Matriz_Curricular/_DetailHeader";

        public const string _MATRIZ_CURRICULAR_003_DETAIL_LIST = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_003_Matriz_Curricular/_DetailList";
        public const string _CONFIG_COMPONENTE_002_DETAIL_LIST = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Lookups/LK_CUR_002_Configuracao_de_Componente_Curricular/_DetailList";
        public const string _TERMO_INTERCAMBIO_002_DETAIL_LIST = "SMC.Academico.UI.Mvc.dll/Areas/ALN/Lookups/LK_ALN_002_Termo_Intercambio/_DetailList";

        public const string INTEGRALIZACAO_CURRICULAR_PRINCIPAL = "SMC.Academico.UI.Mvc.dll/Areas/CNC/Views/IntegralizacaoCurricular/ConsultaIntegralizacaoCurricular";
        public const string INTEGRALIZACAO_CURRICULAR_CABECALHO = "SMC.Academico.UI.Mvc.dll/Areas/CNC/Views/IntegralizacaoCurricular/_CabecalhoIntegralizacaoCurricular";
        public const string INTEGRALIZACAO_CURRICULAR_LISTAR = "SMC.Academico.UI.Mvc.dll/Areas/CNC/Views/IntegralizacaoCurricular/_ListarIntegralizacaoCurricular";
        public const string INTEGRALIZACAO_CURRICULAR_GRUPO = "SMC.Academico.UI.Mvc.dll/Areas/CNC/Views/IntegralizacaoCurricular/_GrupoIntegralizacaoCurricular";
        public const string INTEGRALIZACAO_CURRICULAR_DIVISAO = "SMC.Academico.UI.Mvc.dll/Areas/CNC/Views/IntegralizacaoCurricular/_DivisaoIntegralizacaoCurricular";
        public const string INTEGRALIZACAO_CURRICULAR_MODAL_HISTORICO = "SMC.Academico.UI.Mvc.dll/Areas/CNC/Views/IntegralizacaoCurricular/_ModalHistoricoIntegralizacaoCurricular";

        public const string COMPONENTE_CURRICULAR_EMENTA_MODAL = "SMC.Academico.UI.Mvc.dll/Areas/CUR/Views/ComponenteCurricularEmenta/ModalComponenteCurricularEmenta";

        //Chancela listagem professor
        public const string CHANCELA_PROFESSOR_INDEX = "SMC.Academico.UI.Mvc.dll/Areas/MAT/Views/Chancela/Index";
        public const string CHANCELA_PROFESSOR_LISTAR = "SMC.Academico.UI.Mvc.dll/Areas/MAT/Views/Chancela/_ChancelaItem";


        // Credenciais de acesso
        public const string CREDENCIAIS_ACESSO_INDEX = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/CredenciaisAcesso/Index";

        // Suporte Técnico
        public const string SUPORTE_TECNICO_INDEX = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/SuporteTecnico/Index";
        public const string INTEGRACAO_CSC = "SMC.Academico.UI.Mvc.dll/Areas/PES/Views/SuporteTecnico/IntegracaoCSC";

        // Visualizar Dados de intercâmbio do aluno
        public const string VISUALIZAR_DADOS_INTERCAMBIO_ALUNO = "SMC.Academico.UI.Mvc.dll/Areas/ALN/Views/_VisualizarDadosIntercambioAluno";

        // Views da Entrega Online
        public const string ENTREGA_ONLINE_HISTORICO_SITUACAO = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/EntregaOnlineShared/HistoricoSituacaoEntrega";

        public const string RELATORIO_ACOMPANHAMENTO_NOTA = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/RelatorioAcompanhamentoNota/GerarRelatorio";
        public const string RELATORIO_ACOMPANHAMENTO_NOTA_CABECALHO = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/RelatorioAcompanhamentoNota/_CabecalhoRelatorio";
        public const string RELATORIO_ACOMPANHAMENTO_NOTA_RODAPE = "SMC.Academico.UI.Mvc.dll/Areas/APR/Views/RelatorioAcompanhamentoNota/_RodapeRelatorio";

        // Views da Seleção de PLano de Estudos /MAT
        public const string PLANO_ESTUDO_SELECAO_TURMA = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/PlanoEstudoSelecaoTurma/PlanoEstudoSelecaoTurma";
        public const string PLANO_ESTUDO_SELECAO_TURMA_OFERTA = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/PlanoEstudoSelecaoTurma/_PlanoEstudoTurmaOferta";
        public const string PLANO_ESTUDO_SELECAO_TURMA_OFERTA_PESQUISAR = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/PlanoEstudoSelecaoTurma/_PlanoEstudoTurmaOfertaPesquisar";
        public const string PLANO_ESTUDO_SELECAO_TURMA_OFERTA_ITEM = "SMC.Academico.UI.Mvc.dll/Areas/TUR/Views/PlanoEstudoSelecaoTurma/_PlanoEstudoTurmaOfertaItem";        
    }
}