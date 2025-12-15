using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class ServicoJaAssociadoEmOutroTipoDocumentoException : SMCApplicationException
    {
        public ServicoJaAssociadoEmOutroTipoDocumentoException()
            : base(ExceptionsResource.ERR_ServicoJaAssociadoEmOutroTipoDocumentoException)
        { }
    }
}
