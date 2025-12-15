using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class InstituicaoNivelModalidadeNaoAssociadoException : SMCApplicationException
    {
        public InstituicaoNivelModalidadeNaoAssociadoException(string nivelEnsino)
            : base(string.Format(ExceptionsResource.ERR_InstituicaoNivelModalidadeNaoAssociadoException, nivelEnsino))
        { }
    }
}