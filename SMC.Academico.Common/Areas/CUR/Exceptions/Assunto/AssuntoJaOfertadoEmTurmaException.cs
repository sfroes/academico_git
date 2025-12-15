using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class AssuntoJaOfertadoEmTurmaException : SMCApplicationException
    {
        public AssuntoJaOfertadoEmTurmaException(string nomeCurso, string descUnidade, string descTurno)
            : base(string.Format(ExceptionsResource.ERR_AssuntoJaOfertadoEmTurmaException, nomeCurso, descUnidade, descTurno))
        {
        }
    }
}