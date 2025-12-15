using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data.EntregaOnline;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.IO;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class EntregaOnlineService : SMCServiceBase, IEntregaOnlineService
    {
        #region DomainServices

        public EntregaOnlineDomainService EntregaOnlineDomainService => Create<EntregaOnlineDomainService>();

        #endregion DomainServices

        public AplicacaoAvaliacaoEntregaOnlineData BuscarEntregasOnline(long seqAplicacaoAvaliacao)
        {
            return EntregaOnlineDomainService.BuscarEntregasOnline(seqAplicacaoAvaliacao).Transform<AplicacaoAvaliacaoEntregaOnlineData>();
        }

        public FileInfo BuscarArquivosEntregasOnline(List<long> seqsEntregasOnline)
        {
            // Recupera os arquivos que podem ser baixados dos sequenciais de entregas informados
            return EntregaOnlineDomainService.BuscarArquivosEntregasOnline(seqsEntregasOnline);
        }

        public void SalvarAvaliacao(AplicacaoAvaliacaoEntregaOnlineData entregaOnline)
        {
            EntregaOnlineDomainService.SalvarAvaliacao(entregaOnline.Transform<AplicacaoAvaliacaoEntregaOnlineVO>());
        }

        public HistoricoSituacaoEntregaOnlineData BuscarHistoricoSituacaoEntrega(long seqEntregaOnline)
        {
            return EntregaOnlineDomainService.BuscarHistoricoSituacaoEntrega(seqEntregaOnline).Transform<HistoricoSituacaoEntregaOnlineData>();
        }

        /// <summary>
        /// Salvar entrega online aluno
        /// </summary>
        /// <param name="entregaOnline">Dados da entrega</param>
        /// <returns>Sequencial da entrega</returns>
        public long SalvarEntregaOnline(EntregaOnlineData entregaOnline)
        {
            return EntregaOnlineDomainService.SalvarEntregaOnline(entregaOnline.Transform<EntregaOnlineVO>());
        }

        /// <summary>
        /// Salvar a liberação da entrega para correção
        /// </summary>
        /// <param name="seqEntregaOnline">Sequencial Entrega Online</param>
        public void SalvarLiberarCorrecao(long seqEntregaOnline)
        {
            EntregaOnlineDomainService.SalvarLiberarCorrecao(seqEntregaOnline);
        }

        /// <summary>
        /// Salvar solicitacao de liberação para nova entrega
        /// </summary>
        /// <param name="seqEntregaOnline">Sequencial de entrega online</param>
        /// <param name="observacao">Observação</param>
        public void SalvarSolicitarLiberarNovaEntrega(long seqEntregaOnline, string observacao)
        {
            EntregaOnlineDomainService.SalvarSolicitarLiberarNovaEntrega(seqEntregaOnline, observacao);
        }

        public (long SeqAplicacaoAvaliacao, long SeqOrigemAvaliacao) SalvarLiberacaoNovaEntrega(long seqEntregaOnline, string observacao)
        {
            return EntregaOnlineDomainService.SalvarLiberacaoNovaEntrega(seqEntregaOnline, observacao);
        }

        public (long SeqAplicacaoAvaliacao, long SeqOrigemAvaliacao) SalvarNegacaoNovaEntrega(long seqEntregaOnline, string observacao)
        {
            return EntregaOnlineDomainService.SalvarNegacaoNovaEntrega(seqEntregaOnline, observacao);
        }
    }
}