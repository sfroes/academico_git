using SMC.Academico.Common.Areas.FIN.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class PessoaAtuacaoBeneficioDataInicioSimIncideParcelaException : SMCApplicationException
    {
        public PessoaAtuacaoBeneficioDataInicioSimIncideParcelaException()
            :base(ExceptionsResource.ERR_PessoaAtuacaoBeneficioDataInicioSimIncideParcelaException)
        {

        }

    }
}
