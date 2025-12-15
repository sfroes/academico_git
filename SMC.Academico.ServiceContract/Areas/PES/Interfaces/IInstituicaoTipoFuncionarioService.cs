using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IInstituicaoTipoFuncionarioService : ISMCService
    {
        /// <summary>
        /// Buscar tipo funcionario por instituição de ensino
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Sequencial da instituição de ensino</param>
        /// <returns>Lista de todos os tipos de funcionários da instituição de ensino</returns>
        List<SMCDatasourceItem> BuscarTipoFuncionarioInstituicao();

        long SalvarInstituicaoTipoFuncionario(InstituicaoTipoFuncionarioData instituicaoTipoFuncionario);

        List<SMCDatasourceItem> BuscarTipoFuncionarioInstituicaoETipoEntidadePorFuncionario(long seqInstituicaoEnsino);
    }
}

