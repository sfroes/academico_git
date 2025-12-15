using SMC.Academico.Common.Areas.FIN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class MotivoAlteracaoBeneficioNaoCadastradoException : SMCApplicationException
    {
        public MotivoAlteracaoBeneficioNaoCadastradoException(string token)
            : base(string.Format(ExceptionsResource.ERR_MotivoAlteracaoBeneficioNaoCadastradoException, token))
        {
        }
    }
}