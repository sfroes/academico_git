using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoGPISemEtapaInscricaoException : SMCApplicationException
    {
        public ProcessoGPISemEtapaInscricaoException()
            : base(ExceptionsResource.ERR_ProcessoGPISemEtapaInscricaoException)
        { }
    }
}
