using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaQuantidadeVagasMenorVagasOcupadasException : SMCApplicationException
    {
        public TurmaQuantidadeVagasMenorVagasOcupadasException(string divisoes)
            : base(string.Format(ExceptionsResource.ERR_TurmaQuantidadeVagasMenorVagasOcupadasException, divisoes))
        {
        }
    }
}