using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SalvarGrupoQuantidadeDocumentosAssociadosException : SMCApplicationException
    {
        public SalvarGrupoQuantidadeDocumentosAssociadosException()
            : base(ExceptionsResource.ERR_SalvarGrupoQuantidadeDocumentosAssociadosException)
        {
        }
    }
}
