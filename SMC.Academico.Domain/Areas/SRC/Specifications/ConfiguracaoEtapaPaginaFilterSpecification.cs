using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ConfiguracaoEtapaPaginaFilterSpecification : SMCSpecification<ConfiguracaoEtapaPagina>
    {
        public long? SeqConfiguracaoEtapa { get; set; }

        public string Token { get; set; }

        public long? SeqPaginaEtapaSgf { get; set; }

        public long? SeqFormulario { get; set; }

        public ConfiguracaoDocumento? ConfiguracaoDocumento { get; set; }


        public override Expression<Func<ConfiguracaoEtapaPagina, bool>> SatisfiedBy()
        {
            AddExpression(SeqConfiguracaoEtapa, w => w.SeqConfiguracaoEtapa == this.SeqConfiguracaoEtapa.Value);            
            AddExpression(Token, w => w.TokenPagina.Equals(this.Token));
            AddExpression(SeqPaginaEtapaSgf, w => w.SeqPaginaEtapaSgf == this.SeqPaginaEtapaSgf.Value);
            AddExpression(SeqFormulario, w => w.SeqFormulario == this.SeqFormulario.Value);
            
            if(ConfiguracaoDocumento != null)
                AddExpression(ConfiguracaoDocumento, w => w.ConfiguracaoDocumento == this.ConfiguracaoDocumento);

            return GetExpression();
        }
    }
}