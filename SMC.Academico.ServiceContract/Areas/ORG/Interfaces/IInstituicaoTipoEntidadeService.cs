using SMC.Academico.Service.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IInstituicaoTipoEntidadeService : ISMCService
    {
        /// <summary>
        /// Busca as configurações do tipo de entidade para a instituição
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Configurações do tipo de entidade na instituição</returns>
        InstituicaoTipoEntidadeData BuscarTipoEntidadeDaInstituicao(long seqTipoEntidade);

        /// <summary>
        /// Busca os tipos de entidade baseados em um filtro
        /// </summary>
        /// <param name="filtro">Filtro a ser considerado na busca</param>
        /// <returns>Lista de tipos de entidade na instituição</returns>
        SMCPagerData<InstituicaoTipoEntidadeListaData> BuscarInstituicaoTiposEntidade(InstituicaoTipoEntidadeFiltroData filtro);

        /// <summary>
        /// Busca as situações do tipo de entidade para a instituição
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo de entidade</param>
        /// <returns>Lista de situações de um tipo de entidade na instituição</returns>
        List<SMCDatasourceItem> BuscarSituacoesTipoEntidadeDaInstituicaoSelect(long seqTipoEntidade);

        /// <summary>
        /// Busca a lista de tipos de entidade da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de tipos de entidade</returns>
        List<SMCDatasourceItem> BuscarTipoEntidadesDaInstituicaoSelect();

        /// <summary>
        /// Busca a lista tipos de entidade da instituição para popular um Select
        /// O sequencial retornado é o da instituição_tipo_entidade
        /// </summary>
        /// <returns>Lista de tipos de entidade</returns>
        List<SMCDatasourceItem> BuscarInstituicaoTiposEntidadeSelect();

        /// <summary>
        /// Busca a lista tipos de entidade da instituição, excluindo as que possuem os tokens informados para popular um Select
        /// </summary>
        /// <param name="tokens">tokens a serem desconsiderados no select</param>
        /// <returns>Lista de tipos de entidade</returns>
        List<SMCDatasourceItem> BuscarInstituicaoTiposEntidadeTokenSelect(string[] tokens);

        /// <summary>
        /// Busca a lista de tipos de entidade não externadas da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de tipos de entidade</returns>
        List<SMCDatasourceItem> BuscarTipoEntidadesNaoExternadaDaInstituicaoSelect();

        /// <summary>
        /// Busca os tipos de entidade superiores cujo tipo de entidade seja CURSO_OFERTA_LOCALIDADE
        /// </summary>
        /// <returns>Lista de tipos de entidade</returns>
        List<SMCDatasourceItem> BuscarTipoEntidadesSuperioresCursoUnidadeDaInstituicaoSelect();

        /// <summary>
        /// Grava os parâmetros de uma instinuição validando que suas situações não sejam de categorias duplicadas
        /// </summary>
        /// <param name="instituicaoTipoEntidade">Dados dos parâmetros da instituição de ensino</param>
        /// <returns>Sequencial do objeto gravado</returns>
        long SalvarInstituicaoTipoEntidade(InstituicaoTipoEntidadeData instituicaoTipoEntidade);

        /// <summary>
        /// Buscar todas as situações de entidade
        /// </summary>
        /// <returns>Lista com as situações de entidade</returns>
        List<SMCDatasourceItem> BuscarSituacoesEntidadeSelect();

        bool ExisteTipoEntidadePorVinculoTipoFuncionario(List<long> seqsTipoEntidade);
    }
}