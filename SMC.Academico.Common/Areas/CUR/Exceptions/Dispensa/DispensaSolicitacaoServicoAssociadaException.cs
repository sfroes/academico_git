using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DispensaSolicitacaoServicoAssociadaException : SMCApplicationException
    {
        public DispensaSolicitacaoServicoAssociadaException()
            : base(ExceptionsResource.ERR_DispensaSolicitacaoServicoAssociadaException)
        {
        }
    }
}