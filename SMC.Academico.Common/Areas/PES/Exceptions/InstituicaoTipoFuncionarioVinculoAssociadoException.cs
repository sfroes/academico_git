using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class InstituicaoTipoFuncionarioVinculoAssociadoException : SMCApplicationException
    {
        public InstituicaoTipoFuncionarioVinculoAssociadoException()
            : base(ExceptionsResource.ERR_FuncionarioVinculoJaAssociadoException)
        { }
    }
}