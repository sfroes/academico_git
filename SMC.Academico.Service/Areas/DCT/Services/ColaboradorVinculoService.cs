using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class ColaboradorVinculoService : SMCServiceBase, IColaboradorVinculoService
    {
        #region [ Services ]

        private ColaboradorVinculoDomainService ColaboradorVinculoDomainService
        {
            get { return this.Create<ColaboradorVinculoDomainService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Configura o tipo de formação específica que pode ser selecionado como linha de pesquia
        /// </summary>
        /// <returns>ColaboradorVinculoData com o tipo da formação específcia configurado</returns>
        public ColaboradorVinculoData BuscarConfiguracaoColaboradorVinculo(long seqColaborador)
        {
            return ColaboradorVinculoDomainService.BuscarConfiguracaoColaboradorVinculo(seqColaborador).Transform<ColaboradorVinculoData>();
        }

        /// <summary>
        /// Busca um vinculo de colaborador com suas depêndencias
        /// </summary>
        /// <param name="seq">Sequencial do colaborador</param>
        /// <returns>Dados do colaborador</returns>
        public ColaboradorVinculoData BuscarColaboradorVinculo(long seq)

        {
            return this.ColaboradorVinculoDomainService.BuscarColaboradorVinculo(seq).Transform<ColaboradorVinculoData>();
        }

        /// <summary>
        /// Busca os tipos de atividade para o nível de ensino do curso oferta localidade informado na instituição
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade com o nível de ensino</param>
        /// <returns>Atividades associadas ao nível de ensino do curso oferta localidae</returns>
        public List<SMCDatasourceItem> BuscarTiposAtividadeCursoOfertaLocalidadeSelect(long seqCursoOfertaLocalidade)
        {
            return this.ColaboradorVinculoDomainService.BuscarTiposAtividadeCursoOfertaLocalidadeSelect(seqCursoOfertaLocalidade);
        }

        /// <summary>
        /// Verifica se a entidade vinculada é do tipo grupo programa
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade vinculada</param>
        /// <returns>Verdadeiro se a entidade é do tipo grupo programa</returns>
        public bool ValidarVinculoGrupoPrograma(long seqEntidadeVinculo)
        {
            return this.ColaboradorVinculoDomainService.ValidarVinculoGrupoPrograma(seqEntidadeVinculo);
        }

        /// <summary>
        /// Exclui o colaborador informado
        /// </summary>
        /// <param name="seq">Sequencial do colaborador a ser excluído</param>
        /// <exception cref="ExclusaoColaboradorVinculoNaoPermitidaException">Caso seja solicitada a exclusão de um colaborador inserido por carga</exception>
        public void ExcluirColaboradorVinculo(long seq)
        {
            this.ColaboradorVinculoDomainService.ExcluirColaboradorVinculo(seq);
        }

        /// <summary>
        /// Busca vinculos do colaborador
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos vinculos do colaborador</returns>
        public SMCPagerData<ColaboradorVinculoListaData> BuscarVinculosColaborador(ColaboradorVinculoFiltroData filtros)
        {
            return this.ColaboradorVinculoDomainService.BuscarVinculosColaborador(filtros.Transform<ColaboradorVinculoFilterSpecification>()).Transform<SMCPagerData<ColaboradorVinculoListaData>>();
        }

  

        /// <summary>
        /// Salvar dados do colaborador vinculo
        /// </summary>
        /// <param name="modelo">Dados as serem salvos</param>
        /// <returns>Sequencial do colaborador</returns>
        public long SalvarColaboradorVinculo(ColaboradorVinculoData modelo)
        {
            return this.ColaboradorVinculoDomainService.SalvarColaboradorVinculo(modelo.Transform<ColaboradorVinculoVO>());
        }

        /// <summary>
        /// Verificar se existe a mesma formação especifica com período de datas coincidentes
        /// </summary>
        /// <param name="formacaoEspecificas">Formações específicas do vínculo</param>
        /// <param name="operacao">Operação para a mensagem de erro</param>
        /// <exception cref="ColaboradorVinculoMesmaFormacaoDatasCoincidentesException">Caso ocorra sobreposição de datas para dois vínculos com a mesma formação</exception>
        public void ValidarSobreposicaoPeriodosFormacoesEspecificas(List<ColaboradorVinculoFormacaoEspecificaData> formacaoEspecificas, string operacao)
        {
            this.ColaboradorVinculoDomainService.ValidarSobreposicaoPeriodosFormacoesEspecificas(formacaoEspecificas.TransformList<ColaboradorVinculoFormacaoEspecifica>(), operacao);
        }

        /// <summary>
        /// Valida as datas preenchidas para o colaborador
        /// </summary>
        public void ValidarDatasVinculo(ColaboradorVinculoData colaboradorVinculo)
        {
            ColaboradorVinculoDomainService.ValidarDatasVinculo(colaboradorVinculo.Transform<ColaboradorVinculoVO>());
        }

        /// <summary>
        /// Buscar os professores pós-doutorandos
        /// </summary>
        /// <param name="seqEntidadeResponsavel">Sequencial da entidade responsavel</param>
        /// <returns>Lista com os professores pós-doutorandos</returns>
        public List<SMCDatasourceItem> BuscarPosDoutorandosSelect(long seqEntidadeResponsavel)
        {
            return this.ColaboradorVinculoDomainService.BuscarPosDoutorandosSelect(seqEntidadeResponsavel);
        }

        public List<SMCDatasourceItem> BuscarVinculosColaboradorSelect(long seqColaborador, long seqEntidadeResponsavel)
        {
            return this.ColaboradorVinculoDomainService.BuscarVinculosColaboradorSelect(seqColaborador,seqEntidadeResponsavel);
        }

        public RelatorioCertificadoPosDoutorListaData BuscarDadosCertificadoPosDoutor(RelatorioCertificadoPosDoutorFiltroData filtro)
        {
            return ColaboradorVinculoDomainService.BuscarDadosCertificadoPosDoutor(filtro.Transform<RelatorioCertificadoPosDoutorFiltroVO>()).Transform<RelatorioCertificadoPosDoutorListaData>();
        }
    }
}