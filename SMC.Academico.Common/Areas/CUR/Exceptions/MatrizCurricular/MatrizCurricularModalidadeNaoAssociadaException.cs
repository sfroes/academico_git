using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class MatrizCurricularModalidadeNaoAssociadaException : SMCApplicationException
    {
        public MatrizCurricularModalidadeNaoAssociadaException()
            : base(ExceptionsResource.ERR_MatrizCurricularModalidadeNaoAssociada)
        {
        }
    }
}
