using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class RestricaoSolicitacaoSimultaneaParaAlunoException : SMCApplicationException
    {
        public RestricaoSolicitacaoSimultaneaParaAlunoException(string nomeAluno, List<string> protocolos)
            : base (string.Format(ExceptionsResource.ERR_RestricaoSolicitacaoSimultaneaParaAlunoException, nomeAluno, string.Join("; ", protocolos)))
        { }
    }
}