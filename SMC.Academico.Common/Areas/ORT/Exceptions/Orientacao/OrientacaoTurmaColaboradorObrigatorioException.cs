using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrientacaoTurmaColaboradorObrigatorioException : SMCApplicationException
    {
        public OrientacaoTurmaColaboradorObrigatorioException()
            : base(ExceptionsResource.ERR_OrientacaoTurmaColaboradorObrigatorioException)
        {
        }
    }
}