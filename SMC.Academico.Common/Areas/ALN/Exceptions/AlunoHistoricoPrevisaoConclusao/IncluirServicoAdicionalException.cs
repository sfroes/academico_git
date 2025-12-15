using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IncluirServicoAdicionalException : SMCApplicationException
    {
        public IncluirServicoAdicionalException(string erroGRA)
            : base(string.Format(ExceptionsResource.ERR_IncluirServicoAdicionalException, erroGRA))
        { }
    }
}
