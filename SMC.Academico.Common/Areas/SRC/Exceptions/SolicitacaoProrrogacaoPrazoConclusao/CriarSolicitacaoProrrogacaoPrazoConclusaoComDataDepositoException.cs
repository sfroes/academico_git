using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class CriarSolicitacaoProrrogacaoPrazoConclusaoComDataDepositoException : SMCApplicationException
    {
        public CriarSolicitacaoProrrogacaoPrazoConclusaoComDataDepositoException(string titulo, DateTime dataDeposito)
            : base(string.Format(ExceptionsResource.ERR_CriarSolicitacaoProrrogacaoPrazoConclusaoComDataDepositoException, titulo, dataDeposito.ToShortDateString()))
        { }
    }
}