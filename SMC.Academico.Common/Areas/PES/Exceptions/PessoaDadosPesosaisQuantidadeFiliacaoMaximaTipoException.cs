using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaDadosPesosaisQuantidadeFiliacaoMaximaTipoException : SMCApplicationException
    {
        public PessoaDadosPesosaisQuantidadeFiliacaoMaximaTipoException(int quantidadeRegistrosFiliacao, string tipoFiliacao)
            : base(string.Format(ExceptionsResource.ERR_QuantidadeRegistroFiliacaoMaximaTipo, quantidadeRegistrosFiliacao, tipoFiliacao))
        {
        }
    }
}