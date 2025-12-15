using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoAlunoComSituacaoNaoPermitidaDataDefesaException : SMCApplicationException
    {
        public TrabalhoAcademicoAlunoComSituacaoNaoPermitidaDataDefesaException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoAlunoComSituacaoNaoPermitidaDataDefesa)
        {
        }
    }
}