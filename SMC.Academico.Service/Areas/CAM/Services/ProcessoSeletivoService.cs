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
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class ProcessoSeletivoService : SMCServiceBase, IProcessoSeletivoService
    {
        #region [ Serviços ]

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService
        {
            get { return this.Create<ProcessoSeletivoDomainService>(); }
        }

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService => Create<ProcessoSeletivoOfertaDomainService>();

        #endregion [ Serviços ]

        public List<SMCDatasourceItem> BuscarProcessosSeletivosSelect()
        {
            return ProcessoSeletivoDomainService.BuscarProcessosSeletivosSelect();
        }

        /// <summary>
        /// Busca os processos letivos de uma campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos cilos letivos</returns>
        public List<SMCDatasourceItem> BuscarProcessosSeletivosPorCampanhaSelect(long seqCampanha)
        {
            return ProcessoSeletivoDomainService.BuscarProcessosSeletivosPorCampanhaSelect(seqCampanha);
        }

        /// <summary>
        /// Busca os processos letivos de uma campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos processos seletivos</returns>
        public List<SMCDatasourceItem> BuscarProcessosSeletivosPorCampanhaIngressoDiretoSelect(long seqCampanha)
        {
            return ProcessoSeletivoDomainService.BuscarProcessosSeletivosPorCampanhaIngressoDiretoSelect(seqCampanha);
        }

        /// <summary>
        /// Busca os processos letivos de um tipo de processo seletivo
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <param name="seqTipoProcessoSeletivo">Sequencial do tipo de processo seletivo</param>
        /// <returns>Dados dos processos seletivos</returns>
        public List<SMCDatasourceItem> BuscarProcessosSeletivosPorCampanhaTipoProcessoSeletivoSelect(long seqCampanha, long? seqTipoProcessoSeletivo = null)
        {
            var spec = new ProcessoSeletivoFilterSpecification() { SeqCampanha = seqCampanha, SeqTipoProcessoSeletivo = seqTipoProcessoSeletivo };

            var result = ProcessoSeletivoDomainService
                .SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
                {
                    Seq = p.Seq,
                    Descricao = p.Descricao
                });

            return result.ToList();
        }

        public SMCPagerData<ProcessoSeletivoListaData> BuscarProcessosSeletivos(ProcessoSeletivoFiltroData filtro)
        {
            var spec = filtro.Transform<ProcessoSeletivoFilterSpecification>();
            var lista = ProcessoSeletivoDomainService.SearchProjectionBySpecification(spec, x => new ProcessoSeletivoListaData
            {
                Seq = x.Seq,
                Descricao = x.Descricao,
                NiveisEnsino = x.NiveisEnsino.Select(f => f.NivelEnsino.Descricao).ToList(),
                TipoProcessoSeletivo = x.TipoProcessoSeletivo.Descricao,
                SeqCampanha = filtro.SeqCampanha,
                IngressoDireto = x.TipoProcessoSeletivo.IngressoDireto
            }, out int total).ToList();

            return new SMCPagerData<ProcessoSeletivoListaData>(lista, total);
        }

        public ProcessoSeletivoData BuscarProcessosSeletivo(long seq)
        {
            return ProcessoSeletivoDomainService.BuscarProcessosSeletivo(seq).Transform<ProcessoSeletivoData>();
        }

        public long SalvarProcessoSeletivo(ProcessoSeletivoData processo)
        {
            return ProcessoSeletivoDomainService.SalvarProcessoSeletivo(processo.Transform<ProcessoSeletivoVO>());
        }

        public ProcessoSeletivoCabecalhoData BuscarProcessosSeletivoCabecalho(long seqProcessoSeletivo)
        {
            var spec = new SMCSeqSpecification<ProcessoSeletivo>(seqProcessoSeletivo);
            return ProcessoSeletivoDomainService.SearchProjectionByKey(spec, x => new ProcessoSeletivoCabecalhoData
            {
                SeqCampanha = x.SeqCampanha,
                Campanha = x.Campanha.Descricao,
                CiclosLetivos = x.Campanha.CiclosLetivos.Select(f => f.CicloLetivo.Descricao).ToList(),
                ProcessoSeletivo = x.Descricao,
                TipoProcesso = x.TipoProcessoSeletivo.Descricao,
                NiveisEnsino = x.NiveisEnsino.Select(n => n.NivelEnsino.Descricao).ToList()
            });
        }

        /// <summary>
        /// Método que lista os processos seletivos e suas respectivas convocações, da campanha
        /// </summary>
        /// <param name="seqCampanha"></param>
        /// <returns></returns>
        public List<ProcessoSeletivoData> BuscarProcessosSeletivosConvocacao(long seqCampanha)
        {
            return ProcessoSeletivoDomainService.BuscarProcessosSeletivosConvocacao(seqCampanha)
                                                .TransformList<ProcessoSeletivoData>();
        }

        public List<CampanhaCopiaConvocacaoProcessoSeletivoData> BuscarConvocacoesProcessosSeletivos(long[] seqsProcessosSeletivos)
        {
            return ProcessoSeletivoDomainService.BuscarConvocacoesProcessosSeletivos(seqsProcessosSeletivos)
                                                .TransformList<CampanhaCopiaConvocacaoProcessoSeletivoData>();
        }

        public List<CampanhaCopiaEtapaProcessoGPIData> BuscarEtapasProcessosGPI(long[] seqsProcessosSeletivos)
        {
            return ProcessoSeletivoDomainService.BuscarEtapasProcessosGPI(seqsProcessosSeletivos)
                                                .TransformList<CampanhaCopiaEtapaProcessoGPIData>();
        }

        public void SalvarOfertaProcessoSeletivo(ProcessoSeletivoOfertaData data)
        {
            ProcessoSeletivoOfertaDomainService.SalvarOfertaProcessoSeletivo(data.Transform<ProcessoSeletivoOfertaVO>());
        }

        public ProcessoSeletivoData NovoProcessosSeletivo(long seqCampanha)
        {
            return ProcessoSeletivoOfertaDomainService.NovoProcessosSeletivo(seqCampanha).Transform<ProcessoSeletivoData>();
        }
    }
}