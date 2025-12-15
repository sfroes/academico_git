using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorIntervaloDatasIncompletoException : SMCApplicationException
    {
        public ColaboradorIntervaloDatasIncompletoException()
            : base(ExceptionsResource.ERR_ColaboradorIntervaloDatasIncompletoException)
        {
        }
    }
}