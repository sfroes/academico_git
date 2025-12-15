using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{   
    public class AtividadeAcademicaComHistoricoEscolarException : SMCApplicationException
    {
        public AtividadeAcademicaComHistoricoEscolarException(List<string> atividades)
            : base(string.Format(ExceptionsResource.ERR_AtividadeAcademicaComHistoricoEscolarException, string.Join("<br/>", atividades)))
        {
        }
    }
}
