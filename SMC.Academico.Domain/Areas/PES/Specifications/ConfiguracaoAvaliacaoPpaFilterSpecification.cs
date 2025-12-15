using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class ConfiguracaoAvaliacaoPpaFilterSpecification : SMCSpecification<ConfiguracaoAvaliacaoPpa>
    {
        public long? SeqEntidadeResponsavel { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public List<TipoAvaliacaoPpa> ListaTipoAvaliacaoPpa { get; set; }

        public bool? ConfiguracaoVigente { get; set; }

        public bool? DataLimiteRespostasFutura { get; set; }
        
        public string Descricao { get; set; }

        public int? CodigoAvaliacaoPpa { get; set; }

        public long? Seq { get; set; }

        public override Expression<Func<ConfiguracaoAvaliacaoPpa, bool>> SatisfiedBy()
        {
            DateTime agora = DateTime.Now;

            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(SeqEntidadeResponsavel, c => c.SeqEntidadeResponsavel == this.SeqEntidadeResponsavel);
            AddExpression(SeqsEntidadesResponsaveis, x => SeqsEntidadesResponsaveis.Contains(x.SeqEntidadeResponsavel));
            AddExpression(SeqNivelEnsino, c => c.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(ListaTipoAvaliacaoPpa, a => ListaTipoAvaliacaoPpa.Contains(a.TipoAvaliacaoPpa));
            if (ConfiguracaoVigente.HasValue && ConfiguracaoVigente.Value)
                AddExpression(c => c.DataInicioVigencia <= agora && (!c.DataFimVigencia.HasValue || c.DataFimVigencia > agora));
            if (DataLimiteRespostasFutura.HasValue && DataLimiteRespostasFutura.Value)
                AddExpression(c => !c.DataLimiteRespostas.HasValue || c.DataLimiteRespostas > agora);
            AddExpression(CodigoAvaliacaoPpa, c => c.CodigoAvaliacaoPpa == this.CodigoAvaliacaoPpa);
            AddExpression(Descricao, c => c.Descricao.Contains(this.Descricao));
            AddExpression(SeqCicloLetivo, c => c.CicloLetivo.Seq == this.SeqCicloLetivo);

            return GetExpression();
        }
    }
}