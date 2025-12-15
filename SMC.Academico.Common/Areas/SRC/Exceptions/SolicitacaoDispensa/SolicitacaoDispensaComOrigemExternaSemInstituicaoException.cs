using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaComOrigemExternaSemInstituicaoException : SMCApplicationException
    {
        public SolicitacaoDispensaComOrigemExternaSemInstituicaoException()
            : base(ExceptionsResource.ERR_SolicitacaoDispensaComOrigemExternaSemInstituicaoException)
        { }
    }
}