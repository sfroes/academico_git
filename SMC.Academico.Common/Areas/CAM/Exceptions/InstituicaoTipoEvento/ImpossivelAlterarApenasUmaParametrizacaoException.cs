using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ImpossivelAlterarApenasUmaParametrizacaoException : SMCApplicationException
    {
        public ImpossivelAlterarApenasUmaParametrizacaoException()
            : base(ExceptionsResource.ERR_ImpossivelAlterarApenasUmaParametrizacaoException)
        {
        }
    }
}