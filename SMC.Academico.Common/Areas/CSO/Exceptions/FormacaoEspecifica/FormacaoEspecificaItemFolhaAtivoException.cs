using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class FormacaoEspecificaItemFolhaAtivoException : SMCApplicationException
    {
        public FormacaoEspecificaItemFolhaAtivoException()
            : base(ExceptionsResource.ERR_FormacaoEspecificaItemFolhaAtivoException)
        { }
    }
}