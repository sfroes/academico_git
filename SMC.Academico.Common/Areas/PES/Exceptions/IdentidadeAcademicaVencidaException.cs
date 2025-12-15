using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class IdentidadeAcademicaVencidaException : SMCApplicationException
    {
        public IdentidadeAcademicaVencidaException(IEnumerable<string> identidades)
            : base(string.Format(ExceptionsResource.ERR_IdentidadeAcademicaVencidaException, string.Join("<br />- ", identidades)))
        {
        }
    }
}