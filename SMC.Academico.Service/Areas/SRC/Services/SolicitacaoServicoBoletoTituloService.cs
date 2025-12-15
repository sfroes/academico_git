using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.SRC.Services
{
    public class SolicitacaoServicoBoletoTituloService : SMCServiceBase, ISolicitacaoServicoBoletoTituloService
    {
        #region Domain Service

        private SolicitacaoServicoBoletoTituloDomainService SolicitacaoServicoBoletoTituloDomainService { get => Create<SolicitacaoServicoBoletoTituloDomainService>(); }

        #endregion

        public List<TaxasSolicitacaoData> BuscarTaxasTitulosPorSolicitacao(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoBoletoTituloDomainService.BuscarTaxasTitulosPorSolicitacao(seqSolicitacaoServico).TransformList<TaxasSolicitacaoData>();
        }

        public List<TitulosSolicitacaoData> BuscarTitulosPorSolicitacao(long seqSolicitacaoServico)
        {
            return this.SolicitacaoServicoBoletoTituloDomainService.BuscarTitulosPorSolicitacao(seqSolicitacaoServico).TransformList<TitulosSolicitacaoData>();
        }
    }
}
