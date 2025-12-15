using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ExclusaoTurmaComHistoricoEscolarException : SMCApplicationException
    {
        public ExclusaoTurmaComHistoricoEscolarException()
            : base(ExceptionsResource.ERR_ExclusaoTurmaComHistoricoEscolarException)
        { }
    }
}