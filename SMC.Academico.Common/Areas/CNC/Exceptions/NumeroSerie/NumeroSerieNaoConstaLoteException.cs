using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class NumeroSerieNaoConstaLoteException : SMCApplicationException
    {
        public NumeroSerieNaoConstaLoteException()
            : base(ExceptionsResource.ERR_NumeroSerieNaoConstaLoteException)
        { }
    }
}