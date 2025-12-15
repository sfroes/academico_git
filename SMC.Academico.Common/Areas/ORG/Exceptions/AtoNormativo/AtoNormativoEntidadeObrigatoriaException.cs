using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class AtoNormativoEntidadeObrigatoriaException : SMCApplicationException
    {
        public AtoNormativoEntidadeObrigatoriaException()
            : base(ExceptionsResource.ERR_AtoNormativoEntidadeObrigatoriaException)
        { }
    }
}