using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaComponenteCurricularAprovadoException : SMCApplicationException
    {
        public SolicitacaoDispensaComponenteCurricularAprovadoException(string componentes) 
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoDispensaComponenteCurricularAprovadoException, componentes))
        {
        }
    }
}