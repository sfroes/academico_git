using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class ExtensaoNaoPermitidaException : SMCApplicationException
    {
        public ExtensaoNaoPermitidaException(string nomeArquivo)
            : base(string.Format(ExceptionsResource.ERR_ExtensaoNaoPermitidaException, nomeArquivo))
        { }
    }
}


