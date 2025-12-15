using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class MensagemConcidentesException : SMCApplicationException
    {
        public MensagemConcidentesException()
            : base(ExceptionsResource.ERR_MensagemConcidentesException)
        { }
    }
}