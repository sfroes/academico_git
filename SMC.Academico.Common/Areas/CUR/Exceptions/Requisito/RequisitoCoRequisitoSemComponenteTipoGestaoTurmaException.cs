using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class RequisitoCoRequisitoSemComponenteTipoGestaoTurmaException : SMCApplicationException
    {
        public RequisitoCoRequisitoSemComponenteTipoGestaoTurmaException()
            : base(ExceptionsResource.ERR_RequisitoCoRequisitoSemComponenteTipoGestaoTurmaException)
        {
        }
    }
}