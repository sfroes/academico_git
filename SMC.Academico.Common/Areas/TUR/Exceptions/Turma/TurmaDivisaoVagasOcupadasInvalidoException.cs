using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{  
    public class TurmaDivisaoVagasOcupadasInvalidoException : SMCApplicationException
    {
        public TurmaDivisaoVagasOcupadasInvalidoException()
            : base(ExceptionsResource.ERR_TurmaDivisaoVagasOcupadasInvalidoException)
        {
        }
    }
}
