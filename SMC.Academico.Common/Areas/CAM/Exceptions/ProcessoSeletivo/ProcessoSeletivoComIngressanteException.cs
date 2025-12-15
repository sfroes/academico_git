using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoComIngressanteException : SMCApplicationException
    {
        public ProcessoSeletivoComIngressanteException(string processoMatricula, string cicloLetivo)
            : base(string.Format(Resources.ExceptionsResource.ERR_ProcessoSeletivoComIngressanteException, processoMatricula, cicloLetivo))
        { }
    }
}
