using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface IChamadaService : ISMCService
    {
        /// <summary>
        /// Carrega os ingressantes baseados na seleção do GPI.
        /// </summary>
        void CargaIngressantes(CargaIngressanteSATData data);

        List<SMCDatasourceItem> BuscarChamadasPorCampanhaConvocacaoTipoChamadaSelect(long seqCampanha, long? seqConvocacao = null, TipoChamada? seqTipoChamada = null);

        void AtualizarSeqAgendamento(long seqChamada, long seqAgendamento);

        ChamadaConvocacaoData BuscarChamadaParaConvocacao(long seqChamada);

        bool ExisteInscricoesConvocadas(long seqChamada);

        /// <summary>
        /// Encerra a chamada informada
        /// </summary>
        /// <param name="seqChamada">Sequencial da chamada</param>
        void EncerrarChamada(long seqChamada);
    }
}