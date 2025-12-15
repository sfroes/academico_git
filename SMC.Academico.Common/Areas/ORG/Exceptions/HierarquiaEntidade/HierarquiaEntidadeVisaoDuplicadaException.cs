using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class HierarquiaEntidadeVisaoDuplicadaException : SMCApplicationException
    {
        public HierarquiaEntidadeVisaoDuplicadaException(string Visao)
            : base(String.Format(Resources.ExceptionsResource.ERR_HierarquiaEntidadeVisaoDuplicadaException, Visao))
        {
        }
    }
}
