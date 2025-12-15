using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class PlanoEstudoSemItensException : SMCApplicationException
    {
        public PlanoEstudoSemItensException(string tela)
            : base(string.Format(ExceptionsResource.ERR_PlanoEstudoSemItensException, tela))
        {
        }
    }
}