using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoRegistroDocumentoRequeridoNaoExisteException : SMCApplicationException
    {
        public PessoaAtuacaoRegistroDocumentoRequeridoNaoExisteException(string descricaoSolicitacaoServico)
            : base(string.Format(ExceptionsResource.ERR_PessoaAtuacaoRegistroDocumentoRequeridoNaoExiste, descricaoSolicitacaoServico))
        {
        }
    }
}