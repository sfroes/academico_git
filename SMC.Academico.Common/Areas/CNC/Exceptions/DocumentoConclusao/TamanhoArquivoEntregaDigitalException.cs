using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class TamanhoArquivoEntregaDigitalException : SMCApplicationException
    {
        public TamanhoArquivoEntregaDigitalException(string nomeArquivo, string tamanhoArquivo)
            : base(string.Format(ExceptionsResource.ERR_TamanhoArquivoEntregaDigitalException, nomeArquivo, tamanhoArquivo))
        { }
    }
}
