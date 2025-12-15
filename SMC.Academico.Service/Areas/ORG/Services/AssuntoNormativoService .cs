using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class AssuntoNormativoService : SMCServiceBase, IAssuntoNormativoService
    {
        #region [Dominio]

        private AssuntoNormativoDomainService AssuntoNormativosDomainService =>  this.Create<AssuntoNormativoDomainService>();

        #endregion

        /// <summary>
        /// Buscar os assuntos normativos
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns>Assuntos normativos select</returns>
        public List<SMCDatasourceItem> BuscarAssuntosNormativoSelect(TipoAtoNormativoFiltroData filtro)
        {
            return this.AssuntoNormativosDomainService.BuscarAssuntosNormativoSelect(filtro.Transform<AssuntoNormativoFilterSpecification>());
        }

        /// <summary>
        /// Buscar assunto normativo
        /// </summary>
        /// <param name="seqAssuntoNormativo">Sequencial ato normativo</param>
        /// <returns>Assunto normativo</returns>
        public AssuntoNormativoData BuscarAssuntoNormativo(long seqAssuntoNormativo)
        {
            return this.AssuntoNormativosDomainService.BuscarAssuntoNormativo(seqAssuntoNormativo).Transform<AssuntoNormativoData>();
        }
    }
}