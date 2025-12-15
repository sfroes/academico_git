using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class InstituicaoNivelTipoAtividadeColaboradorService : SMCServiceBase, IInstituicaoNivelTipoAtividadeColaboradorService
    {
        private InstituicaoNivelTipoAtividadeColaboradorDomainService InstituicaoNivelTipoAtividadeColaboradorDomainService
        {
            get { return this.Create<InstituicaoNivelTipoAtividadeColaboradorDomainService>(); }
        }

        /// <summary>
        /// Retorna os tipos de atividade configurados para os colaboradoes na instituição logada
        /// </summary>
        /// <param name="filtroData">Dados do filtro</param>
        /// <returns>Dados das atividades configuradas para os colaboradoes na instituição logada</returns>
        public List<SMCDatasourceItem> BuscarTiposAtividadeColaboradorSelect(InstituicaoNivelTipoAtividadeColaboradorFiltroData filtroData)
        {
            return this.InstituicaoNivelTipoAtividadeColaboradorDomainService.BuscarTiposAtividadeColaboradorSelect(filtroData.Transform<InstituicaoNivelTipoAtividadeColaboradorFiltroVO>());
        }
    }
}