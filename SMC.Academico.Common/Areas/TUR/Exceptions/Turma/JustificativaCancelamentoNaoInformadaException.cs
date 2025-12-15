using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class JustificativaCancelamentoNaoInformadaException : SMCApplicationException
    {
        public JustificativaCancelamentoNaoInformadaException()
            : base(ExceptionsResource.ERR_JustificativaCancelamentoNaoInformadaException)
        {
        }
    }
}