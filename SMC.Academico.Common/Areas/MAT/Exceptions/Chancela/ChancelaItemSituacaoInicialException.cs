using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ChancelaItemSituacaoInicialException : SMCApplicationException
    {
        public ChancelaItemSituacaoInicialException(string dscSituacao)
            : base(string.Format(ExceptionsResource.ERR_ChancelaItemAguardandoChancela, dscSituacao))
        { }
    }
}