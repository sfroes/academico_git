using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaDadosPesosaisQuantidadeFiliacaoMinimaException : SMCApplicationException
    {
        public PessoaDadosPesosaisQuantidadeFiliacaoMinimaException(int quantidadeRegistrosFiliacao)
            : base(string.Format(ExceptionsResource.ERR_QuantidadeRegistroFiliacaoMinima, quantidadeRegistrosFiliacao))
        {
        }
    }
}