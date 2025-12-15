using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.CUR.Exceptions.MatrizCurricularOferta
{
    public class MatrizCurricularOfertaAlunoAssociadoExcluirException : SMCApplicationException
    {
        public MatrizCurricularOfertaAlunoAssociadoExcluirException(List<string> matrizes)
            : base(string.Format(ExceptionsResource.ERR_MatrizCurricularOfertaAlunoAssociadoExcluirException, string.Join("<br />", matrizes)))
        { }
    }
}