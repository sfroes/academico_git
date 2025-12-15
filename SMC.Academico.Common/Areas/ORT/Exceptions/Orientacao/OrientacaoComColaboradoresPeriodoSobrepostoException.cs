using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrientacaoComColaboradoresPeriodoSobrepostoException : SMCApplicationException
    {
        public OrientacaoComColaboradoresPeriodoSobrepostoException(string status)
            : base(string.Format(ExceptionsResource.ERR_OrientacaoComColaboradoresPeriodoSobrepostoException, status))
        {
        }
    }
}