using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class TipoDocumentoUnicoException : SMCApplicationException
    {
        public TipoDocumentoUnicoException()
            : base(ExceptionsResource.ERR_TipoDocumentoUnicoException)
        {

        }
    }
}
