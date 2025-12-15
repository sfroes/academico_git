using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class FormacaoEspecificaVigenteEmUmCursoException : SMCApplicationException
    {
        public FormacaoEspecificaVigenteEmUmCursoException()
            : base(ExceptionsResource.ERR_FormacaoEspecificaVigenteEmUmCursoException)
        { }
    }
}