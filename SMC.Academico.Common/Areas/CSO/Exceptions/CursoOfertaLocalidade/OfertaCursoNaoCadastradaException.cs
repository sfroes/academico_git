using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class OfertaCursoNaoCadastradaException : SMCApplicationException
    {
        public OfertaCursoNaoCadastradaException(string curso)
            : base(string.Format(ExceptionsResource.ERR_OfertaCursoNaoCadastradaException, curso))
        { }
    }
}