using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Specification;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoHistoricoNavegacaoDomainService : AcademicoContextDomain<SolicitacaoHistoricoNavegacao>
    {
        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService
        {
            get { return this.Create<SolicitacaoServicoEtapaDomainService>(); }
        }

        /// <summary>
        /// Busca um histórico de uma navegação de solicitação
        /// </summary>
        /// <param name="seqSolicitacaoServicoEtapa">Sequencial da SolicitaçãoServicoEtapa</param>
        /// <param name="seqConfiguracaoEtapaPagina">Sequencial da ConfiguracaoEtapaPagina</param>
        /// <param name="inserir">Indicador para informar se deve inserir um registro novo no histórico caso não encontre o mesmo</param>
        /// <returns>Histórico encontrado ou null</returns>
        public SolicitacaoHistoricoNavegacao BuscarSolicitacaoHistoricoNavegacao(long seqSolicitacaoServicoEtapa, long seqConfiguracaoEtapaPagina, bool inserir)
        {
            SolicitacaoHistoricoNavegacao historico = null;

            // Busca todos os históricos da SolicitacaoServicoEtapa
            var solicitacaoServicoEtapa = SolicitacaoServicoEtapaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoServicoEtapa>(seqSolicitacaoServicoEtapa), x => x.HistoricosNavegacao);

            // Tem histórico?
            if (solicitacaoServicoEtapa.HistoricosNavegacao.Any())
            {
                // Procura se o último histórico por data de entrada é o histórico que estou solicitando
                var ultimoHistorico = solicitacaoServicoEtapa.HistoricosNavegacao.Where(h => h.DataSaida == null || h.DataSaida == default(DateTime)).OrderBy(h => h.DataEntrada).LastOrDefault();
                if (ultimoHistorico?.SeqConfiguracaoEtapaPagina == seqConfiguracaoEtapaPagina)
                    historico = ultimoHistorico;
            }

            // Caso seja marcado para inserir e o último histórico não seja o do sequencial que procuro, insere e retorna
            if (historico == null && inserir)
            {
                historico = new SolicitacaoHistoricoNavegacao
                {
                    DataEntrada = DateTime.Now,
                    SeqConfiguracaoEtapaPagina = seqConfiguracaoEtapaPagina,
                    SeqSolicitacaoServicoEtapa = seqSolicitacaoServicoEtapa
                };

                this.SaveEntity(historico);
            }

            // Retorna o histórico ou null
            return historico;
        }

        /// <summary>
        /// Atualiza a data de saída de um determinado item de histórico
        /// </summary>
        /// <param name="seqSolicitacaoHistoricoNavegacao"></param>
        public void AtualizarDataSaidaSolicitacaoHistoricoNavegacao(long seqSolicitacaoHistoricoNavegacao)
        {
            //var historico = this.SearchByKey(new SMCSeqSpecification<SolicitacaoHistoricoNavegacao>(seqSolicitacaoHistoricoNavegacao));
            //historico.DataSaida = DateTime.Now;
            //this.UpdateEntity(historico);

            var historico = new SolicitacaoHistoricoNavegacao() { Seq = seqSolicitacaoHistoricoNavegacao, DataSaida = DateTime.Now };
            this.UpdateFields(historico, x => x.DataSaida);
        }

        public bool ValidarSeExisteSolicitacaoHistoricoPorConfiguracaoEtapaPagina(long seqConfiguracaoEtapaPagina)
        {
            var filtro = new SolicitacaoHistoricoNavegacaoFilterSpecification()
            {
                SeqConfiguracaoEtapaPagina = seqConfiguracaoEtapaPagina
            };

            var registros = this.SearchBySpecification(filtro).ToList();

            return registros.Any();
        }
    }
}