using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class TamanhoArquivoExcedidoException : SMCApplicationException
    {
        public TamanhoArquivoExcedidoException()
            : base(ExceptionsResource.ERR_TamanhoArquivoExcedidoException)
        { }
    }
}


