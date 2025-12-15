using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class ChamadaService : SMCServiceBase, IChamadaService
    {
        #region Domain Services

        public ChamadaDomainService ChamadaDomainService
        {
            get { return Create<ChamadaDomainService>(); }
        }

        #endregion Domain Services

        #region [ Services ]

        private IIntegracaoService IntegracaoService
        {
            get { return Create<IIntegracaoService>(); }
        }

        #endregion [ Services ]

        public void CargaIngressantes(CargaIngressanteSATData data)
        {
            try
            {
                ChamadaDomainService.AtualizaStatusProcessamento(data.SeqChamada, true);
                ChamadaDomainService.CargaIngressantes(data.Transform<CargaIngressanteSATVO>());
            }
            finally
            {
                ChamadaDomainService.AtualizaStatusProcessamento(data.SeqChamada, false);
            }
        }

        /// <summary>
        /// Busca as chamadas por tipo de chamada
        /// </summary>
        /// <param name="seqCampanha">Seq da campanha</param>
        /// <param name="seqConvocacao">Seq da convocação</param>
        /// <param name="seqTipoChamada">Seq do tipo da chamada</param>
        /// <returns>Cabeçalho com os dados da campanha</returns>
        public List<SMCDatasourceItem> BuscarChamadasPorCampanhaConvocacaoTipoChamadaSelect(long seqCampanha, long? seqConvocacao = null, TipoChamada? tipoChamada = null)
        {
            var spec = new ChamadaFilterSpecification() { SeqCampanha = seqCampanha, SeqConvocacao = seqConvocacao, TipoChamada = tipoChamada };

            var result = this.ChamadaDomainService.SearchProjectionBySpecification(spec, c => new SMCDatasourceItem()
            {
                Seq = c.Seq,
                Descricao = c.Numero + "ª - " + c.TipoChamada
            });

            return result.ToList();
        }

        public void AtualizarSeqAgendamento(long seqChamada, long seqAgendamento)
        {
            ChamadaDomainService.AtualizarSeqAgendamento(seqChamada, seqAgendamento);
        }

        public ChamadaConvocacaoData BuscarChamadaParaConvocacao(long seqChamada)
        {
            return ChamadaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Chamada>(seqChamada),
                                                            x => new ChamadaConvocacaoData
                                                            {
                                                                SeqAgendamento = x.SeqAgendamentoSat,
                                                                Campanha = x.Convocacao.CampanhaCicloLetivo.Campanha.Descricao,
                                                                ProcessoSeletivo = x.Convocacao.ProcessoSeletivo.Descricao,
                                                                NumeroChamada = x.Numero,
                                                                TipoChamada = x.TipoChamada,
                                                                SituacaoChamada = x.SituacaoChamada,
                                                                Convocacao = x.Convocacao.Descricao,
                                                                DescricaoCicloLetivo = x.Convocacao.CampanhaCicloLetivo.CicloLetivo.Descricao
                                                            });
        }

        public bool ExisteInscricoesConvocadas(long seqChamada)
        {
            return IntegracaoService.ExisteInscricoesConvocadas(ChamadaDomainService.BuscarSeqsOfertasGPIPorSeqChamada(seqChamada));
        }

        /// <summary>
        /// Encerra a chamada informada
        /// </summary>
        /// <param name="seqChamada">Sequencial da chamada</param>
        public void EncerrarChamada(long seqChamada)
        {
            ChamadaDomainService.EncerrarChamada(seqChamada);
        }
    }
}