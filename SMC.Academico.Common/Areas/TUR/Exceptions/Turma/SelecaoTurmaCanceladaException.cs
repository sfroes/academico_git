using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;
using SMC.Framework.Util;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.TUR.Exceptions.Turma
{
    public class SelecaoTurmaCanceladaException : SMCApplicationException
    {
        public SelecaoTurmaCanceladaException(IEnumerable<string> turmas)
            : base(string.Format(ExceptionsResource.ERR_SelecaoTurmaCanceladaException, string.Join("<br />", turmas)))
        {
        }
    }
}