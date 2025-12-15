using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class RelatorioSemSelecionarAlunoException : Exception
    {
        public RelatorioSemSelecionarAlunoException()
            : base(Resources.ExceptionsResource.ERR_RelatorioSemSelecionarAlunoException)
        { }
    }
}