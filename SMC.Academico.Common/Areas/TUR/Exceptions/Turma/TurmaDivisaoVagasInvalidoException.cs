using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{  
    public class TurmaDivisaoVagasInvalidoException : SMCApplicationException
    {
        public TurmaDivisaoVagasInvalidoException()
            : base(ExceptionsResource.ERR_TurmaDivisaoVagasInvalidoException)
        {
        }
    }
}
