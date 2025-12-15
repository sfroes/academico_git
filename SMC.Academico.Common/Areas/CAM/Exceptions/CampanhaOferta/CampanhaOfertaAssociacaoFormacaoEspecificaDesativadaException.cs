using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class CampanhaOfertaAssociacaoFormacaoEspecificaDesativadaException : SMCApplicationException
    {
        public CampanhaOfertaAssociacaoFormacaoEspecificaDesativadaException(string oferta)
            : base(string.Format(ExceptionsResource.ERR_CampanhaOfertaAssociacaoFormacaoEspecificaDesativadaException, oferta))
        { }
    }
}
