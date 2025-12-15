using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.DCT.Services
{
    public class LogAtualizacaoColaboradorService : SMCServiceBase, ILogAtualizacaoColaboradorService
    {
        #region [ DomainServices ]

        private LogAtualizacaoColaboradorDomainService LogAtualizacaoColaboradorDomainService
        {
            get { return this.Create<LogAtualizacaoColaboradorDomainService>(); }
        }

        #endregion [ DomainServices ]

        public List<RelatorioLogAtualizacaoColaboradorListaData> BuscarLogsAtualizacoesColaboradoresRelatorio(RelatorioLogAtualizacaoColaboradorFiltroData filtro)
        {
            return LogAtualizacaoColaboradorDomainService.BuscarLogsAtualizacoesColaboradoresRelatorio(filtro.Transform<RelatorioLogAtualizacaoColaboradorFiltroVO>()).TransformList<RelatorioLogAtualizacaoColaboradorListaData>();
        }
    }
}