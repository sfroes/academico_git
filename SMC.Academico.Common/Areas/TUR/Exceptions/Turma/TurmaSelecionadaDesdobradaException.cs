using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaSelecionadaDesdobradaException : SMCApplicationException
    {
        public TurmaSelecionadaDesdobradaException(string registros)
            : base(string.Format(ExceptionsResource.ERR_TurmaSelecionadaDesdobradaException, registros))
        {
        }
    }
}
