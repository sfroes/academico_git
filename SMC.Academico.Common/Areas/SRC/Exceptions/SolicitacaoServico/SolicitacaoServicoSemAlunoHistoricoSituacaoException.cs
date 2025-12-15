using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoSemAlunoHistoricoSituacaoException : SMCApplicationException
    {
        public SolicitacaoServicoSemAlunoHistoricoSituacaoException()
            : base(ExceptionsResource.ERR_SolicitacaoServicoSemAlunoHistoricoSituacaoException)
        { }
    }
}
