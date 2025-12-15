using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaIgualSelecionadaInvalidoException : SMCApplicationException
    {
        public TurmaIgualSelecionadaInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_TurmaIgualSelecionadaInvalidoException, registros))
        {
        }
    }
}
