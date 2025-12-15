using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaTitulacaoSomenteUmaAssociadaAtivaException : SMCApplicationException
    {
        public CursoFormacaoEspecificaTitulacaoSomenteUmaAssociadaAtivaException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaTitulacaoSomenteUmaAssociadaAtivaException)
        { }
    }
}
