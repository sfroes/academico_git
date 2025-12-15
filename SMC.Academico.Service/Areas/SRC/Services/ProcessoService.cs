using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class ProcessoService : SMCServiceBase, IProcessoService
    {
        #region [ DomainService ]

        private ProcessoDomainService ProcessoDomainService
        {
            get { return Create<ProcessoDomainService>(); }
        }

        private ProcessoEtapaDomainService ProcessoEtapaDomainService
        {
            get { return Create<ProcessoEtapaDomainService>(); }
        }

        private GrupoEscalonamentoItemDomainService GrupoEscalonamentoItemDomainService
        {
            get { return Create<GrupoEscalonamentoItemDomainService>(); }
        }

        private TipoEntidadeDomainService TipoEntidadeDomainService
        {
            get { return Create<TipoEntidadeDomainService>(); }
        }
        private EntidadeDomainService EntidadeDomainService
        {
            get { return Create<EntidadeDomainService>(); }
        }

        #endregion [ DomainService ]

        public SMCPagerData<ProcessoListaData> BuscarProcessos(ProcessoFiltroData filtros)
        {
            var result = ProcessoDomainService.BuscarProcessos(filtros.Transform<ProcessoFiltroVO>());
            return new SMCPagerData<ProcessoListaData>(result.TransformList<ProcessoListaData>(), result.Total);
        }

        public ProcessoData BuscarProcessoInserir()
        {
            var processo = new ProcessoData();
            return processo;
        }

        public ProcessoData BuscarProcessoEditar(long seqProcesso)
        {
            var processo = ProcessoDomainService.BuscarProcessoEditar(seqProcesso).Transform<ProcessoData>();

            processo.SeqsEntidadesResponsaveis = processo.EntidadesResponsaveis.Select(a => a.Seq).ToList();
            processo.SeqsEntidadesCompartilhadas = processo.EntidadesCompartilhadas.Select(a => a.Seq).ToList();
            return processo;
        }

        public CopiarProcessoData BuscarProcessoCopiar(long seqProcesso)
        {
            var processo = ProcessoDomainService.BuscarProcessoCopiar(seqProcesso).Transform<CopiarProcessoData>();

            return processo;

        }

        public long SalvarProcesso(ProcessoData modelo)
        {
            return ProcessoDomainService.SalvarProcesso(modelo.Transform<ProcessoVO>());
        }

        public List<SMCDatasourceItem> BuscarEtapasDoProcessoSelect(long seqProcesso)
        {
            return ProcessoDomainService.BuscarEtapasDoProcessoSelect(seqProcesso);
        }

        public List<SMCDatasourceItem> BuscarSituacoesEtapasComCategoriaSelect(long? seqProcessoEtapa, List<long> seqsProcessos)
        {
            return ProcessoEtapaDomainService.BuscarSituacoesEtapasComCategoriaSelect(seqProcessoEtapa, seqsProcessos);
        }

        public List<SMCDatasourceItem> BuscarSituacoesEtapasSgfSelect(long? seqProcessoEtapaSgf, long? seqServico)
        {
            return ProcessoEtapaDomainService.BuscarSituacoesEtapasSgfSelect(seqProcessoEtapaSgf, seqServico);
        }

        /// <summary>
        /// Busca a descrição do ciclo letivo do processo
        /// </summary>
        /// <param name="seq">Sequencial do processo</param>
        /// <returns>Descrição do ciclo letivo</returns>
        public string BuscarDescricaoCicloLetivoProcesso(long seq)
        {
            return ProcessoDomainService.BuscarDescricaoCicloLetivoProcesso(seq);
        }

        /// <summary>
        /// Busca uma lista contendo todos os processos
        /// </summary>
        /// <param name="filtros">Filtros e ordenação</param>
        /// <returns>Dados dos processos filtrados e ordenados</returns>
        public List<SMCDatasourceItem> BuscarProcessosSelect(ProcessoFiltroData filtros)
        {
            return ProcessoDomainService.BuscarProcessosSelect(filtros.Transform<ProcessoFiltroVO>());
        }

        /// <summary>
        /// Busca um processo por dados do processo ou do seu relacionamento com processo seletivo
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Dados do processo</returns>
        public ProcessoData BuscarProcesso(ProcessoFiltroData filtros)
        {
            return ProcessoDomainService.BuscarProcesso(filtros.Transform<ProcessoFiltroVO>()).Transform<ProcessoData>();
        }

        /// <summary>
        /// Buscar dados do processo para montar o cabeçalho das ações relativas ao mesmo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <param name="quantidadeSolicitacoes">Exibir a quantidade de solicitações do processo</param>
        /// <returns></returns>
        public ProcessoCabecalhoData BuscarCabecalhoProcesso(long seqProcesso, bool quantidadeSolicitacoes)
        {
            var registro = ProcessoDomainService.BuscarCabecalhoProcesso(seqProcesso, quantidadeSolicitacoes).Transform<ProcessoCabecalhoData>();
            return registro;
        }

        /// <summary>
        /// Buscar dados do processo e do grupo de escalonamento para montar o cabeçalho das ações relativas ao mesmo
        /// </summary>
        /// <param name="seqGrupoEscalonamentoItem">Sequencial do grupo de escalonamento</param>
        /// <returns></returns>
        public GrupoEscalonamentoItemCabecalhoData BuscarCabecalhoGrupoEscalonamentoItem(long seqGrupoEscalonamentoItem)
        {
            GrupoEscalonamentoItem grupoEscalonamentoItem = GrupoEscalonamentoItemDomainService.SearchByKey(new SMCSeqSpecification<GrupoEscalonamentoItem>(seqGrupoEscalonamentoItem), i => i.GrupoEscalonamento, i => i.Escalonamento.ProcessoEtapa);
            var registro = ProcessoDomainService.BuscarCabecalhoProcesso(grupoEscalonamentoItem.GrupoEscalonamento.SeqProcesso, false).Transform<GrupoEscalonamentoItemCabecalhoData>();
            registro.Seq = seqGrupoEscalonamentoItem;
            registro.SeqGrupoEscalonamento = grupoEscalonamentoItem.SeqGrupoEscalonamento;
            registro.DescricaoGrupoEscalonamento = grupoEscalonamentoItem.GrupoEscalonamento.Descricao;
            registro.Ativo = grupoEscalonamentoItem.GrupoEscalonamento.Ativo;
            registro.DescricaoEtapa = grupoEscalonamentoItem.Escalonamento.ProcessoEtapa.DescricaoEtapa;
            registro.DataInicioEscalonamento = grupoEscalonamentoItem.Escalonamento.DataInicio;
            registro.DataFimEscalonamento = grupoEscalonamentoItem.Escalonamento.DataFim;
            registro.NumeroDivisaoParcelas = grupoEscalonamentoItem.GrupoEscalonamento.NumeroDivisaoParcelas;
            return registro;
        }

        public (long? SeqAgendamento, SituacaoAgendamento? Processando) BuscarAgendamentoSAT(long seqProcesso)
        {
            var agendamento = ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(seqProcesso),
                                                    x => new { x.SeqAgendamentoSat, x.SituacaoAgendamento });
            return (agendamento.SeqAgendamentoSat, agendamento.SituacaoAgendamento);
        }

        public void CriarAgendamentoPreparacaoRematricula(long seqProcesso, long seqAgendamento)
        {
            ProcessoDomainService.CriarAgendamentoPreparacaoRematricula(seqProcesso, seqAgendamento);
        }

        public bool ValidarBloqueioSituacaoDocumentacao(List<long> seqsProcessos)
        {
            return ProcessoDomainService.ValidarBloqueioSituacaoDocumentacao(seqsProcessos);
        }

        public List<SMCDatasourceItem> BuscarProcessosPorAlunoSelect(ServicoPorAlunoFiltroData filtro)
        {
            // Faz a busca dos processos liberados para o aluno
            return ProcessoDomainService.BuscarProcessosPorAlunoSelect(filtro.Transform<ServicoPorAlunoFiltroVO>());
        }

        public List<SMCDatasourceItem> BuscarProcessosPorServicoSelect(long seqServico)
        {
            return ProcessoDomainService.BuscarProcessosPorServicoSelect(seqServico);
        }

        public List<SMCDatasourceItem> BuscarProcessosMatriculaPorCicloLetivoSelect(long seqCicloLetivo, long seqCampanha)
        {
            return ProcessoDomainService.BuscarProcessosMatriculaPorCicloLetivoSelect(seqCicloLetivo, seqCampanha);
        }

        public InformacaoProcessoListaData BuscarDadosProcesso(long seqProcesso, long? seqGrupoEscalonamento)
        {
            var result = ProcessoDomainService.BuscarDadosProcesso(seqProcesso, seqGrupoEscalonamento);

            var modelo = result.Transform<InformacaoProcessoListaData>();

            modelo.InformacoesProcesso = new SMCPagerData<InformacaoProcessoItemData>(result.InformacoesProcesso.TransformList<InformacaoProcessoItemData>(), result.InformacoesProcesso.Total);

            return modelo;
        }

        public CabecalhoInformacaoProcessoData BuscarCabecalhoDadosProcesso(long seqProcesso, long? seqGrupoEscalonamento)
        {
            var result = ProcessoDomainService.BuscarCabecalhoDadosProcesso(seqProcesso, seqGrupoEscalonamento);

            return result.Transform<CabecalhoInformacaoProcessoData>();
        }

        public ConsultaPosicaoGeralData BuscarPosicaoConsolidadaGeral(ProcessoFiltroData filtros)
        {
            var model = ProcessoDomainService.BuscarPosicaoConsolidadaGeral(filtros.Transform<ProcessoFiltroVO>());
            var data = new ConsultaPosicaoGeralData()
            {
                QuantidadeSolicitacoesTotal = model.QuantidadeSolicitacoesTotal,
                Processos = model.Processos.Transform<SMCPagerData<PosicaoConsolidadaListarData>>()
            };
            return data;
        }

        /// <summary>
        /// Executa procedimentos para encerramento do processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo a ser encerrado</param>
        public void EncerrarProcesso(long seqProcesso)
        {
            ProcessoDomainService.EncerrarProcesso(seqProcesso);
        }

        public long SalvarCopiaProcesso(CopiarProcessoData modelo)
        {
            return ProcessoDomainService.SalvarCopiaProcesso(modelo.Transform<CopiarProcessoVO>());
        }

        public List<ProcessoEtapaSGFData> BuscarEtapasSGFPorServico(long seqServico)
        {
            return ProcessoDomainService.BuscarEtapasSGFPorServico(seqServico).TransformList<ProcessoEtapaSGFData>();
        }

        public bool VerificarValidadeTokenManutencaoProcesso(long seqProcesso)
        {
            return ProcessoDomainService.VerificarValidadeTokenManutencaoProcesso(seqProcesso);
        }

        public void ReabrirProcesso(long seqProcesso)
        {
            ProcessoDomainService.ReabrirProcesso(seqProcesso);
        }
    }
}