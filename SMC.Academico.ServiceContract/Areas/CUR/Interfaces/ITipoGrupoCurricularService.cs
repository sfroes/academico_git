using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface ITipoGrupoCurricularService : ISMCService
    {
        /// <summary>
        /// Busca todos os Tipos de Grupos Curriculares do Níveil de Ensino informado, respeidando a configuração de Instituição Nível X Grupo Curricular
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do Nível de Ensino</param>
        /// <returns>Dados dos Grupos Curriculares do Nível de Ensino informado e que estejam associados à Instituição</returns>
        List<SMCDatasourceItem> BuscarTiposGruposCurricularesInstituicaoNivelEnsinoSelect(long seqNivelEnsino);

        /// <summary>
        /// Busca os Formatos de Configuração de Grupo respeitando a configuração de Nível de Ensino da Instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos Formatos de Configuração de Grupo Curricular disponíveis na instituição</returns>
        List<SMCDatasourceItem> BuscarFormatosConfiguracaoGrupoPorNivelEnsinoDaInstituicaoSelect(long seqNivelEnsino);
    }
}
