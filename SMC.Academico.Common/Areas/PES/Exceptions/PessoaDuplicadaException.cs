using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaDuplicadaException : SMCApplicationException
    {
        public PessoaDuplicadaException(string acao)
            : base(string.Format(ExceptionsResource.ERR_PessoaDuplicadaException, acao))
        {
        }
    }
}