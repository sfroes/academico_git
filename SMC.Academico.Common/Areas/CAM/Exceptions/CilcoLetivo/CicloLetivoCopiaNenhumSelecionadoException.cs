using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class CicloLetivoCopiaNenhumSelecionadoException : SMCApplicationException
    {
        public CicloLetivoCopiaNenhumSelecionadoException()
            : base(ExceptionsResource.ERR_ObrigatoriedadeCopiaCiclo)
        {
        }
    }
}