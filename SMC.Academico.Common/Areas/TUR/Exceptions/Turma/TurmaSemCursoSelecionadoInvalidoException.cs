using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaSemCursoSelecionadoInvalidoException : SMCApplicationException
    {
        public TurmaSemCursoSelecionadoInvalidoException()
            : base(ExceptionsResource.ERR_TurmaSemCursoSelecionadoInvalidoException)
        {
        }
    }
}
