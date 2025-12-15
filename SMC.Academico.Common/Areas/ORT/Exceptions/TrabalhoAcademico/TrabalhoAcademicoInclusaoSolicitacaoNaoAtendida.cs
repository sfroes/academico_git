using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoInclusaoSolicitacaoNaoAtendida : SMCApplicationException
    {
        public TrabalhoAcademicoInclusaoSolicitacaoNaoAtendida()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoInclusaoSolicitacaoNaoAtendida)
        {
        }
    }
}