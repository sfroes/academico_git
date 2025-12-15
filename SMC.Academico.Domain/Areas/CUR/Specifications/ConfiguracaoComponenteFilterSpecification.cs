using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class ConfiguracaoComponenteFilterSpecification : SMCSpecification<ConfiguracaoComponente>
    {
        public long? Seq { get; set; }

        public long?[] SeqConfiguracoesComponentes { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string CodigoOuDescricao { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long?[] SeqsComponentesCurriculares { get; set; }

        public bool? Ativo { get; set; }

        public TipoGestaoDivisaoComponente? TipoGestaoDivisaoComponente { get; set; }

		public TipoGestaoDivisaoComponente?[] VariosTipoGestaoDivisaoComponente { get; set; }

		public TipoGestaoDivisaoComponente?[] VariosTipoGestaoDivisaoComponenteDiferente { get; set; }

		public List<long> SeqsEntidadesResponsaveis { get; set; }

        public List<long> SeqsMatrizCurricular { get; set; }

        public override Expression<Func<ConfiguracaoComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqConfiguracoesComponentes, p => this.SeqConfiguracoesComponentes.Contains(p.Seq));
            AddExpression(this.Codigo, p => p.Codigo.Contains(this.Codigo));
            AddExpression(this.Descricao, p => p.Descricao.Contains(this.Descricao));
            AddExpression(this.CodigoOuDescricao, p => p.Codigo.Contains(this.CodigoOuDescricao) || p.Descricao.Contains(this.CodigoOuDescricao));
            AddExpression(this.SeqComponenteCurricular, p => this.SeqComponenteCurricular.Value == p.SeqComponenteCurricular);
            AddExpression(this.SeqsComponentesCurriculares, p => this.SeqsComponentesCurriculares.Contains(p.SeqComponenteCurricular));
            AddExpression(this.Ativo, p => this.Ativo.Value == p.Ativo);
            AddExpression(this.TipoGestaoDivisaoComponente, p => p.DivisoesComponente.Any(c => c.TipoDivisaoComponente.TipoGestaoDivisaoComponente == this.TipoGestaoDivisaoComponente.Value));
			AddExpression(this.VariosTipoGestaoDivisaoComponente, p => p.DivisoesComponente.Any(c => this.VariosTipoGestaoDivisaoComponente.Contains(c.TipoDivisaoComponente.TipoGestaoDivisaoComponente)));
			AddExpression(this.VariosTipoGestaoDivisaoComponenteDiferente, p => !p.DivisoesComponente.Any(c => this.VariosTipoGestaoDivisaoComponenteDiferente.Contains(c.TipoDivisaoComponente.TipoGestaoDivisaoComponente)));
            AddExpression(this.SeqsEntidadesResponsaveis, p => p.ComponenteCurricular.EntidadesResponsaveis.Any(a => SeqsEntidadesResponsaveis.Contains(a.SeqEntidade)));
            AddExpression(this.SeqsMatrizCurricular, p => p.DivisoesMatrizCurricularComponente.Any(d => SeqsMatrizCurricular.Contains(d.SeqMatrizCurricular)));

            return GetExpression();
        }
    }
}