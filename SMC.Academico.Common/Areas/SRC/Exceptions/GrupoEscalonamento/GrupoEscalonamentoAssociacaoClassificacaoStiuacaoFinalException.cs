using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoAssociacaoClassificacaoStiuacaoFinalException : SMCApplicationException
    {
        public GrupoEscalonamentoAssociacaoClassificacaoStiuacaoFinalException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoAssociacaoClassificacaoStiuacaoFinalException)
        {
        }
    }
}