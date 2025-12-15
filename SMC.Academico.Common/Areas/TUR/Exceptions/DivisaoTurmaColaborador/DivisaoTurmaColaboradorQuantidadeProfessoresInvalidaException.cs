using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class DivisaoTurmaColaboradorQuantidadeProfessoresInvalidaException : SMCApplicationException
    {
        public DivisaoTurmaColaboradorQuantidadeProfessoresInvalidaException(short? qtdProfessores)
            : base(string.Format(ExceptionsResource.ERR_DivisaoTurmaColaboradorQuantidadeProfessoresInvalidaException, qtdProfessores))
        {
        }
    }
}
