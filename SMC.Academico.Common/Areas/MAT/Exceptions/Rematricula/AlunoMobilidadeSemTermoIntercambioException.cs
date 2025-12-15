using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class AlunoMobilidadeSemTermoIntercambioException : SMCApplicationException
    {
        public AlunoMobilidadeSemTermoIntercambioException(string nomeAluno)
            : base(string.Format(ExceptionsResource.ERR_AlunoMobilidadeSemTermoIntercambioException, nomeAluno))
        { }
    }
}