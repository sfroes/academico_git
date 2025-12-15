using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaDivisaoComNotaCadastradaException : SMCApplicationException
    {
        public TurmaDivisaoComNotaCadastradaException() : base(ExceptionsResource.ERR_TurmaDivisaoComNotaCadastradaException)
        {
        }
    }
}
