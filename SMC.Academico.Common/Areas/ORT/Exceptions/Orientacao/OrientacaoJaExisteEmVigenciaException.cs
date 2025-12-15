using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class OrientacaoJaExisteEmVigenciaException : SMCApplicationException
    {
        public OrientacaoJaExisteEmVigenciaException(string nomeAluno, string descricaoTipoOrientacao)
            : base(string.Format(ExceptionsResource.ERR_OrientacaoJaExisteEmVigenciaException, nomeAluno, descricaoTipoOrientacao))
        {
        }
    }
}