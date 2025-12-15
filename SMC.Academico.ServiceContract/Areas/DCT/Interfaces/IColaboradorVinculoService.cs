using SMC.Academico.Common.Areas.DCT.Exceptions;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface IColaboradorVinculoService : ISMCService
    {
        /// <summary>
        /// Configura o tipo de formação específica que pode ser selecionado como linha de pesquia
        /// </summary>
        /// <returns>ColaboradorVinculoData com o tipo da formação específcia configurado</returns>
        ColaboradorVinculoData BuscarConfiguracaoColaboradorVinculo(long seqColaborador);

        /// <summary>
        /// Busca um vinculo de colaborador com suas depêndencias
        /// </summary>
        /// <param name="seq">Sequencial do colaborador</param>
        /// <returns>Dados do colaborador</returns>
        ColaboradorVinculoData BuscarColaboradorVinculo(long seq);

        /// <summary>
        /// Busca os tipos de atividade para o nível de ensino do curso oferta localidade informado na instituição
        /// </summary>
        /// <param name="seqCursoOfertaLocalidade">Sequencial do curso oferta localidade com o nível de ensino</param>
        /// <returns>Atividades associadas ao nível de ensino do curso oferta localidae</returns>
        List<SMCDatasourceItem> BuscarTiposAtividadeCursoOfertaLocalidadeSelect(long seqCursoOfertaLocalidade);

        /// <summary>
        /// Verifica se a entidade vinculada é do tipo grupo programa
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade vinculada</param>
        /// <returns>Verdadeiro se a entidade é do tipo grupo programa</returns>
        bool ValidarVinculoGrupoPrograma(long seqEntidadeVinculo);

        /// <summary>
        /// Exclui o colaborador informado
        /// </summary>
        /// <param name="seq">Sequencial do colaborador a ser excluído</param>
        /// <exception cref="ExclusaoColaboradorVinculoNaoPermitidaException">Caso seja solicitada a exclusão de um colaborador inserido por carga</exception>
        void ExcluirColaboradorVinculo(long seq);

        /// <summary>
        /// Busca vinculos do colaborador
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos vinculos do colaborador</returns>
        SMCPagerData<ColaboradorVinculoListaData> BuscarVinculosColaborador(ColaboradorVinculoFiltroData filtros);

        /// <summary>
        /// Buscar os vinculos de um colaborador
        /// </summary>
        /// <param name="SeqColaborador">Sequencial do colaborador</param>
        /// <param name="SeqEntidadeResponsavel">Sequencial da entidade responsável</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarVinculosColaboradorSelect(long seqColaborador, long seqEntidadeResponsavel);

        /// <summary>
        /// Salvar dados do colaborador vinculo
        /// </summary>
        /// <param name="modelo">Dados as serem salvos</param>
        /// <returns>Sequencial do colaborador vinculo</returns>
        long SalvarColaboradorVinculo(ColaboradorVinculoData modelo);

        /// <summary>
        /// Verificar se existe a mesma formação especifica com período de datas coincidentes
        /// </summary>
        /// <param name="formacaoEspecificas">Formações específicas do vínculo</param>
        /// <param name="operacao">Operação para a mensagem de erro</param>
        /// <exception cref="ColaboradorVinculoMesmaFormacaoDatasCoincidentesException">Caso ocorra sobreposição de datas para dois vínculos com a mesma formação</exception>
        void ValidarSobreposicaoPeriodosFormacoesEspecificas(List<ColaboradorVinculoFormacaoEspecificaData> formacaoEspecificas, string operacao);

        /// <summary>
        /// Valida as datas preenchidas para o colaborador
        /// </summary>
        void ValidarDatasVinculo(ColaboradorVinculoData colaboradorVinculoData);

        /// <summary>
        /// Buscar os professores pós-doutorandos
        /// </summary>
        /// <param name="seqEntidadeResponsavel">Sequencial da entidade responsavel</param>
        /// <returns>Lista com os professores pós-doutorandos</returns>
        List<SMCDatasourceItem> BuscarPosDoutorandosSelect(long seqEntidadeResponsavel);

        /// <summary>
        /// Buscar os dados para emissão do certificado de pós doutorando
        /// </summary>
        /// <param name="filtro">Filtro para recuperar os dados</param>
        /// <returns>Dados para emissão do certificado</returns>
        RelatorioCertificadoPosDoutorListaData BuscarDadosCertificadoPosDoutor(RelatorioCertificadoPosDoutorFiltroData filtro);

    }
}