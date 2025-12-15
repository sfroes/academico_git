using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class NovaDataPrevisaoAnteriorDataAtualException : SMCApplicationException
    {
        public NovaDataPrevisaoAnteriorDataAtualException()
            : base(ExceptionsResource.ERR_NovaDataPrevisaoAnteriorDataAtualException)
        { }
    }
}
