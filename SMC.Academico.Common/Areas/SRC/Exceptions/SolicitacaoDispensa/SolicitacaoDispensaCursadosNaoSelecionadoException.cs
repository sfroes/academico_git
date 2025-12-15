using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaCursadosNaoSelecionadoException : SMCApplicationException
    {
        public SolicitacaoDispensaCursadosNaoSelecionadoException()
            : base(ExceptionsResource.ERR_SolicitacaoServicoDispensaCursadosNaoSelecionadoException)
        { }
    }
}
