using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrientacaoObrigatoriaException : SMCApplicationException
    {
        public OrientacaoObrigatoriaException(string tiposOrientacoes, string status)
            : base(string.Format(ExceptionsResource.ERR_OrientacaoObrigatoriaException, tiposOrientacoes, status))
        {
        }
    }
}