using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class InclusoaMatriculaAcademicoException : Framework.Exceptions.SMCApplicationException
    {
        public InclusoaMatriculaAcademicoException()
            : base(Resources.ExceptionsResource.ERR_InclusoaMatriculaAcademicoException)
        { }
    }
     
}
