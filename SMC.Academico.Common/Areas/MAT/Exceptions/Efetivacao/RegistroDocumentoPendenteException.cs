using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class RegistroDocumentoPendenteException : SMCApplicationException
    {
        public RegistroDocumentoPendenteException()
            : base(ExceptionsResource.ERR_RegistroDocumentoPendenteException)
        {
        }
    }
}