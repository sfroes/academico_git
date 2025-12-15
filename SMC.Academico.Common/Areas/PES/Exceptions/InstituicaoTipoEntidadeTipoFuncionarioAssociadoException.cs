using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class InstituicaoTipoEntidadeTipoFuncionarioAssociadoException : SMCApplicationException
    {
        public InstituicaoTipoEntidadeTipoFuncionarioAssociadoException()
            : base(ExceptionsResource.ERR_InstituicaoTipoEntidadeVinculoJaAssociadoException)
        { }
    }
}