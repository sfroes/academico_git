using SMC.Academico.Common.Areas.FIN.Resources;
using SMC.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Common.Areas.FIN.Exceptions
{
    public class PessoaAtuacaoBeneficioForaPeriodoSelecionadoException : SMCApplicationException
    {
        public PessoaAtuacaoBeneficioForaPeriodoSelecionadoException()
            : base(ExceptionsResource.ERR_PessoaAtuacaoBeneficioForaPeriodoSelecionadoException)
        {

        }

    }
}
