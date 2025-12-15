using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoReaberturaMatricula
{
    public class SolicitacaoReaberturaAlunoSemOfertaMatrizException : SMCApplicationException
    {
        public SolicitacaoReaberturaAlunoSemOfertaMatrizException()
            : base(ExceptionsResource.ERR_SolicitacaoReaberturaAlunoSemOfertaMatrizException)
        { }
    }
}