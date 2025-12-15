using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SalvarGrupoUploadObrigatorioException : SMCApplicationException
    {
        public SalvarGrupoUploadObrigatorioException()
            : base(ExceptionsResource.ERR_SalvarGrupoUploadObrigatorioException)
        {
        }
    }
}
