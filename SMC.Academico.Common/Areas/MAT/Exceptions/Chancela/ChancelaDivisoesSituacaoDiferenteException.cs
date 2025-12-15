using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ChancelaDivisoesSituacaoDiferenteException : SMCApplicationException
    {
        public ChancelaDivisoesSituacaoDiferenteException(string turma)
            : base(string.Format(ExceptionsResource.ERR_ChancelaDivisoesSituacaoDiferenteException, turma))
        { }
    }
}