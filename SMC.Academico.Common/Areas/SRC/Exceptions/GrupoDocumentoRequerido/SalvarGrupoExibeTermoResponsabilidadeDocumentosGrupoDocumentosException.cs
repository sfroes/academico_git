using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SalvarGrupoExibeTermoResponsabilidadeDocumentosGrupoDocumentosException : SMCApplicationException
    {
        public SalvarGrupoExibeTermoResponsabilidadeDocumentosGrupoDocumentosException()
        : base(ExceptionsResource.ERR_SalvarGrupoExibeTermoResponsabilidadeDocumentosGrupoDocumentosException)
        {}
    }
}