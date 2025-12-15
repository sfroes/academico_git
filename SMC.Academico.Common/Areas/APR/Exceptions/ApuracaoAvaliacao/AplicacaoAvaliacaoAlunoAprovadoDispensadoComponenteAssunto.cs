using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class AplicacaoAvaliacaoAlunoAprovadoDispensadoComponenteAssunto : SMCApplicationException
    {
        public AplicacaoAvaliacaoAlunoAprovadoDispensadoComponenteAssunto(string nome)
            : base(string.Format(ExceptionsResource.ERR_AplicacaoAvaliacaoAlunoAprovadoDispensadoComponenteAssuntoException, nome))
        {
        }
    }
}