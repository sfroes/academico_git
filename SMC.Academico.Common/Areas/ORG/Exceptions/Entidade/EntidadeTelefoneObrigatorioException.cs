using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeTelefoneObrigatorioException : SMCApplicationException
    {
        public EntidadeTelefoneObrigatorioException(string descricaoTipoTelefone)
            : base(string.Format(ExceptionsResource.ERR_EntidadeTelefoneObrigatorioException, descricaoTipoTelefone.ToLower()))
        {
        }
    }
}