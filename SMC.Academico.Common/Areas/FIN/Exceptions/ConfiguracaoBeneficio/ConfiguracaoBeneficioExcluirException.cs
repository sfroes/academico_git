using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMC.Academico.Common.Areas.FIN.Resources;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class ConfiguracaoBeneficioExcluirException : SMCApplicationException
    {
        public ConfiguracaoBeneficioExcluirException() :
            base(ExceptionsResource.ERR_ConfiguracaoBeneficioExcluir)
        {

        }
    }
}
