using SMC.Academico.Common.Constants;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ITipoVinculoAlunoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTiposVinculoAlunoSelect();

        List<SMCDatasourceItem> BuscarTiposVinculoAlunoPorServicoSelect(long seqServico);

        /// <summary>
        /// Busca o tipo de processo vinculado ao processo
        /// </summary>
        /// <param name="seqProcessoSeletivo"></param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTiposVinculoAlunoPorProcessoSeletivo(long seqProcessoSeletivo);

        /// <summary>
        /// Busca todos tipos de vínculos de aluno configurados na instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos tipos de vínculo de aluno</returns>
        List<SMCDatasourceItem> BuscarTipoVinculoAlunoNaInstituicaoSelect(long? seqNivelEnsino);

        List<SMCDatasourceItem> BuscarTipoVinculoAlunoPorTipoProcessoSeletivo(long seqTipoProcessoSeletivo);
    }
}