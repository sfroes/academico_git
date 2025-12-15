using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class EscalonamentoService : SMCServiceBase, IEscalonamentoService
    {
        #region [ DomainService ]

        private EscalonamentoDomainService EscalonamentoDomainService
        {
            get { return this.Create<EscalonamentoDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca a lista de escalonamentos pelo sequencial do processo etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial processo etapa</param>
        /// <returns>Select com a lista de escalonamento</returns>
        public List<SMCDatasourceItem> BuscarEscalonamentoPorProcessoEtapaSelect(long seqProcessoEtapa)
        {
            return EscalonamentoDomainService.BuscarEscalonamentoPorProcessoEtapaSelect(seqProcessoEtapa);
        }

        /// <summary>
        /// Busca a lista de grupos escalonamento do processo etapa
        /// </summary>
        /// <param name="seq">Objeto de filtro com escalonamento</param>
        /// <returns>Objeto escalonamento com sequenciais de grupo escalonamento</returns>
        public ProcessoEtapaProcessamentoListarData BuscarSeqsGrupoEscalonamentosPorProcessoEtapa(long seq)
        {
            return EscalonamentoDomainService.BuscarSeqsGrupoEscalonamentosPorProcessoEtapa(seq).Transform<ProcessoEtapaProcessamentoListarData>();
        }

        /// <summary>
        /// Buscar escalonamentos de um processo organizados por etapas
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista de escalonamentos de um processo organizado por etapas</returns>
        public SMCPagerData<EscalonamentoListarData> BuscarEscalonamentosPorProcesso(EscalonamentoFiltroData filtro)
        {
            var spec = new ProcessoFilterSpecification() { Seq = filtro.SeqProcesso };

            return new SMCPagerData<EscalonamentoListarData>(EscalonamentoDomainService.BuscarEscalonamentosPorProcesso(spec).TransformList<EscalonamentoListarData>());
        }

        /// <summary>
        /// Buscar escalonamento
        /// </summary>
        /// <param name="seq">Sequencial do escalonamento</param>
        /// <returns>Dados do escalonamento</returns>
        public EscalonamentoData BuscarEscalonamento(long seq)
        {
            return this.EscalonamentoDomainService.BuscarEscalonamento(seq).Transform<EscalonamentoData>();
        }

        /// <summary>
        /// Salvar um novo escalonamento
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        public long SalvarEscalonamento(EscalonamentoData modelo)
        {
            return this.EscalonamentoDomainService.SalvarEscalonamento(modelo.Transform<Escalonamento>());
        }

        /// <summary>
        /// Validar se a data final do escalonametno e maior que a data final do processo e menor
        /// data final periodo financeiro de um respectivo ciclo letivo de um processo, segundo RN_SRC_036 CONSISTÊNCIAS NA INCLUSÃO / ALTERAÇÃO
        /// </summary>
        /// <param name="dataFimEscalonamento">Data final do escalonamento</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa</param>       
        /// <returns>True e False</returns>
        public bool ValidarDataFimEscalonamento(long seqProcessoEtapa, DateTime dataFimEscalonamento)
        {
            return this.EscalonamentoDomainService.ValidarDataFimEscalonamento(seqProcessoEtapa, dataFimEscalonamento);
        }

        /// <summary>
        /// Recupera os grupos de escalonamentos de um escalonamento
        /// </summary>
        /// <param name="seqEscalonamento">Sequencial do escalonamento</param>
        /// <returns>Grupos de escalonamentos separados por virgua</returns>
        public string RecuperaGrupoEscalonamentoPorEscalonamento(long seqEscalonamento)
        {
            return this.EscalonamentoDomainService.RecuperaGrupoEscalonamentoPorEscalonamento(seqEscalonamento);
        }

        /// <summary>
        /// Verifica se a data fim do escalonamento e a data data fim da parcela são iguais
        /// </summary>
        /// <param name="modelo">Modelo com os dados escalonamento</param>
        /// <returns>True se as parcelas forem iguais</returns>
        public bool VerificarDataParcelasDataFimEscalonamento(EscalonamentoData modelo)
        {
            return this.EscalonamentoDomainService.VerificarDataParcelasDataFimEscalonamento(modelo.Transform<EscalonamentoVO>());
        }

        /// <summary>
        /// Exluir escalonamento selecionado
        /// </summary>
        /// <param name="seq">Sequencial do escalonamento</param>
        public void ExlcuirEscalonamento(long seq)
        {
            this.EscalonamentoDomainService.ExlcuirEscalonamento(seq);
        }

        /// <summary>
        /// Verificar se existem solicitações associadas a um grupo de escalonamento
        /// </summary>
        /// <param name="seqEscalonamento">Sequencial do escalonamento</param>
        /// <param name="dataFimEscalonamento">Data final do escalonamento</param>
        /// <returns>Boleano afirmando se existe solicitação de serviço</returns>
        public bool VerificarExisteSolicitacaoServicoGrupoPorEscalonamento(long seqEscalonamento, DateTime dataFimEscalonamento)
        {
            return this.EscalonamentoDomainService.VerificarExisteSolicitacaoServicoGrupoPorEscalonamento(seqEscalonamento, dataFimEscalonamento);

        }
    }
}
