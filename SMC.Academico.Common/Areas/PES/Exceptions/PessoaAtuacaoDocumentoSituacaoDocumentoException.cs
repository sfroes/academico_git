using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoDocumentoSituacaoDocumentoException : SMCApplicationException
    {
        public PessoaAtuacaoDocumentoSituacaoDocumentoException(string descricaoServico)
            : base(string.Format(ExceptionsResource.ERR_PessoaAtuacaoDocumentoSituacaoDocumentoException, descricaoServico))
        {     
        }
    }
}
