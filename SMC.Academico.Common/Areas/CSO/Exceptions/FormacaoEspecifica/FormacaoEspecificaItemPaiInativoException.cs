using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class FormacaoEspecificaItemPaiInativoException : SMCApplicationException
    {
        public FormacaoEspecificaItemPaiInativoException()
            : base(ExceptionsResource.ERR_FormacaoEspecificaItemPaiInativoException)
        { }
    }
}