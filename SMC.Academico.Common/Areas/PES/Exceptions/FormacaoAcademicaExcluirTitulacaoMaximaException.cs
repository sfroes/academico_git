using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class FormacaoAcademicaExcluirTitulacaoMaximaException : SMCApplicationException
    {
        public FormacaoAcademicaExcluirTitulacaoMaximaException()
            : base(string.Format(ExceptionsResource.ERR_FormacaoAcademicaExcluirTitulacaoMaximaException))
        {
        }
    }
}