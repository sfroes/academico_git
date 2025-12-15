using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrientacaoOrigemException : SMCApplicationException
    {
        public OrientacaoOrigemException(string tiposOrientacoes, string status)
            : base(string.Format(ExceptionsResource.ERR_OrientacaoOrigemException, tiposOrientacoes, status))
        {
        }
    }
}