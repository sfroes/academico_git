using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TiposParticipacaoOrientacaoObrigatoriosException : SMCApplicationException
    {
        public TiposParticipacaoOrientacaoObrigatoriosException(IEnumerable<string> tiposParticipacao)
            : base(string.Format(ExceptionsResource.ERR_TiposParticipacaoOrientacaoObrigatoriosException,
                string.Join("<br />- ", tiposParticipacao.OrderBy(o => o))))
        {
        }
    }
}