using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.APR.Exceptions.LancamentoHistoricoEscolar
{
    public class LancamentoHistoricoEscolarTurmaPermiteAvaliacaoParcialException : SMCApplicationException
    {
        public LancamentoHistoricoEscolarTurmaPermiteAvaliacaoParcialException()
            : base(ExceptionsResource.ERR_LancamentoHistoricoEscolarTurmaPermiteAvaliacaoParcialException)
        {
        }
    }
}
