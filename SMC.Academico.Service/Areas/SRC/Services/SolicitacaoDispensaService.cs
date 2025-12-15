using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class SolicitacaoDispensaService : SMCServiceBase, ISolicitacaoDispensaService
    {
        #region [ DomainService ]

        private SolicitacaoDispensaGrupoDomainService SolicitacaoDispensaGrupoDomainService => Create<SolicitacaoDispensaGrupoDomainService>();

        private SolicitacaoDispensaDomainService SolicitacaoDispensaDomainService => Create<SolicitacaoDispensaDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os dados da solicitação de dispensa de acordo com a solicitação de serviço e a pessoa atuação
        /// Retorna inclusive a lista de ciclos letivos disponivel para seleção do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação (aluno que solicitou o serviço)</param>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Modelo para realizar o passo de escolha dos itens a serem dispensados</returns>
        public SolicitacaoDispensaItensDispensadosData PrepararModeloSolicitacaoDispensaItensDispensados(long seqPessoaAtuacao, long seqSolicitacaoServico)
        {
            var modelo = this.SolicitacaoDispensaDomainService.PrepararModeloSolicitacaoDispensaItensDispensados(seqPessoaAtuacao, seqSolicitacaoServico).Transform<SolicitacaoDispensaItensDispensadosData>();

            return modelo;
        }

        /// <summary>
        /// Grava os dados da solicitação de dispensa destino para a solicitação de dispensa informada
        /// </summary>
        /// <param name="modelo">Modelo da solicitação de dispensa com a solicitação de dispensa destino preenchida</param>
        public void SalvarSolicitacaoDispensaItensDispensados(SolicitacaoDispensaItensDispensadosData modelo)
        {
            this.SolicitacaoDispensaDomainService.SalvarSolicitacaoDispensaItensDispensados(modelo.Transform<SolicitacaoDispensaItensDispensadosVO>());
        }

        /// <summary>
        /// Busca os dados da solicitação de dispensa de acordo com a solicitação de serviço e a pessoa atuação
        /// Retorna inclusive a lista de ciclos letivos disponivel para seleção do aluno em caso de dispensa externa
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação (aluno que solicitou o serviço)</param>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Modelo para realizar o passo de escolha dos itens a serem dispensados</returns>
        public SolicitacaoDispensaItensCursadosData PrepararModeloSolicitacaoDispensaItensCursados(long seqPessoaAtuacao, long seqSolicitacaoServico)
        {
            return this.SolicitacaoDispensaDomainService.PrepararModeloSolicitacaoDispensaItensCursados(seqPessoaAtuacao, seqSolicitacaoServico).Transform<SolicitacaoDispensaItensCursadosData>();
        }

        /// <summary>
        /// Grava os dados da solicitação de dispensa origem interna e origem externa para a solicitação de dispensa informada
        /// </summary>
        /// <param name="modelo">Modelo da solicitação de dispensa com a solicitação de dispensa rigem interna e origem externa preenchidas</param>
        public void SalvarSolicitacaoDispensaItensCursados(SolicitacaoDispensaItensCursadosData modelo)
        {
            this.SolicitacaoDispensaDomainService.SalvarSolicitacaoDispensaItensCursados(modelo.Transform<SolicitacaoDispensaItensCursadosVO>());
        }

        /// <summary>
        /// Verifica se existe solicitação de dispensa para solicitação de serviço, se não existir cria um solicitação de dispensa apenas com o sequencial da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void CriarSolicitacaoDispensaPorSolicitacaoServico(long seqSolicitacaoServico)
        {
            SolicitacaoDispensaDomainService.CriarSolicitacaoDispensaPorSolicitacaoServico(seqSolicitacaoServico);
        }

        /// <summary>
        /// Retorna os dados de agrupamento de uma solicitação de dispensa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço de dispensa</param>
        /// <returns>Dados do agrupamento</returns>
        public AtendimentoDispensaAgrupamentoData BuscarDadosAgrupamentoAtendimentoDispensa(long seqSolicitacaoServico)
        {
            return SolicitacaoDispensaDomainService.BuscarDadosAgrupamentoAtendimentoDispensa(seqSolicitacaoServico).Transform<AtendimentoDispensaAgrupamentoData>();
        }

        public void SalvarAtendimentoDispensaAgrupamentoGrupo(SolicitacaoDispensaGrupoData dados)
        {
            SolicitacaoDispensaGrupoDomainService.SalvarAtendimentoDispensaAgrupamentoGrupo(dados.Transform<SolicitacaoDispensaGrupo>());
        }

        /// <summary>
        /// Busca os itens para o datasource dos grupos de dispensa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de dispensa</param>
        /// <param name="seqGrupo">Sequencial do grupo que está sendo editado. Informar null caso seja inclusão</param>
        /// <returns>Itens de datasource</returns>
        public AtendimentoDispensaAgrupamentoItensData BuscarItensAgrupamentoAtendimentoDispensa(long seqSolicitacaoServico, long? seqGrupo)
        {
            return SolicitacaoDispensaDomainService.BuscarItensAgrupamentoAtendimentoDispensa(seqSolicitacaoServico, seqGrupo).Transform<AtendimentoDispensaAgrupamentoItensData>();
        }

        /// <summary>
        /// Busca os dados de um grupo para edição
        /// </summary>
        /// <param name="seqGrupo">Sequencial do grupo a ser buscado</param>
        /// <returns>Dados do grupo recuperado</returns>
        public SolicitacaoDispensaGrupoData BuscarDadosGrupoAgrupamentoAtendimentoDispensa(long seqGrupo)
        {
            return SolicitacaoDispensaGrupoDomainService.SearchProjectionByKey(seqGrupo, x => new SolicitacaoDispensaGrupoData
            {
                Seq = seqGrupo,
                SeqDispensa = x.SeqDispensa,
                SeqSolicitacaoDispensa = x.SeqSolicitacaoDispensa,
                ModoExibicaoHistoricoEscolar = x.ModoExibicaoHistoricoEscolar,
                OrigensInternas = x.OrigensInternas.Select(o => new SolicitacaoDispensaGrupoOrigemData
                {
                    Seq = o.Seq,
                    SeqSolicitacaoDispensaGrupo = o.SeqSolicitacaoDispensaGrupo,
                    SeqSolicitacaoDispensaOrigem = o.SeqSolicitacaoDispensaOrigemInterna
                }).ToList(),
                OrigensExternas = x.OrigensExternas.Select(o => new SolicitacaoDispensaGrupoOrigemData
                {
                    Seq = o.Seq,
                    SeqSolicitacaoDispensaGrupo = o.SeqSolicitacaoDispensaGrupo,
                    SeqSolicitacaoDispensaOrigem = o.SeqSolicitacaoDispensaOrigemExterna
                }).ToList(),
                Destinos = x.Destinos.Select(o => new SolicitacaoDispensaGrupoDestinoData
                {
                    Seq = o.Seq,
                    SeqSolicitacaoDispensaGrupo = o.SeqSolicitacaoDispensaGrupo,
                    SeqSolicitacaoDispensaDestino = o.SeqSolicitacaoDispensaDestino
                }).ToList(),
            });
        }

        public void RemoverGrupoAgrupamentoAtendimentoDispensa(long seqGrupo)
        {
            SolicitacaoDispensaGrupoDomainService.DeleteEntity(seqGrupo);
        }

        /// <summary>
        /// RN_SRC_089 - Solicitação - Criação automática de grupos de itens de dispensa
        /// Verifica se existe apenas 1 item cursado (interno ou externo) e também só 1 item a ser dispensado.
        /// Nesse caso, já cria o agrupamento, verificando também se esses itens formam uma equivalência.
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void VerificarAgrupamentoAutomatico(long seqSolicitacaoServico)
        {
            SolicitacaoDispensaGrupoDomainService.VerificarAgrupamentoAutomatico(seqSolicitacaoServico);
        }

        /// <summary>
        /// Faz as validações para prosseguir com a criação de grupos para o atendimento de dispensa individual
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void SalvarAtendimentoDispensaAgrupamento(long seqSolicitacaoServico)
        {
            SolicitacaoDispensaDomainService.SalvarAtendimentoDispensaAgrupamento(seqSolicitacaoServico);
        }

        /// <summary>
        /// Verifica se é uma solicitação de dispensa pelo token e se na dispensa destino possui a flag item_excluido_plano
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns></returns>
        public bool VerificarReabrirDispensaItemPlano(long seqSolicitacaoServico)
        {
            return SolicitacaoDispensaDomainService.VerificarReabrirDispensaItemPlano(seqSolicitacaoServico);
        }

        /// <summary>
        /// Verificar se existe item no plano de estudo e sem historico lançado
        /// </summary>
        /// <param name="seqSolicitacaoDispensa">Sequencial da Solicitação de Serviço de Dispensa</param>
        /// <returns>Lista de Plano de Estudo Item sem histórico lançado</returns>
        public List<PlanoEstudoItemSemHistoricoData> VerificarTurmasPlanoEstudoSemHistorico(long seqSolicitacaoDispensa)
        {
            // Busca os dados da solicitação de serviço 
            var spec = new SolicitacaoDispensaFilterSpecification() { Seq = seqSolicitacaoDispensa };
            var dados = SolicitacaoDispensaDomainService.SearchProjectionByKey(spec, s => new
            {
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                Destinos = s.Destinos.Select(d => new HistoricoEscolarAprovadoFiltroVO()
                { 
                    SeqComponenteCurricular = d.SeqComponenteCurricular,
                    SeqComponenteCurricularAssunto = d.SeqComponenteCurricularAssunto
                }).ToList()
            });

            // Realiza a validação e retorna
            return SolicitacaoServicoDomainService.VerificarTurmasPlanoEstudoSemHistorico(dados.SeqPessoaAtuacao, dados.Destinos).TransformList<PlanoEstudoItemSemHistoricoData>();
        }
    }
}