using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using SMC.Academico.Common.Areas.CUR.Enums;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class MatrizCurricularFilterSpecification : SMCSpecification<MatrizCurricular>
    {
        public long? Seq { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqCurriculo { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqCurriculoCursoOferta { get; set; }

        public long? SeqModalidade { get; set; }

        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public string DescricaoComplementar { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public List<long> GrupoOrigemSeqComponentesCurriculares { get; set; } = new List<long>();

        public List<long> GrupoDispensadoSeqComponentesCurriculares { get; set; } = new List<long>();

        public List<long> SeqsCurriculoCursoOfertas { get; set; } = new List<long>();

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public TipoGestaoDivisaoComponente[] TipoGestaoDivisaoComponenteDiferentes { get; set; }

        public override Expression<Func<MatrizCurricular, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqCurso, p => p.CurriculoCursoOferta.CursoOferta.SeqCurso == this.SeqCurso);
            AddExpression(this.SeqCurriculo, p => p.CurriculoCursoOferta.SeqCurriculo == this.SeqCurriculo);
            AddExpression(this.SeqCursoOferta, p => p.CurriculoCursoOferta.SeqCursoOferta == this.SeqCursoOferta);
            AddExpression(this.SeqCurriculoCursoOferta, p => p.SeqCurriculoCursoOferta == this.SeqCurriculoCursoOferta);
            AddExpression(this.SeqModalidade, p => p.CurriculoCursoOferta.CursoOferta.CursosOfertaLocalidade.Count(c => c.SeqModalidade == this.SeqModalidade) > 0);
            AddExpression(this.Codigo, p => p.Codigo.Contains(this.Codigo));
            AddExpression(this.Descricao, p => p.Descricao.Contains(this.Descricao));
            AddExpression(this.DescricaoComplementar, p => p.DescricaoComplementar.Contains(this.DescricaoComplementar));
            AddExpression(this.SeqLocalidade, p => p.Ofertas.Count(c => c.CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade == this.SeqLocalidade) > 0);
            AddExpression(this.SeqTurno, p => p.Ofertas.Count(c => c.CursoOfertaLocalidadeTurno.SeqTurno == this.SeqTurno) > 0);
            AddExpression(this.SeqMatrizCurricularOferta, p => p.Ofertas.Count(c => c.Seq == this.SeqMatrizCurricularOferta) > 0);
            AddExpression(this.GrupoOrigemSeqComponentesCurriculares, p => this.GrupoOrigemSeqComponentesCurriculares.Count(c => p.ConfiguracoesComponente.Select(a => a.GrupoCurricularComponente.SeqComponenteCurricular).Contains(c)) > 0);
            AddExpression(this.GrupoDispensadoSeqComponentesCurriculares, p => this.GrupoDispensadoSeqComponentesCurriculares.Count(c => p.ConfiguracoesComponente.Select(a => a.GrupoCurricularComponente.SeqComponenteCurricular).Contains(c)) == 0);
            AddExpression(this.SeqsCurriculoCursoOfertas, p => this.SeqsCurriculoCursoOfertas.Contains(p.SeqCurriculoCursoOferta));
            AddExpression(this.SeqConfiguracaoComponente, p => p.ConfiguracoesComponente.Any(c => c.SeqConfiguracaoComponente == this.SeqConfiguracaoComponente));

            AddExpression(this.SeqCursoOfertaLocalidade, p => p.CurriculoCursoOferta.CursoOferta.CursosOfertaLocalidade.Count(col => col.Seq == this.SeqCursoOfertaLocalidade) > 0);
            AddExpression(this.TipoGestaoDivisaoComponenteDiferentes, p => p.ConfiguracoesComponente.Any(c => c.DivisoesComponente.Any(d => !this.TipoGestaoDivisaoComponenteDiferentes.Contains(d.DivisaoComponente.TipoDivisaoComponente.TipoGestaoDivisaoComponente))));

            return GetExpression();
        }
    }
}