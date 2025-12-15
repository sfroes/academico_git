using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class AtoNormativoCamposZeradosException : SMCApplicationException
    {
        public AtoNormativoCamposZeradosException()
            : base(ExceptionsResource.ERR_AtoNormativoCamposZeradosException)
        { }
    }
}