using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class FuncionarioVinculoTipoObrigatorioException : SMCApplicationException
    {
        public FuncionarioVinculoTipoObrigatorioException(string tipoObrigatorio)
            : base(string.Format(ExceptionsResource.ERR_FuncionarioVinculoTipoObrigatorioException, tipoObrigatorio))
        {
        }
    }
}