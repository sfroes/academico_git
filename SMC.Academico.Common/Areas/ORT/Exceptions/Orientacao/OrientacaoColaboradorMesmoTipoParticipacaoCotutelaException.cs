using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrientacaoColaboradorMesmoTipoParticipacaoCotutelaException : SMCApplicationException
    {
        public OrientacaoColaboradorMesmoTipoParticipacaoCotutelaException(string status)
            : base(string.Format(ExceptionsResource.ERR_OrientacaoColaboradorMesmoTipoParticipacaoCotutelaException, status))
        {
        }
    }
}