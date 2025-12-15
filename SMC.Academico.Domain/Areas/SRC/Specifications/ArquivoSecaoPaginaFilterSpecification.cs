using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ArquivoSecaoPaginaFilterSpecification : SMCSpecification<ArquivoSecaoPagina>
    {
        public long SeqConfiguracaoEtapaPagina { get; set; }

        public long SeqSecaoPaginaSgf { get; set; }

        public override Expression<Func<ArquivoSecaoPagina, bool>> SatisfiedBy()
        {
            return c => c.SeqConfiguracaoEtapaPagina == SeqConfiguracaoEtapaPagina && c.SeqSecaoPaginaSgf == SeqSecaoPaginaSgf;
        }
    }
}
