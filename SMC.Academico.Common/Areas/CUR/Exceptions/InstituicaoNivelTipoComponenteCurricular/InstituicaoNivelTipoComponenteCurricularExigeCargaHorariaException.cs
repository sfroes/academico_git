using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class InstituicaoNivelTipoComponenteCurricularExigeCargaHorariaException : SMCApplicationException
    {
        public InstituicaoNivelTipoComponenteCurricularExigeCargaHorariaException()
            : base(ExceptionsResource.ERR_InstituicaoNivelTipoComponenteCurricularExigeCargaHoraria)
        {
        }
    }
}