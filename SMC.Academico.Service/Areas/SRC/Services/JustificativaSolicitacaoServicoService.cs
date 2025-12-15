using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class JustificativaSolicitacaoServicoService : SMCServiceBase, IJustificativaSolicitacaoServicoService
    {
        public JustificativaSolicitacaoServicoDomainService JustificativaSolicitacaoServicoDomainService { get { return this.Create<JustificativaSolicitacaoServicoDomainService>(); } }

        public List<SMCDatasourceItem> BuscarJustificativasSolicitacaoServicoSelect(JustificativaSolicitacaoServicoFiltroData filter)
        {
            return JustificativaSolicitacaoServicoDomainService.BuscarJustificativasSolicitacaoServicoSelect(filter.Transform<JustificativaSolicitacaoServicoFilterSpecification>());
        }
    }
}