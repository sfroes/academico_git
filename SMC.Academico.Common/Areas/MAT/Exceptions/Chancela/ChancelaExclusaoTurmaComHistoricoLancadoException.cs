using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.MAT.Exceptions.Chancela
{
    public class ChancelaExclusaoTurmaComHistoricoLancadoException : SMCApplicationException
    {
        public ChancelaExclusaoTurmaComHistoricoLancadoException(List<string> turmas)
            : base(string.Format(ExceptionsResource.ERR_ChancelaExclusaoTurmaComHistoricoLancadoException, string.Join("<br/>", turmas)))
        { }
    }
}