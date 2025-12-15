using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class SolicitacaoDocumentoConclusaoEntregaDigitalService : SMCServiceBase, ISolicitacaoDocumentoConclusaoEntregaDigitalService
    {
        #region DomainServices

        private SolicitacaoDocumentoConclusaoEntregaDigitalDomainService SolicitacaoDocumentoConclusaoEntregaDigitalDomainService => Create<SolicitacaoDocumentoConclusaoEntregaDigitalDomainService>();

        #endregion DomainServices

        public long SalvarLogDownloadDocumentoDigital(long seqSolicitacaoDocumentoConclusao, TipoArquivoDigital tipoArquivoDigital)
        {
            return SolicitacaoDocumentoConclusaoEntregaDigitalDomainService.SalvarLogDownloadDocumentoDigital(seqSolicitacaoDocumentoConclusao, tipoArquivoDigital);
        }
    }
}