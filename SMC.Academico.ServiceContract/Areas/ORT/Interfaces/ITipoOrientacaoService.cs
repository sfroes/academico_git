using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Interfaces
{
    public interface ITipoOrientacaoService : ISMCService
    {
        /// <summary>
        /// Busca a lista de TipoOrientacao para popular um Select
        /// </summary>
        /// <returns>Lista de tipo orientação</returns>
        List<SMCDatasourceItem> BuscarTipoOrientacaoSelect();

        /// <summary>
        /// Busca a lista de TipoOrientacao que tenham manutencão manual para popular um Select
        /// </summary>
        /// <returns>Lista de tipo orientação</returns>
        List<SMCDatasourceItem> BuscarTipoOrientacaoManutencaoManualSelect();

        /// <summary>
        ///  Listar os tipos de orientação cadastrados no sistema que não sejam orientação de turma.
        /// </summary>
        List<SMCDatasourceItem> BuscarTipoOrientacaoNaoOrientacaoTurmaSelect();

        /// <summary>
        /// Buscar tipo de orientação
        /// </summary>
        /// <param name="seq">Sequencial tipo de orientação</param>
        /// <returns>Tipo de orientação</returns>
        TipoOrientacaoData BuscarTipoOrientacao(long seq);

        /// <summary>
        /// Valida se o tipo de orientação e de conclusão de curso - TCC
        /// </summary>
        /// <param name="seq">Sequencial do tipo de orientação</param>
        /// <returns>Verdaeiro caso seja TCC</returns>
        bool ValidarTipoOrientacaoConclucaoCurso(long seq);
    }
}
