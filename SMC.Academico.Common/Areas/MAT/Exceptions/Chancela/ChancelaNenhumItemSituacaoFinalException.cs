using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ChancelaNenhumItemSituacaoFinalException : SMCApplicationException
    {
        public ChancelaNenhumItemSituacaoFinalException(string dscSituacao)
            : base(string.Format(ExceptionsResource.ERR_ChancelaNenhumItemSituacaoFinal, dscSituacao))
        { }
    }
}