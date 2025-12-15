using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class SituacaoAlunoFormadoSemCicloAtualException : SMCApplicationException
    {
        public SituacaoAlunoFormadoSemCicloAtualException(string data)
            : base(string.Format(ExceptionsResource.ERR_SituacaoAlunoFormadoSemCicloAtualException, data))
        {
            
        }
    }
}