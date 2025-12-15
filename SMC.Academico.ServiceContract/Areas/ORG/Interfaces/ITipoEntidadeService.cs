using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface ITipoEntidadeService : ISMCService
    {
        /// <summary>
        /// Busca o tipos de entidade
        /// </summary>
        /// <param name="tokenTipoEntidade">Filtro</param>
        /// <returns>Tipos de entidade</returns>
        TipoEntidadeData BuscarTipoEntidadeNaInstituicao(string tokenTipoEntidade);

        /// <summary>
        /// Busca o tipos de entidade
        /// </summary>
        /// <param name="filtro">Filtro</param>
        /// <returns>Tipos de entidade</returns>
        List<TipoEntidadeData> BuscarTiposEntidade(TipoEntidadeFiltroData filtro);

        /// <summary>
        /// Busca o tipos de entidade com a inclusão do tipo Instituição de Ensino
        /// </summary>
        /// <param name="permiteAtoNormativo">Permite Ato Normativo</param>
        /// <param name="seqInstituicaoEnsino">Sequencial da Instituição de Ensino logada</param>
        /// <returns>Tipos de entidade</returns>
        List<SMCDatasourceItem> BuscarTiposEntidadeComInstituicao(bool permiteAtoNormativo, long seqInstituicaoEnsino);

        /// <summary>
        /// Busca o token de acordo com o filtro informado
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial tipo entidade</param>
        /// <returns>Retorna token</returns>
        string BuscarTokenTipoEntidade(long seqTipoEntidade);

        /// <summary>
        /// Buscar os tipos de entidade que são responsáveis em uma visão
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <param name="visao">Visão a ser pesquisada</param>
        /// <returns>Lista de tipos de entidade que são responsáveis na visão e instituição de ensino do usuário logado</returns>
        List<SMCDatasourceItem> BuscarTipoEntidadeResponsavelPorVisao(long seqInstituicaoEnsino, TipoVisao visao);

    }
}