using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class FormacaoAcademicaVinculoExigeFormacaoException : SMCApplicationException
    {
        public FormacaoAcademicaVinculoExigeFormacaoException()
            : base(string.Format(ExceptionsResource.ERR_FormacaoAcademicaVinculoExigeFormacaoException))
        {
        }
    }
}