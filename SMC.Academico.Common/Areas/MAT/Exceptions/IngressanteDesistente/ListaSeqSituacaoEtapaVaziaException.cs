using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ListaSeqSituacaoEtapaVaziaException : SMCApplicationException
    {
        public ListaSeqSituacaoEtapaVaziaException()
            : base(ExceptionsResource.ERR_ListaSeqSituacaoEtapaVaziaException)
        {
        }
    }
}