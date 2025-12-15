using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class ProgramaSemTipoAutorizacaoTextoCompletoException : SMCApplicationException
    {
        public ProgramaSemTipoAutorizacaoTextoCompletoException()
            : base(ExceptionsResource.ERR_ProgramaSemTipoAutorizacaoTextoCompletoException)
        {
        }
    }
}