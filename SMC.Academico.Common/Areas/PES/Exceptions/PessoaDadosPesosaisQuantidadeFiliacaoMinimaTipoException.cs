using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaDadosPesosaisQuantidadeFiliacaoMinimaTipoException : SMCApplicationException
    {
        public PessoaDadosPesosaisQuantidadeFiliacaoMinimaTipoException(int quantidadeRegistrosFiliacao, string tipoFiliacao)
            : base(string.Format(ExceptionsResource.ERR_QuantidadeRegistroFiliacaoMinimaTipo, quantidadeRegistrosFiliacao, tipoFiliacao))
        {
        }
    }
}