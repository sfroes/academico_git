
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class ExisteFuncionarioComMesmoTipoDeVinculoVigenteException : SMCApplicationException
    {
        public ExisteFuncionarioComMesmoTipoDeVinculoVigenteException(string descricaoTipoVinculo)
            : base(string.Format(SMC.Academico.Common.Areas.PES.Resources.ExceptionsResource.ERR_ExisteFuncionarioComMesmoTipoDeVinculoVigenteException, descricaoTipoVinculo))
        {
        }
    }
}
