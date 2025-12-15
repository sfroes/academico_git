using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IInstituicaoNivelTipoOrientacaoService : ISMCService
    {
        long SalvarInstituicaoNivelTipoOrientacao(InstituicaoNivelTipoOrientacaoData modelo);

        /// <summary>
        /// Busca os tipos de orientação que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Tipos de orientação ordenados por descrição</returns>
        List<SMCDatasourceItem> BuscarTiposOrientacaoSelect(InstituicaoNivelTipoOrientacaoFiltroData filtros);

        /// <summary>
        /// Busca os tipos de orientação que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Tipos de orientação ordenados por descrição</returns>
        List<SMCDatasourceItem> BuscarTiposOrientacaoGambiarraBindDynamicIngressanteSelect(InstituicaoNivelTipoOrientacaoGambiarraBindDynamicIngressanteFiltroData filtros);

        /// <summary>
        /// Buscar o numero máximo de alunos por orientação 
        /// </summary>
        /// <param name="filtro">Filtros a serem selecionados</param>
        /// <returns>O numero de alunos possivel em uma orientação</returns>
        short? BuscarNumeroMaximoAlunosOrientacao(InstituicaoNivelTipoOrientacaoFiltroData filtro);

        /// <summary>
        /// Buscar todas os tipos de orientação
        /// </summary>
        /// <param name="filtros">Filtros a serem selecionados</param>
        /// <returns>Lista de tipos de orientação</returns>
        List<InstituicaoNivelTipoOrientacaoData> BuscarTiposOritencoes(InstituicaoNivelTipoOrientacaoFiltroData filtros);

        /// <summary>
        /// Busca os tipos de orientação que permite orientação manual
        /// </summary>
        /// <returns>Tipos de orientação select</returns>
        List<SMCDatasourceItem> BuscarTiposOrientacaoPermiteManutencaoManualSelect();
    }
}