using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{   
    public class EditarTurmaGrupoChancelaIndeferidaException : SMCApplicationException
    {
        public EditarTurmaGrupoChancelaIndeferidaException()
            : base(ExceptionsResource.ERR_EditarTurmaGrupoChancelaIndeferidaException)
        { }
    }
}
