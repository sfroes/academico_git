using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Service;
using System.Collections.Generic;
using SMC.Framework.Model;
using SMC.Academico.Domain.Areas.CUR.DomainServices;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class TipoDivisaoCurricularService : SMCServiceBase, ITipoDivisaoCurricularService
    {

        #region DomainServices 

        public InstituicaoNivelTipoDivisaoCurricularDomainService InstituicaoNivelTipoDivisaoCurricularDomainService
        {
            get { return this.Create<InstituicaoNivelTipoDivisaoCurricularDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca a lista de tipo de divisao curricular de acordo com o nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Lista de tipos de divisão curricular</returns>
        public List<SMCDatasourceItem> BuscarTiposDivisaoCurricularNivelEnsinoSelect(long seqNivelEnsino)
        {
            return InstituicaoNivelTipoDivisaoCurricularDomainService.BuscarTiposDivisaoCurricularNivelEnsinoSelect(seqNivelEnsino);
        }
    }
}
