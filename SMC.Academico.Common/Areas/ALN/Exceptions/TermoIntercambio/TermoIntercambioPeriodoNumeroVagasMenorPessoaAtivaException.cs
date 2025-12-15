using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TermoIntercambioPeriodoNumeroVagasMenorPessoaAtivaException : SMCApplicationException
    {
        public TermoIntercambioPeriodoNumeroVagasMenorPessoaAtivaException()
            : base(Resources.ExceptionsResource.ERR_TermoIntercambioPeriodoNumeroVagasMenorPessoaAtivaException)
        { }
    }
}

