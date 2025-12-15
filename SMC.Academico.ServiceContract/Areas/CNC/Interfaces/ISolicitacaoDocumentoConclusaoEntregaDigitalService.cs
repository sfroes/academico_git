using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface ISolicitacaoDocumentoConclusaoEntregaDigitalService : ISMCService
    {

        long SalvarLogDownloadDocumentoDigital(long seqSolicitacaoDocumentoConclusao, TipoArquivoDigital tipoArquivoDigital);
    }
}