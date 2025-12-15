using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class ModalidadeService : SMCServiceBase, IModalidadeService
    {
        #region [ DomainService ]

        private ModalidadeDomainService ModalidadeDomainService
        {
            get { return this.Create<ModalidadeDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curriculo curso oferta da tabela curso oferta localidade
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do curriculo curso oferta</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta)
        {
            return this.ModalidadeDomainService.BuscarModalidadesPorCurriculoCursoOfertaSelect(seqCurriculoCursoOferta);
        }

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curso oferta da tabela curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta</param>
        /// <returns>Lista de modalidades</returns>
        public List<SMCDatasourceItem> BuscarModalidadesPorCursoOfertaSelect(long seqCursoOferta)
        {
            return this.ModalidadeDomainService.BuscarModalidadesPoCursoOfertaSelect(seqCursoOferta);
        }
    }
}
