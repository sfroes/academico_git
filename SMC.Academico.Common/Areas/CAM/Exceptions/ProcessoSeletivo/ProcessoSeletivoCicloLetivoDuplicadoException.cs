using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoCicloLetivoDuplicadoException : SMCApplicationException
    {
        public ProcessoSeletivoCicloLetivoDuplicadoException() 
            : base(Resources.ExceptionsResource.ERR_ProcessoSeletivoCicloLetivoDuplicadoException)
        { }
    }
}
