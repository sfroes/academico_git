using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class TipoGrupoCurricularService : SMCServiceBase, ITipoGrupoCurricularService
    {
        #region [ Serviços ]

        private InstituicaoNivelTipoGrupoCurricularDomainService InstituicaoNivelTipoGrupoCurricularDomainService
        {
            get { return this.Create<InstituicaoNivelTipoGrupoCurricularDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca todos os Tipos de Grupos Curriculares do Níveil de Ensino informado, respeidando a configuração de Instituição Nível X Grupo Curricular
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do Nível de Ensino</param>
        /// <returns>Dados dos Grupos Curriculares do Nível de Ensino informado e que estejam associados à Instituição</returns>
        public List<SMCDatasourceItem> BuscarTiposGruposCurricularesInstituicaoNivelEnsinoSelect(long seqNivelEnsino)
        {
            return this.InstituicaoNivelTipoGrupoCurricularDomainService
                .BuscarTipoGrupoCurricularNivelEnsinoSelect(seqNivelEnsino);
        }

        /// <summary>
        /// Busca os Formatos de Configuração de Grupo respeitando a configuração de Nível de Ensino da Instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Formatos de Configuração de Grupo Curricular disponíveis na instituição</returns>
        public List<SMCDatasourceItem> BuscarFormatosConfiguracaoGrupoPorNivelEnsinoDaInstituicaoSelect(long seqNivelEnsino)
        {
            return this.InstituicaoNivelTipoGrupoCurricularDomainService
                .BuscarFormatosConfiguracaoGrupoPorNivelEnsinoDaInstituicaoSelect(seqNivelEnsino);
        }
    }
}
