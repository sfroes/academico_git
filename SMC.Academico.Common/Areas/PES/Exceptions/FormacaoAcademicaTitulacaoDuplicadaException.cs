using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class FormacaoAcademicaTitulacaoDuplicadaException : SMCApplicationException
    {
        public FormacaoAcademicaTitulacaoDuplicadaException()
            : base(string.Format(ExceptionsResource.ERR_FormacaoAcademicaTitulacaoDuplicadaException))
        {
        }
    }
}