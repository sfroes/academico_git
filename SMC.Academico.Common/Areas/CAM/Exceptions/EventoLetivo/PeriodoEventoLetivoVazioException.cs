using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class PeriodoEventoLetivoVazioException : SMCApplicationException
    {
        public PeriodoEventoLetivoVazioException(string args)
            : base(string.Format(Resources.ExceptionsResource.ERR_PeriodoEventoLetivoVazioException, args))
        { }
    }
}
