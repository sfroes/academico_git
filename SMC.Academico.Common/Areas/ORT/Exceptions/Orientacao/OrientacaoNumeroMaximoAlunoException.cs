using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrientacaoNumeroMaximoAlunoException : SMCApplicationException
    {
        public OrientacaoNumeroMaximoAlunoException()
            : base(ExceptionsResource.ERR_OrientacaoNumeroMaximoAlunoException)
        {
        }
    }
}