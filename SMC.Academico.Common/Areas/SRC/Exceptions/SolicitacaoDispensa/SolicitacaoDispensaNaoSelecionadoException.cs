using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaNaoSelecionadoException : SMCApplicationException
    {
        public SolicitacaoDispensaNaoSelecionadoException()
            : base(ExceptionsResource.ERR_SolicitacaoServicoDispensaNaoSelecionadoException)
        { }
    }
}
