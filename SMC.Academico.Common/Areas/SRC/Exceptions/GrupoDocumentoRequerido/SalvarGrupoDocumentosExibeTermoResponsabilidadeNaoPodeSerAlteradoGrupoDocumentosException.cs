using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SalvarGrupoDocumentosExibeTermoResponsabilidadeNaoPodeSerAlteradoGrupoDocumentosException : SMCApplicationException
    {
        public SalvarGrupoDocumentosExibeTermoResponsabilidadeNaoPodeSerAlteradoGrupoDocumentosException()
            : base(ExceptionsResource.ERR_SalvarGrupoDocumentosExibeTermoResponsabilidadeNaoPodeSerAlteradoGrupoDocumentosException)
        {
        }
    }
}
