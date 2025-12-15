using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaDivisaoComAvaliacaoCadastradaException : SMCApplicationException
    {
        public TurmaDivisaoComAvaliacaoCadastradaException() : base(ExceptionsResource.ERR_TurmaDivisaoComAvaliacaoCadastradaException)
        {
        }
    }
}
