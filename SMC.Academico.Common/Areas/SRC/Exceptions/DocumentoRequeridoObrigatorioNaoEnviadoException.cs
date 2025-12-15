using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class DocumentoRequeridoObrigatorioNaoEnviadoException : SMCApplicationException
    {
        public DocumentoRequeridoObrigatorioNaoEnviadoException(string grupos) 
            : base(string.Format(ExceptionsResource.ERR_DocumentoRequeridoObrigatorioNaoEnviadoException, 
                        !string.IsNullOrEmpty(grupos) ?
                            string.Format(ExceptionsResource.ERR_DocumentoRequeridoObrigatorioNaoEnviadoExceptionGrupos, grupos) :
                            string.Empty
                        )
                )
        { }
    }
}
