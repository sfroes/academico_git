using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class EntregaOnlineParticipaEmOutraEntregaException : SMCApplicationException
    {
        public EntregaOnlineParticipaEmOutraEntregaException(string aluno)
            : base(string.Format(ExceptionsResource.ERR_EntregaOnlineParticipaEmOutraEntregaException, aluno))
        {
        }
    }
}