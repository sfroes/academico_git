using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoComOfertasException : SMCApplicationException
    {
        public ProcessoSeletivoComOfertasException()
            : base(Resources.ExceptionsResource.ERR_ProcessoSeletivoComOfertasException)
        { }
    }
}
