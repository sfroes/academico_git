using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SalvarGrupoUploadNaoObrigatorioException : SMCApplicationException
    {
        public SalvarGrupoUploadNaoObrigatorioException()
            : base(ExceptionsResource.ERR_SalvarGrupoUploadNaoObrigatorioException)
        {
        }
    }
}
