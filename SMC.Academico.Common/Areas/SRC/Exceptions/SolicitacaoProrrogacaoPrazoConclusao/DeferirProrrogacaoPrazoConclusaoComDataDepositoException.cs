using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class DeferirProrrogacaoPrazoConclusaoComDataDepositoException : SMCApplicationException
    {
        public DeferirProrrogacaoPrazoConclusaoComDataDepositoException(string titulo, DateTime dataDeposito)
            : base(string.Format(ExceptionsResource.ERR_DeferirProrrogacaoPrazoConclusaoComDataDepositoException, titulo, dataDeposito.ToShortDateString()))
        { }
    }
}