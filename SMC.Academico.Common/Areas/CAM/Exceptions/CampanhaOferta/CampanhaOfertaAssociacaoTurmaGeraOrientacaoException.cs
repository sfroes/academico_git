using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class CampanhaOfertaAssociacaoTurmaGeraOrientacaoException : SMCApplicationException
    {
        public CampanhaOfertaAssociacaoTurmaGeraOrientacaoException(string descTurma)
            : base(string.Format(ExceptionsResource.ERR_CampanhaOfertaAssociacaoTurmaGeraOrientacaoException, descTurma))
        { }
    }
}
