using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.MAT.Services
{
    public class SolicitacaoMatriculaItemService : SMCServiceBase, ISolicitacaoMatriculaItemService
    {
        #region [ DomainService ]

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService
        {
            get { return this.Create<SolicitacaoMatriculaItemDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Adiciona os itens selecionado na tela de matricula ao registro de solicitacao de matricula
        /// </summary>
        /// <param name="turmasSelecionadas">Turmas selecionadas no processo de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <returns>Mensagem de erro da validação de vagas e requisitos</returns>
        public string AdicionarSolicitacaoMatriculaTurmasItens(List<SolicitacaoMatriculaItemData> turmasSelecionadas, long seqProcessoEtapa)
        {
            return this.SolicitacaoMatriculaItemDomainService.AdicionarSolicitacaoMatriculaTurmasItens(turmasSelecionadas.TransformList<SolicitacaoMatriculaItemVO>(), seqProcessoEtapa);
        }

        /// <summary>
        /// Alterar os itens de solicitação de matricula de acordo com a turma selecionada para edição
        /// </summary>
        /// <param name="itens">Itens de solicitação de matricula</param>
        /// <param name="seqProcessoEtapa">Controle de vagas de acordo com Processo Etapa</param>
        public void AlterarSolicitacaoMatriculaTurmasItens(List<SolicitacaoMatriculaItemData> itens, long seqProcessoEtapa)
        {
            this.SolicitacaoMatriculaItemDomainService.AlterarSolicitacaoMatriculaTurmasItens(itens.TransformList<SolicitacaoMatriculaItemVO>(), seqProcessoEtapa);
        }

        /// <summary>
        /// Lista os itens de turmas selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial processo etapa</param>
        /// <param name="habilitar">Habilitar os botões de editar e excluir de cada registro</param>
        /// <returns>Lista de turmas gravadas pelo ingressante</returns>
        public SMCPagerData<TurmaMatriculaListarData> BuscarSolicitacaoMatriculaTurmasItens(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqTurma, long? seqProcessoEtapa, bool habilitar, bool selecaoTurma = false, bool listagemTurmas = false)
        {
            var turmas = this.SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaTurmasItens(seqSolicitacaoMatricula, seqPessoaAtuacao, seqTurma, seqProcessoEtapa, habilitar, selecaoTurma, listagemTurmas);

            return turmas.Transform<SMCPagerData<TurmaMatriculaListarData>>();
        }

        /// <summary>
        /// Busca os sequencial de acordo com os filtros informados
        /// </summary>
        /// <param name="filtro">Filtro </param>
        /// <returns>Sequenciais da solicitação matrícula item</returns>
        public List<long?> BuscarSequenciaisSelecaoTurmaSolicitacoesMatriculaItem(long seqSolicitacaoMatricula)
        {
            return this.SolicitacaoMatriculaItemDomainService.BuscarSequenciaisSelecaoTurmaSolicitacoesMatriculaItem(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Verifica se existe alguma turma ou atividade já cadastrada
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma ou atividade selecionada no processo de matricula</returns>
        public bool VerificarTurmasAtividadesCadastradas(long seqSolicitacaoMatricula)
        {
            return this.SolicitacaoMatriculaItemDomainService.VerificarTurmasAtividadesCadastradas(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Verifica se existe alguma turma ou atividade não alterada e pelo menos uma cancelada
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma ou atividade selecionada no processo de matricula</returns>
        public bool VerificarTurmasAtividadesCanceladasPlano(long seqSolicitacaoMatricula)
        {
            return this.SolicitacaoMatriculaItemDomainService.VerificarTurmasAtividadesCanceladasPlano(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Verifica se existe alguma turma cancelada para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir turma cancelada na solicitação de matricula</returns>
        public bool VerificarSolicitacaoMatriculaTurmasCancelada(long? seqSolicitacaoMatricula)
        {
            return this.SolicitacaoMatriculaItemDomainService.VerificarSolicitacaoMatriculaTurmasCancelada(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Verifica se existe alguma turma cancelada para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir item cancelado na solicitação de matricula</returns>
        public bool VerificarSolicitacaoMatriculaItemCancelado(long? seqSolicitacaoMatricula)
        {
            return this.SolicitacaoMatriculaItemDomainService.VerificarSolicitacaoMatriculaItemCancelado(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Verifica se existe alguma turma ou atividade academica cancelada pelo solicitante para a solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existir item cancelado na solicitação de matricula</returns>
        public bool VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(long? seqSolicitacaoMatricula)
        {
            return this.SolicitacaoMatriculaItemDomainService.VerificarSolicitacaoMatriculaItemCanceladoPorSolicitante(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Remove os itens de turmas selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        public void RemoverSolicitacaoMatriculaItemPorTurma(long seqSolicitacaoMatricula, long seqTurma, long seqProcessoEtapa)
        {
            this.SolicitacaoMatriculaItemDomainService.RemoverSolicitacaoMatriculaItemPorTurma(seqSolicitacaoMatricula, seqTurma, seqProcessoEtapa);
        }

        /// <summary>
        /// Insere novamente os itens de turmas selecionado na tela de matricula como não editado
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="descricao">Descrição da turma para caso de erro na ocupação de vaga</param>
        public string DesfazerRemoverSolicitacaoMatriculaItemPorTurma(long seqSolicitacaoMatricula, long seqTurma, long seqProcessoEtapa, string descricao)
        {
            return this.SolicitacaoMatriculaItemDomainService.DesfazerRemoverSolicitacaoMatriculaItemPorTurma(seqSolicitacaoMatricula, seqTurma, seqProcessoEtapa, descricao);
        }

        /// <summary>
        /// Remove o item de atividade selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="motivo">Motivo do cancelamento do item de matrícula</param>
        public void AlterarSolicitacaoMatriculaItemParaCancelado(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa, MotivoSituacaoMatricula? motivo)
        {
            this.SolicitacaoMatriculaItemDomainService.AlterarSolicitacaoMatriculaItemParaCancelado(seqSolicitacaoMatriculaItem, seqProcessoEtapa, motivo);
        }

        /// <summary>
        /// Insere novamente o item de atividade selecionado na tela de matricula como não editado
        /// </summary>
        /// <param name="seqSolicitacaoMatriculaItem">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        public void DesfazerRemoverSolicitacaoMatriculaItemPorAtividade(long seqSolicitacaoMatriculaItem, long seqProcessoEtapa)
        {
            this.SolicitacaoMatriculaItemDomainService.DesfazerRemoverSolicitacaoMatriculaItemPorAtividade(seqSolicitacaoMatriculaItem, seqProcessoEtapa);
        }

        /// <summary>
        /// Busca turma e atividades de acordo com solicitação de matricula, ingressante e etapa
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="erro">Exibir apenas com situação cancelado ou finalizado sem sucesso</param>
        /// <param name="desconsiderarEtapa">Desconsidera a etapa para recuperar a informação de histórico do item</param>
        /// <param name="desativarFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta de solicitações de serviço </param>
        /// <returns>Objeto com as turmas e atividade</returns>
        public SolicitacaoMatriculaTurmaAtividadeData BuscarSolicitacaoMatriculaTurmasAtividades(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, bool erro, bool desconsiderarEtapa, bool desativarFiltroDados = false)
        {
            var registro = SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaTurmasAtividades(seqSolicitacaoMatricula, seqConfiguracaoEtapa, erro, desconsiderarEtapa, desativarFiltroDados);
            return registro.Transform<SolicitacaoMatriculaTurmaAtividadeData>();
        }

        /// <summary>
        /// Lista os itens de atividades selecionado na tela de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa</param>
        /// <param name="classificacaoFinal">Classificação do registro na tabela de histórico</param>
        /// <returns>Lista de sequenciais de atividades gravados pelo ingressante</returns>
        public List<SolicitacaoMatriculaItemData> BuscarSolicitacaoMatriculaAtividadesItens(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long? seqProcessoEtapa, ClassificacaoSituacaoFinal? classificacaoFinal)
        {
            var registro = SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaAtividadesItens(seqSolicitacaoMatricula, seqPessoaAtuacao, seqProcessoEtapa, classificacaoFinal);
            return registro.TransformList<SolicitacaoMatriculaItemData>();
        }

        /// <summary>
        /// Valida os pré e co-requisitos independente se é turma ou atividade
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="validaTipoGestao">Valida se os componentes tem o mesmo tipo de gestão ex turma com pré/có-requisito atividade</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        public (bool valido, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemPreCoRequisito(long seqSolicitacaoMatricula, long seqPessoaAtuacao, TipoGestaoDivisaoComponente? validaTipoGestao = null)
        {
            return SolicitacaoMatriculaItemDomainService.ValidaSolicitacaoMatriculaItemPreCoRequisito(seqSolicitacaoMatricula, seqPessoaAtuacao, validaTipoGestao);
        }

        /// <summary>
        /// Valida os co-requisitos independente se é turma ou atividade
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="validaTipoGestao">Valida se os componentes tem o mesmo tipo de gestão ex turma com pré/có-requisito atividade</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        public (bool valido, TipoRequisito tipo, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemCoRequisito(long seqSolicitacaoMatricula, long seqPessoaAtuacao, TipoGestaoDivisaoComponente? validaTipoGestao = null)
        {
            return SolicitacaoMatriculaItemDomainService.ValidaSolicitacaoMatriculaItemCoRequisito(seqSolicitacaoMatricula, seqPessoaAtuacao, validaTipoGestao);
        }

        /// <summary>
        /// Valida se existe duas turmas de mesmo componente curricular selecionada
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <returns>Valido = true quando não tem nada impeditivo para cadastro do componente</returns>
        public (bool valido, List<string> mensagemErro) ValidaSolicitacaoMatriculaItemTurmaDuplicada(long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaItemDomainService.ValidaSolicitacaoMatriculaItemTurmaDuplicada(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Recupera os planos de estudo item do aluno e grava na solicitação matricula item, com histórico de "Não Alterado"
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa para gravar o histórico do item</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        public void ConverterPlanoEstudoItemEmSolicitacaoMatriculaItem(long seqSolicitacaoMatricula, long seqProcessoEtapa, long seqPessoaAtuacao)
        {
            SolicitacaoMatriculaItemDomainService.ConverterPlanoEstudoItemEmSolicitacaoMatriculaItem(seqSolicitacaoMatricula, seqProcessoEtapa, seqPessoaAtuacao);
        }

        /// <summary>
        /// Verifica se existe algum item da solicitação com situação mais atual da primeira etapa com classificação diferente da situação mais atual da última etapa.
        /// ou se existe algum item da solicitação que não possui situação da última etapa e a situação mais atual da primeira etapa deste item for diferente de “Cancelada”.
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Retorna true se existe alguma das situações descritas</returns>
        public bool VerificarSolicitacaoMatriculaItemRegistroDomumentoVia(long seqSolicitacaoMatricula, long seqPessoaAtuacao)
        {
            return SolicitacaoMatriculaItemDomainService.VerificarSolicitacaoMatriculaItemRegistroDomumentoVia(seqSolicitacaoMatricula, seqPessoaAtuacao);
        }

        public string VerificarTurmasAprovadasDispensadasSelecaoTurma(long seqSolicitacaoServico)
        {
            return SolicitacaoMatriculaItemDomainService.VerificarTurmasAprovadasDispensadasSelecaoTurma(seqSolicitacaoServico);
        }

        /// <summary>
        /// Lista os itens da solicitação de matricula
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitacao de matricula</param>
        /// <returns>Lista de itens da solicitacao de matricula</returns>
        public List<SolicitacaoMatriculaItemData> BuscarSolicitacaoMatriculaItensPlano(long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaItensPlano(seqSolicitacaoMatricula).TransformList<SolicitacaoMatriculaItemData>();
        }


        /// <summary>
        /// Implementação da RN_MAT_151
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Solicitação de matrícula a ser verificada</param>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação da solicitação de serviço</param>
        /// <param name="seqProcessoEtapa">Processo etapa para considerar na hora de verificar a situação dos itens</param>
        /// <param name="seqsNovasDivisoesTurma">Novas divisões de turmas que estão sendo incluidas na solicitação de matrícula</param>
        /// <returns>Mensagem de erro no caso de falha da regra ou "" em caso de sucesso</returns>
        public string ValidarTurmasDuplicadas(long seqSolicitacaoMatricula, long seqPessoaAtuacao, long seqProcessoEtapa, List<long> seqsNovasDivisoesTurma)
        {
            return SolicitacaoMatriculaItemDomainService.ValidarTurmasDuplicadas(seqSolicitacaoMatricula, seqPessoaAtuacao, seqProcessoEtapa, seqsNovasDivisoesTurma, null);
        }

        public string ValidarVagaTurmaAtividadeIngressante(List<SolicitacaoMatriculaItemData> solicitacaoMatriculaItem, long seqPessoaAtuacao)
        {
            string listaErro = string.Empty;
            foreach (var item in solicitacaoMatriculaItem)
            {
                var erro = SolicitacaoMatriculaItemDomainService.ValidarVagaTurmaAtividadeIngressante(seqPessoaAtuacao, item.DescricaoFormatada, item.SeqDivisaoTurma);
                if (!string.IsNullOrEmpty(erro))
                    listaErro += $"{erro} </br>";
            }

            return listaErro;
        }

        public bool VerificaPertenceAoPlano(long seqSolicitacaoMatricula, long seqDivisaoTurma)
        {
            return SolicitacaoMatriculaItemDomainService.VerificaPertenceAoPlano(seqSolicitacaoMatricula, seqDivisaoTurma);
        }
    }
}