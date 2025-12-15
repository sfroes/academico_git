using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeEnderecoEletronicoObrigatorioException : SMCApplicationException
    {
        public EntidadeEnderecoEletronicoObrigatorioException(string descricaoTipoEnderecoEletronico)
            : base(string.Format(ExceptionsResource.ERR_EntidadeEnderecoEletronicoObrigatorioException, descricaoTipoEnderecoEletronico.ToLower()))
        {
        }
    }
}