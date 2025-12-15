using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaAtuacaoAmostraPpaFilterSpecification : SMCSpecification<PessoaAtuacaoAmostraPpa>
    {
        public long? SeqPessoaAtuacao { get; set; }

        public TipoAvaliacaoPpa? TipoAvaliacaoPpa { get; set; }

        public  bool? NaoPreenchido { get; set; }

        public bool? DataLimiteRespostasFutura { get; set; }

        public bool? DataInicioVigenciaPassada { get; set; }

        public long? SeqConfiguracaoAvaliacaoPpa { get; set; }

        public long? SeqConfiguracaoAvaliacaoPpaTurma { get; set; }

        public override Expression<Func<PessoaAtuacaoAmostraPpa, bool>> SatisfiedBy()
        {
            DateTime agora = DateTime.Now;

            AddExpression(SeqPessoaAtuacao, a => a.SeqPessoaAtuacao == this.SeqPessoaAtuacao);
            AddExpression(TipoAvaliacaoPpa, a => a.ConfiguracaoAvaliacaoPpa.TipoAvaliacaoPpa == this.TipoAvaliacaoPpa);
            if (NaoPreenchido.HasValue && NaoPreenchido.Value)
                AddExpression(a => !a.DataPreenchimento.HasValue);
            if (DataLimiteRespostasFutura.HasValue && DataLimiteRespostasFutura.Value)
                AddExpression(a => !a.ConfiguracaoAvaliacaoPpa.DataLimiteRespostas.HasValue || a.ConfiguracaoAvaliacaoPpa.DataLimiteRespostas > agora);
            if (DataInicioVigenciaPassada.HasValue && DataInicioVigenciaPassada.Value)
                AddExpression(a => a.ConfiguracaoAvaliacaoPpa.DataInicioVigencia < agora);
            AddExpression(SeqConfiguracaoAvaliacaoPpa, a => a.SeqConfiguracaoAvaliacaoPpa == this.SeqConfiguracaoAvaliacaoPpa);
            AddExpression(SeqConfiguracaoAvaliacaoPpaTurma, a => a.SeqConfiguracaoAvaliacaoPpaTurma == this.SeqConfiguracaoAvaliacaoPpaTurma);
            return GetExpression();
        }
    }
}