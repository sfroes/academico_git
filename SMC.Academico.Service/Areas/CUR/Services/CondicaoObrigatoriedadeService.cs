using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class CondicaoObrigatoriedadeService : SMCServiceBase, ICondicaoObrigatoriedadeService
    {
        #region [ Services ]

        private CondicaoObrigatoriedadeDomainService CondicaoObrigatoriedadeDomainService
        {
            get { return this.Create<CondicaoObrigatoriedadeDomainService>(); }
        }

        #endregion [ Services ]

        /// <summary>
        /// Busca as condições de obrigatoriedade na instituição do nível de ensino informado
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados das condições de obrigatoriedade</returns>
        public List<SMCDatasourceItem> BuscarCondicoesObrigatoriedadePorNivelEnsino(long seqNivelEnsino)
        {
            return this.CondicaoObrigatoriedadeDomainService.BuscarCondicoesObrigatoriedadePorNivelEnsino(seqNivelEnsino);
        }

        /// <summary>
        /// Busaca as condições de obigatoriedade configuradas nos grupos curriculares da currículo curso oferta da matriz informada
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da oferta de matriz currícular</param>
        /// <returns>Condições de obrigatoriedade</returns>
        public List<SMCDatasourceItem> BuscarCondicoesObrigatoriedadePorMatrizCurricularOferta(long seqMatrizCurricularOferta)
        {
            return this.CondicaoObrigatoriedadeDomainService.BuscarCondicoesObrigatoriedadePorMatrizCurricularOferta(seqMatrizCurricularOferta);
        }
    }
}