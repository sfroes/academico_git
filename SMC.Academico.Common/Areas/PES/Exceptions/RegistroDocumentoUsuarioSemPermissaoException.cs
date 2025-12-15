using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class RegistroDocumentoPessoaAtuacaoUsuarioSemPermissaoException : SMCApplicationException
    {
        public RegistroDocumentoPessoaAtuacaoUsuarioSemPermissaoException()
            : base(ExceptionsResource.ERR_RegistroDocumentoPessoaAtuacaoUsuarioSemPermissao)
        {
        }
    }
}