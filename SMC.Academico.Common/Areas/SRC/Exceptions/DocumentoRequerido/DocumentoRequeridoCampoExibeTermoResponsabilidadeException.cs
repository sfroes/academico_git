using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class DocumentoRequeridoCampoExibeTermoResponsabilidadeException : SMCApplicationException
    {
        public DocumentoRequeridoCampoExibeTermoResponsabilidadeException()
             : base(ExceptionsResource.ERR_DocumentoRequeridoExibeTermoResponsabilidadeException)
        { }
    }
}
