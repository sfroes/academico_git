using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class InstituicaoNivelTurnoNaoAssociadoException : SMCApplicationException
    {
        public InstituicaoNivelTurnoNaoAssociadoException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTurnoNaoAssociadoException)
        { }
    }
}