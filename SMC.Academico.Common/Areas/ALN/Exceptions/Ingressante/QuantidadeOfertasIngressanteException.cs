using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class QuantidadeOfertasIngressanteException : SMCApplicationException
    {
        public QuantidadeOfertasIngressanteException(string vinculo) 
            : base(string.Format(Resources.ExceptionsResource.ERR_QuantidadeOfertasIngressanteException, vinculo))
        { }
    }
}
