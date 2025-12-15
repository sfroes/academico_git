using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.MAT.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ISolicitacaoMatriculaItemService : ISMCService
    {
        /// <summary>
        /// Adiciona os itens selecionado na tela de matricula ao registro de solicitacao de matricula
        /// </summary>
        /// <param name="turmasSelecionadas">Turmas selecionadas no processo de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <returns>Mensagem de erro da validação de vagas e requisitos</returns>
        string AdicionarSolicitacaoMatriculaTurmasItens(List<SolicitacaoMatriculaItemData> turmasSelecionadas, long seqProcessoEtapa);

        /// <summary>
        /// Alterar os itens de solicitação de matricula de acordo com a turma selecionada para edição
        /// </summary>
        /// <param name="itens">Itens de solicitação de matricula</param>
        /// <param name="seqProcessoEtapa">Controle de vagas de acordo com Processo Etapa</param>
        void AlterarSolicitacaoMatriculaTurmasItens(List<SolicitacaoMatriculaItemData> itens, long seqProcessoEtapa);

        /// <summary>
        /// Lista os itens de turmas selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial processo etapa</param>
        /// <param name="habilitar">Habilitar os botões de editar e excluir de cada registro</param>
        /// <returns>Lista de turmas gravadas pelo ingressante</returns>
        SMCPagerData<TurmaMatriculaListarData> BuscarSolicitacaoMatriculaTurmasItens(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqTurma, long? seqProcessoEtapa, bool habilitar, bool selecaoTurma = false, bool listagemTurmas = false);

        /// <summary>
        /// Busca os sequencial de acordo com os filtros informados
        /// </summary>
        /// <param name="filtro">Filtro </param>
        /// <returns>Sequenciais da solicitação matrícula item</returns>
        List<long?> BuscarSequenciaisSelecaoTurmaSolicitacoesMatriculaItem(long seqSolicitacaoMatricula);

        /// <summary>
        /// Verifica se existe alguma turma ou atividade já cadastrada
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma ou atividade selecionada no processo de matricula</returns>
        bool VerificarTurmasAtividadesCadastradas(long seqSolicitacaoMatricula);

        /// <summary>
        /// Verifica se existe alguma turma ou atividade não alterada e pelo menos uma cancelada
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma ou atividade selecionada no processo de matricula</returns>
        bool VerificarTurmasAtividadesCanceladasPlano(long seqSolicitacaoMatricula);

        /// <summary>
        /// Verifica se existe alguma turma cancelada para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma cancelada na solicitação de matricula</returns>
        bool VerificarSolicitacaoMatriculaTurmasCancelada(long? seqSolicitacaoMatricula);

        /// <summary>
        /// Verifica se existe alguma turma cancelada para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir item cancelado na solicitação de matricula</returns>
        bool VerificarSolicitacaoMatriculaItemCancelado(long? seqSolicitacaoMatricula);

        /// <summary>
        /// Verifica se existe alguma turma ou atividade academica cancelada pelo solicitante para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir item cancelado na solicitação de matricula</returns>
        bool VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(long? seqSolicitacaoMatricula);

        /// <summary>
        /// Remove os itens de turmas selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        void RemoverSolicitacaoMatriculaItemPorTurma(long seqSolicitacaoMatricula, long seqTurma, long seqProcessoEtapa);

        /// <summary>
        /// Insere novamente os itens de turmas selecionado na tela de matricula como não editado
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="descricao">Descrição da turma para caso de erro na ocupação de vaga</param>
        string DesfazerRemoverSolicitacaoMatriculaItemPorTurma(long seqSolicitacaoMatricula, long seqTurma, long seqProcessoEtapa, string descricao);

        /// <summary>
        /// Remove o item de atividade selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="motivo">motivo do cancelamento do item de matrícula</param>
        void AlterarSolicitacaoMatriculaItemParaCancelado(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa, MotivoSituacaoMatricula? motivo);

        /// <summary>
        /// Insere novamente o item de atividade selecionado na tela de matricula como não editado
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        void DesfazerRemoverSolicitacaoMatriculaItemPorAtividade(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa);

        /// <summary>
        /// Busca turma e atividades de acordo com solicitação de matricula, ingressante e etapa
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="erro">Exibir apenas com situação cancelado ou finalizado sem sucesso</param>
        /// <param name="desconsiderarEtapa">Desconsidera a etapa para recuperar a informação de histórico do item</param>
        /// <param name="desativarFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta de solicitações de serviço </param>
        /// <returns>Objeto com as turmas e atividade</returns>
        SolicitacaoMatriculaTurmaAtividadeData BuscarSolicitacaoMatriculaTurmasAtividades(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, bool erro, bool desconsiderarEtapa, bool desativarFiltroDados = false);

        /// <summary>
        /// Lista os itens de atividades selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa</param>
        /// <param name="classificacaoFinal">Classificação do registro na tabela de histórico</param>
        /// <returns>Lista de sequenciais de atividades gravados pelo ingressante</returns>
        List<SolicitacaoMatriculaItemData> BuscarSolicitacaoMatriculaAtividadesItens(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqProcessoEtapa, ClassificacaoSituacaoFinal? classificacaoFinal);

        /// <summary>
        /// Valida os pré e có-requisitos independente se é turma ou atividade
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="validaTipoGestao">Valida se os componentes tem o mesmo tipo de gestão ex turma com pré/có-requisito atividade</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        (bool valido, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemPreCoRequisito(long seqSolicitacaoMatricula, long seqPessoaAtuacao, TipoGestaoDivisaoComponente? validaTipoGestao = null);

        /// <summary>
        /// Valida có-requisitos independente se é turma ou atividade
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="validaTipoGestao">Valida se os componentes tem o mesmo tipo de gestão ex turma com pré/có-requisito atividade</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        (bool valido, TipoRequisito tipo, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemCoRequisito(long seqSolicitacaoMatricula, long seqPessoaAtuacao, TipoGestaoDivisaoComponente? validaTipoGestao = null);

        /// <summary>
        /// Valida se existe duas turmas de mesmo componente curricular selecionada
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        (bool valido, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemTurmaDuplicada(long seqSolicitacaoMatricula);

        /// <summary>
        /// Recupera os planos de estudo item do aluno e grava na solicitação matricula item, com histórico de "Não Alterado"
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        void ConverterPlanoEstudoItemEmSolicitacaoMatriculaItem(long seqSolicitacaoMatricula, long seqProcessoEtapa, long seqPessoaAtuacao);

        /// <summary>
        /// Verifica se existe algum item da solicitação com situação mais atual da primeira etapa com classificação diferente da situação mais atual da última etapa.
        /// ou se existe algum item da solicitação que não possui situação da última etapa e a situação mais atual da primeira etapa deste item for diferente de “Cancelada”.
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existe alguma das situações descritas</returns>
        bool VerificarSolicitacaoMatriculaItemRegistroDomumentoVia(long seqSolicitacaoMatricula, long seqPessoaAtuacao);

        /// <summary>
        /// Verifica se nas turmas selecionadas, alguma já foi cursada ou dispensada segundo a NV11 do UC_MAT_003_25 
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>String contendo as turmas que já foram cursadas ou dispensadas, separadas por vírgula. Empty caso não tenha nenhuma</returns>
        string VerificarTurmasAprovadasDispensadasSelecaoTurma(long seqSolicitacaoServico);
               
        /// <summary>
        /// Lista os itens da solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <returns>Lista de itens da solicitacao de matricula</returns>
        List<SolicitacaoMatriculaItemData> BuscarSolicitacaoMatriculaItensPlano(long seqSolicitacaoMatricula);


        /// <summary>
        /// Implementação da RN_MAT_151
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Solicitação de matrícula a ser verificada</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação da solicitação de serviço</param>
        /// <param name="seqProcessoEtapa">Processo etapa para considerar na hora de verificar a situação dos itens</param>
        /// <param name="seqsNovasDivisoesTurma">Novas divisões de turmas que estão sendo incluidas na solicitação de matrícula</param>
        /// <returns>Mensagem de erro no caso de falha da regra ou "" em caso de sucesso</returns>
        string ValidarTurmasDuplicadas(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long seqProcessoEtapa, List<long> seqsNovasDivisoesTurma);

        string ValidarVagaTurmaAtividadeIngressante(List<SolicitacaoMatriculaItemData> solicitacaoMatriculaItem, long seqPessoaAtuacao);

        bool VerificaPertenceAoPlano(long seqSolicitacaoMatricula, long seqDivisaoTurma);
    }
}