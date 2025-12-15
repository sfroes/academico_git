using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions.AplicacaoAvaliacao
{
    public class AplicacaoAvaliacaoMembrosBancaFaltantesException : SMCApplicationException
    {
        public AplicacaoAvaliacaoMembrosBancaFaltantesException()
            : base(ExceptionsResource.ERR_AplicacaoAvaliacaoMembrosBancaFaltantesException)
        {
        }
    }
}