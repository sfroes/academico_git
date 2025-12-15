using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class CodigoAmostraPpaNaoEncontradoException : SMCApplicationException
    {
        public CodigoAmostraPpaNaoEncontradoException() 
            : base(ExceptionsResource.ERR_CodigoAmostraPpaNaoEncontradoException)
        { }
    }
}
