using System;
using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class InstituicaoNivelCursoHoraFimMaiorInicioException : SMCApplicationException
    {
        public InstituicaoNivelCursoHoraFimMaiorInicioException()
            : base(ExceptionsResource.ERR_InstituicaoNivelCursoHoraFimMaiorInicioException)
        { }
    }
}