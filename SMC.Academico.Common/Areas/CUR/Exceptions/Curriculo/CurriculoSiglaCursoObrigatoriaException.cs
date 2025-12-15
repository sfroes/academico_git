using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class CurriculoSiglaCursoObrigatoriaException : SMCApplicationException
    {
        public CurriculoSiglaCursoObrigatoriaException()
            : base(ExceptionsResource.ERR_CurriculoSiglaCursoObrigatoriaException)
        {
        }
    }
}