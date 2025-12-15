using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaSemCursoCanceladoInvalidoException : SMCApplicationException
    {
        public TurmaSemCursoCanceladoInvalidoException()
            : base(ExceptionsResource.ERR_TurmaSemCursoCanceladoInvalidoException)
        {
        }
    }
}