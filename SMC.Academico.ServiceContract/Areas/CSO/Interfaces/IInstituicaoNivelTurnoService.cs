using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IInstituicaoNivelTurnoService : ISMCService
    {
        /// <summary>
        /// Recupera os turnos de um nível de ensino na instituição
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial do nível de ensino na instituição</param>
        /// <returns>Sequenciais e descrições dos turnos do nível informado e instituição informados</returns>
        List<SMCDatasourceItem> BuscarInstituicaoNivelTurnoSelect(long seqInstituicaoNivel);

        /// <summary>
        /// Busca turnos para a listagem de acordo com o instituição
        /// </summary>       
        /// <returns>Lista de turnos</returns>
        List<SMCDatasourceItem> BuscarTurnosPorInstituicaoSelect();

        /// <summary>
        /// Salva a instituicao nivel turno 
        /// </summary>
        /// <param name="modelo">Modelo a ser salvo</param>
        /// <returns>Sequencial da instituicao nivel turno</returns>
        long Salvar(InstituicaoNivelTurnoData modelo);
    }
}
