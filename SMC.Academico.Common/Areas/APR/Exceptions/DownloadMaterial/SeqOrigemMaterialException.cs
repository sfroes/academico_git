using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class SeqOrigemMaterialException : SMCApplicationException
    {
        public SeqOrigemMaterialException()
            : base(ExceptionsResource.ERR_SeqOrigemMaterialException)
        {
        }
    }
}