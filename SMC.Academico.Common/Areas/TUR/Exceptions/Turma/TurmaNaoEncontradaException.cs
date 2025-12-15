using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaNaoEncontradaException : SMCApplicationException
    {
        public TurmaNaoEncontradaException()
            : base(ExceptionsResource.ERR_TurmaNaoEncontradaException)
        {
        }
    }
}
