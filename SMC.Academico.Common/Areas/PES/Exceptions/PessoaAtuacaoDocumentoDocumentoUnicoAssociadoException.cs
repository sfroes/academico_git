using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Util;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaAtuacaoDocumentoDocumentoUnicoAssociadoException : SMCApplicationException
    {
        public PessoaAtuacaoDocumentoDocumentoUnicoAssociadoException(string descricaoTipoDocumento)
            : base(string.Format(ExceptionsResource.ERR_PessoaAtuacaoDocumentoDocumentoUnicoAssociadoException, descricaoTipoDocumento))
        {     
        }
    }
}
