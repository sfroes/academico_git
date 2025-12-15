using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class SituacaoAtualSolicitacaoNaoPermiteChancelaException : SMCApplicationException
    {
        public SituacaoAtualSolicitacaoNaoPermiteChancelaException()
            : base(ExceptionsResource.ERR_SituacaoAtualSolicitacaoNaoPermiteChancela)
        { }
    }
}