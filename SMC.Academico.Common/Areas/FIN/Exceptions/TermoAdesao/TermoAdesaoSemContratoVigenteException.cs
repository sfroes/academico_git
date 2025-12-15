using SMC.Academico.Common.Areas.FIN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class TermoAdesaoSemContratoVigenteException : SMCApplicationException
    {
        public TermoAdesaoSemContratoVigenteException()
            : base(ExceptionsResource.ERR_TermoAdesaoSemContratoVigenteException)
        {
        }
    }
}