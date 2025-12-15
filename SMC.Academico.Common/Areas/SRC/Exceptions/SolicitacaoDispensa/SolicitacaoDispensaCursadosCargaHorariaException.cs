using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaCursadosCargaHorariaException : SMCApplicationException
    {
        public SolicitacaoDispensaCursadosCargaHorariaException(string percentual)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoDispensaCursadosCargaHorariaException, percentual))
        {
        }
    }
}