using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class TipoDocumentoJaAssociadoEmOutraEtapaDocumentoException : SMCApplicationException
    {
        public TipoDocumentoJaAssociadoEmOutraEtapaDocumentoException()
            : base(ExceptionsResource.ERR_TipoDocumentoJaAssociadoEmOutraEtapaDocumentoException)
        {
        }
    }
}
