using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class TipoDocumentoRequeridoJaExistenteEmEtapaException : SMCApplicationException
    {
        public TipoDocumentoRequeridoJaExistenteEmEtapaException(string descricaoEtapa)
             : base(string.Format(ExceptionsResource.ERR_TipoDocumentoRequeridoJaExistenteEmEtapa, descricaoEtapa))
        {}
    }
}
