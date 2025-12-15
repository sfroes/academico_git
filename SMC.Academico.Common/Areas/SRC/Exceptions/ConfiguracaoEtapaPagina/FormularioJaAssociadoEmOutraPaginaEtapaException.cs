using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;


namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class FormularioJaAssociadoEmOutraPaginaEtapaException : SMCApplicationException
    {
        public FormularioJaAssociadoEmOutraPaginaEtapaException()
            : base(ExceptionsResource.ERR_FormularioJaAssociadoEmOutraPaginaEtapaException)
        {
        }
    }
}
