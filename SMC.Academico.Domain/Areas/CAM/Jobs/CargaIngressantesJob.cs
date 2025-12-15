using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Logging;
using SMC.Framework.Specification;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class CargaIngressantesJob : SMCWebJobBase<CargaIngressanteSATVO, PessoaIntegracaoData>
    {
        #region [ DomainServices ]

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();
        private ChamadaDomainService ChamadaDomainService => Create<ChamadaDomainService>();

        #endregion [ DomainServices ]

        #region [ Services ]

        private IIntegracaoService IntegracaoService => Create<IIntegracaoService>();

        #endregion

        #region [ API ]

        public ISMCApiClient CargaAPI => CreateApiClient("CargaIngressante");

        #endregion [ API ]

        #region [ Construtor ]

        public CargaIngressantesJob()
        {
            UnhandledErrorMesage = "Um erro impediu a execução da importação: {0}";
        }

        #endregion [ Construtor ]

        #region [ Campos ]

        public double ProcessadosComSucesso { get; set; }

        private bool _cargaIndividual = false;

        #endregion [ Campos ]

        #region [ Métodos principais ]

        /// <summary>
        /// Recupera no GPI os ingressantes a serem importados para o SGA
        /// </summary>
        /// <param name="filter">Dados do filtro</param>
        /// <returns>Lista de ingressantes a serem importados</returns>
        public override ICollection<PessoaIntegracaoData> GetItems(CargaIngressanteSATVO filter)
        {
            var seqsOfertasGPI = ChamadaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Chamada>(filter.SeqChamada),
                                    x => x.Convocacao.ProcessoSeletivo.Ofertas.Where(f => f.SeqHierarquiaOfertaGpi.HasValue)
                                                                                .Select(f => f.SeqHierarquiaOfertaGpi.Value));

            // Recupera os convocados do GPI
            var lote = IntegracaoService.BuscarDadosInscricoes(seqsOfertasGPI.ToList());

            Scheduler.LogSucess($"Candidatos encontrados: {lote.Count}");

            _cargaIndividual = Convert.ToBoolean(ConfigurationManager.AppSettings["CargaIngressanteIndividual"]);

            return lote;
        }

        /// <summary>
        /// Processa um ingressante
        /// </summary>
        /// <param name="item">Dados do ingressante</param>
        /// <returns>True caso o ingressante seja processado com sucesso</returns>
        public override bool ProcessItem(PessoaIntegracaoData item)
        {
            try
            {
                ExecutaImportacao(item, Filter.SeqEntidadeInstituicao, Filter.SeqChamada);
                return true;
            }
            catch (SMCApplicationException ex)
            {
                Scheduler.LogError(ex.Message, item.SeqInscricao, item.Nome);
                return true;
            }
            catch (Exception ex)
            {
                Scheduler.LogError("Ocorreu um erro inesperado ao processar o ingressante, verifique o Event Viewer.", item.SeqInscricao, item.Nome);
                SMCLogger.Error("Falha na importação de ingressante", ex);
                return false;
            }
        }

        #endregion [ Métodos principais ]

        #region [ Métodos opcionais ]

        public override bool OnJobFinished(bool success)
        {
            if (ProcessadosComSucesso > 0)
            {
                var chamada = new Chamada()
                {
                    Seq = Filter.SeqChamada,
                    SituacaoChamada = SituacaoChamada.AguardandoLiberacaoParaMatricula
                };
                ChamadaDomainService.UpdateFields(chamada, f => f.SituacaoChamada);

                Scheduler.LogSucess($"Carga de ingressantes finalizada. Ingressantes carregados: {ProcessadosComSucesso}. Candidatos não carregados: {Batch.Count - ProcessadosComSucesso}");
            }
            else if (Batch.Count > 0)
            {
                // Se nenhum usuário tiver sido importado e existirem inscritos encontrados, termina a tarefa sem sucesso.
                // Registra o feedback do final da operação
                Scheduler.LogWaring($"Carga de ingressantes finalizada. Nenhum candidato foi carregado.");
            }
            return base.OnJobFinished(success);
        }

        #endregion [ Métodos opcionais ]

        #region [ Métodos de apoio ]

        public void ExecutaImportacao(PessoaIntegracaoData inscrito, long seqEntidadeInstituicao, long seqChamada)
        {
            if (_cargaIndividual)
            {
                try
                {
                    CargaAPI.Execute("", new
                    {
                        Filter.SeqHistoricoAgendamento,
                        Filter.SeqChamada,
                        Filter.SeqEntidadeInstituicao,
                        Filter.SeqSolicitante,
                        Filter.NomeSolicitante,
                        Inscrito = inscrito
                    });
                }
                catch (SMCApplicationException ex)
                {
                    throw new SMCApplicationException(ex.Message.Replace("InternalServerError - ",""), ex);
                }
            }
            else
            {
                IngressanteDomainService.ProcessarInscrito(inscrito, seqEntidadeInstituicao, seqChamada);
            }
            ProcessadosComSucesso++;
        }

        #endregion [ Métodos de apoio ]
    }
}
