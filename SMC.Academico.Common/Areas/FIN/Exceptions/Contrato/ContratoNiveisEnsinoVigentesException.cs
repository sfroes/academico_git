using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.FIN.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class ContratoNiveisEnsinoVigentesException : SMCApplicationException
    {
        public ContratoNiveisEnsinoVigentesException(string status) :
            base(string.Format(ExceptionsResource.ERR_ContratoNiveisEnsinoVigentesException, status))
        {
        }
    }
}
 