using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class TipoEntidadeNaoAssociadoInstituicaoException : SMCApplicationException
    {
        public TipoEntidadeNaoAssociadoInstituicaoException()
            : base(Resources.ExceptionsResource.ERR_TipoEntidadeNaoAssociadoInstituicaoException)
        {
        }
    }
}