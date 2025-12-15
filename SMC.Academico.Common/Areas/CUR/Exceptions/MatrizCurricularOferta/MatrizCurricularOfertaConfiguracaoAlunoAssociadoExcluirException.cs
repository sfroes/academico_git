using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.CUR.Exceptions.MatrizCurricularOferta
{
    public class MatrizCurricularOfertaConfiguracaoAlunoAssociadoExcluirException : SMCApplicationException
    {
        public MatrizCurricularOfertaConfiguracaoAlunoAssociadoExcluirException(List<string> matrizes)
            : base(string.Format(ExceptionsResource.ERR_MatrizCurricularOfertaConfiguracaoAlunoAssociadoExcluirException, string.Join("<br />", matrizes)))
        { }
    }
}