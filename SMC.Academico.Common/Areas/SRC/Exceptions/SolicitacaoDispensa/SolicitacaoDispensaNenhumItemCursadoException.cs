using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaNenhumItemCursadoException : SMCApplicationException
    {
        public SolicitacaoDispensaNenhumItemCursadoException()
            : base(ExceptionsResource.ERR_SolicitacaoDispensaNenhumItemCursadoException)
        { }
    }
}