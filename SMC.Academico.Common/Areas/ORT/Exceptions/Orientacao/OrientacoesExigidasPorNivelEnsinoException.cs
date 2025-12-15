using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrientacoesExigidasPorNivelEnsinoException : SMCApplicationException
    {
        public OrientacoesExigidasPorNivelEnsinoException(string nivelEnsino, string tipoVinculo, IEnumerable<string> tiposOrientacao)
            : base(string.Format(ExceptionsResource.ERR_OrientacoesExigidasPorNivelEnsinoException,
                nivelEnsino,
                tipoVinculo,
                string.Join("<br /> -", tiposOrientacao.OrderBy(o => o))))
        {
        }
    }
}