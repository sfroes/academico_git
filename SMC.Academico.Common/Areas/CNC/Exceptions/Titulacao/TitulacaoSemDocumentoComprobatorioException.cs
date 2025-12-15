using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class TitulacaoSemDocumentoComprobatorioException : SMCApplicationException
    {
        public TitulacaoSemDocumentoComprobatorioException()
            : base(ExceptionsResource.ERR_TitulacaoSemDocumentoComprobatorioException)
        { }
    }
}
