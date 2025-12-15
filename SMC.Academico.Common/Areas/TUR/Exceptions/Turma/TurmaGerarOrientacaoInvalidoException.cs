using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaGerarOrientacaoInvalidoException : SMCApplicationException
    {
        public TurmaGerarOrientacaoInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_TurmaGerarOrientacaoInvalidoException, registros))
        {
        }
    }
}
