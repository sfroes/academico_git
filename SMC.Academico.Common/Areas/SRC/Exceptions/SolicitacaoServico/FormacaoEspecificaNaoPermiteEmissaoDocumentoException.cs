using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoServico
{
    public class FormacaoEspecificaNaoPermiteEmissaoDocumentoException : SMCApplicationException
    {
        public FormacaoEspecificaNaoPermiteEmissaoDocumentoException(string descricaoTipoDocumentoAcademico)
            : base(string.Format(ExceptionsResource.ERR_FormacaoEspecificaNaoPermiteEmissaoDocumentoException, descricaoTipoDocumentoAcademico))
        {
        }
    }
}