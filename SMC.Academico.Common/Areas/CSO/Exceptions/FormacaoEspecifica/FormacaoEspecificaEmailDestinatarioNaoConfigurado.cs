using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class FormacaoEspecificaEmailDestinatarioNaoConfigurado : SMCApplicationException
    {
        public FormacaoEspecificaEmailDestinatarioNaoConfigurado()
            : base(ExceptionsResource.ERR_FormacaoEspecificaEmailDestinatarioNaoConfigurado)
        { }
    }
}