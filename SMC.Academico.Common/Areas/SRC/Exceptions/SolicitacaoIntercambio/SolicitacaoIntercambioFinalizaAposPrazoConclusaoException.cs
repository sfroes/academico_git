using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoIntercambioFinalizaAposPrazoConclusaoException : SMCApplicationException
    {
        public SolicitacaoIntercambioFinalizaAposPrazoConclusaoException(DateTime dataPrevisao)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoIntercambioFinalizaAposPrazoConclusaoException, dataPrevisao.ToShortDateString()))
        { }
    }
}