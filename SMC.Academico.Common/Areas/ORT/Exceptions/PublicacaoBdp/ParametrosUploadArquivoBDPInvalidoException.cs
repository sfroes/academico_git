using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class ParametrosUploadArquivoBDPInvalidoException : SMCApplicationException
    {
        public ParametrosUploadArquivoBDPInvalidoException(string parametro)
            : base(string.Format(ExceptionsResource.ERR_ParametrosUploadArquivoBDPInvalidoException))
        {
        }
    }
}
