using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TrancamentoMatriculaAlunoDIException : SMCApplicationException
    {
        public TrancamentoMatriculaAlunoDIException()
            : base(ExceptionsResource.ERR_TrancamentoMatriculaAlunoDIException)
        { }
    }
}