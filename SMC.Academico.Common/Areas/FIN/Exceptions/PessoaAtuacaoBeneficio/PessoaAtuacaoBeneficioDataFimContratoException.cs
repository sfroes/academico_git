using SMC.Academico.Common.Areas.FIN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class PessoaAtuacaoBeneficioDataFimContratoException : SMCApplicationException
    {
        public PessoaAtuacaoBeneficioDataFimContratoException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoBeneficioDataFimContratoException)
        {
        }
    }
}