using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions.Turma
{
    public class TurmaExcluirGrupoComEventoOuMatriculaException : SMCApplicationException
    {
        public TurmaExcluirGrupoComEventoOuMatriculaException(string turmas) 
             : base(string.Format(ExceptionsResource.ERR_TurmaExcluirGrupoComEventoOuMatriculaException, turmas))
        {}
    }
}