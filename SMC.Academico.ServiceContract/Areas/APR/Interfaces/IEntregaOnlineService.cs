using SMC.Academico.ServiceContract.Areas.APR.Data.EntregaOnline;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.IO;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IEntregaOnlineService : ISMCService
    {
        AplicacaoAvaliacaoEntregaOnlineData BuscarEntregasOnline(long seqAplicacaoAvaliacao);

        FileInfo BuscarArquivosEntregasOnline(List<long> seqsEntregasOnline);

        void SalvarAvaliacao(AplicacaoAvaliacaoEntregaOnlineData entregaOnline);

        HistoricoSituacaoEntregaOnlineData BuscarHistoricoSituacaoEntrega(long seqEntregaOnline);

        /// <summary>
        /// Salvar entrega online aluno
        /// </summary>
        /// <param name="entregaOnline">Dados da entrega</param>
        /// <returns>Sequencial da entrega</returns>
        long SalvarEntregaOnline(EntregaOnlineData entregaOnline);

        /// <summary>
        /// Salvar a liberação da entrega para correção 
        /// </summary>
        /// <param name="seqEntregaOnline">Sequencial Entrega Online</param>
        void SalvarLiberarCorrecao(long seqEntregaOnline);

        /// <summary>
        /// Salvar solicitacao de liberação para nova entrega
        /// </summary>
        /// <param name="seqEntregaOnline">Sequencial de entrega online</param>
        /// <param name="observacao">Observação</param>
        void SalvarSolicitarLiberarNovaEntrega(long seqEntregaOnline, string observacao);

        /// <summary>
        /// Salva no histórico da entrega online um novo registro com a situação Entregue e a observação informada
        /// </summary>
        /// <param name="seqEntregaOnline">Sequencial da entrega online</param>
        /// <param name="observacao">Observação a ser salva</param>
        (long SeqAplicacaoAvaliacao, long SeqOrigemAvaliacao) SalvarLiberacaoNovaEntrega(long seqEntregaOnline, string observacao);

        /// <summary>
        /// Salva no histórico da entrega online um novo registro com a situação liberado para correção e a observação informada
        /// </summary>
        /// <param name="seqEntregaOnline">Sequencial da entrega online</param>
        /// <param name="observacao">Observação a ser salva</param>
        (long SeqAplicacaoAvaliacao, long SeqOrigemAvaliacao) SalvarNegacaoNovaEntrega(long seqEntregaOnline, string observacao);

    }
}