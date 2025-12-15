using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeEnderecoObrigatorioException : SMCApplicationException
    {
        public EntidadeEnderecoObrigatorioException(string descricaoTipoEndereco)
            : base(string.Format(ExceptionsResource.ERR_EntidadeEnderecoObrigatorioException, descricaoTipoEndereco.ToLower()))
        {
        }
    }
}