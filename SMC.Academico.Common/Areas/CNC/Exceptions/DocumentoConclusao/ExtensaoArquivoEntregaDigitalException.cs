using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class ExtensaoArquivoEntregaDigitalException : SMCApplicationException
    {
        public ExtensaoArquivoEntregaDigitalException(string nomeArquivo)
            : base(string.Format(ExceptionsResource.ERR_ExtensaoArquivoEntregaDigitalException, nomeArquivo))
        { }
    }
}
