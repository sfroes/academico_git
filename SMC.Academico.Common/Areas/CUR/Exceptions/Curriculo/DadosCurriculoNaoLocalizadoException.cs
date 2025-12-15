using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DadosCurriculoNaoLocalizadoException : SMCApplicationException
    {
        public DadosCurriculoNaoLocalizadoException()
            : base(ExceptionsResource.ERR_DadosCurriculoNaoLocalizadoException)
        { }
    }

}
