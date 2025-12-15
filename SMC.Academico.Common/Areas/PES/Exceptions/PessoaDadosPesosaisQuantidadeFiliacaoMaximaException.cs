using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaDadosPesosaisQuantidadeFiliacaoMaximaException : SMCApplicationException
    {
        public PessoaDadosPesosaisQuantidadeFiliacaoMaximaException(int quantidadeRegistrosFiliacao)
            : base(string.Format(ExceptionsResource.ERR_QuantidadeRegistroFiliacaoMaxima, quantidadeRegistrosFiliacao))
        {
        }
    }
}