using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class DivisaoMatrizCurricularComponenteFilterSpecification : SMCSpecification<DivisaoMatrizCurricularComponente>, ISMCMappable
    {
        public long? Seq { get; set; }
        public long? SeqMatrizCurricular { get; set; }
        public long? SeqMatrizCurricularOferta { get; set; }
        public long? SeqGrupoCurricularComponente { get; set; }
        public long? SeqDivisaoMatrizCurricular { get; set; }
        public long? SeqCurriculoCursoOferta { get; set; }
        public long? SeqComponenteCurricular { get; set; }
        public long? SeqConfiguracaoComponente { get; set; }
        public long? SeqComponenteCurricularAssunto { get; set; }
        public long? SeqGrupoCurricular { get; set; }
        public List<long> SeqsMatrizCurricularOferta { get; set; }
        public List<long> SeqsComponenteCurricularAssunto { get; set; }
        public List<long> SeqsConfiguracoesComponente { get; set; }
        public string DescricaoGrupoCurricular { get; set; }
        public long? SeqTipoComponente { get; set; }
        public override Expression<Func<DivisaoMatrizCurricularComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqMatrizCurricular, p => p.SeqMatrizCurricular == this.SeqMatrizCurricular);
            AddExpression(this.SeqMatrizCurricularOferta, p => p.MatrizCurricular.Ofertas.Any(w => w.Seq == this.SeqMatrizCurricularOferta));
            AddExpression(this.SeqsMatrizCurricularOferta, p => p.MatrizCurricular.Ofertas.Any(w => this.SeqsMatrizCurricularOferta.Contains(w.Seq)));
            AddExpression(this.SeqGrupoCurricularComponente, p => p.SeqGrupoCurricularComponente == this.SeqGrupoCurricularComponente);
            AddExpression(this.SeqDivisaoMatrizCurricular, p => p.SeqDivisaoMatrizCurricular == this.SeqDivisaoMatrizCurricular);
            AddExpression(this.SeqCurriculoCursoOferta, p => p.MatrizCurricular.SeqCurriculoCursoOferta == this.SeqCurriculoCursoOferta);
            AddExpression(this.SeqComponenteCurricular, p => p.GrupoCurricularComponente.SeqComponenteCurricular == this.SeqComponenteCurricular);
            AddExpression(this.SeqConfiguracaoComponente, p => p.SeqConfiguracaoComponente == this.SeqConfiguracaoComponente);
            AddExpression(this.SeqsConfiguracoesComponente, p => p.SeqConfiguracaoComponente.HasValue && SeqsConfiguracoesComponente.Contains(p.SeqConfiguracaoComponente.Value));
            AddExpression(this.SeqGrupoCurricular, p => p.GrupoCurricularComponente.SeqGrupoCurricular == SeqGrupoCurricular);
            AddExpression(this.SeqComponenteCurricularAssunto, p => p.ComponentesCurricularSubstitutos.Any(a => a.Seq == this.SeqComponenteCurricularAssunto));
            AddExpression(this.SeqsComponenteCurricularAssunto, p => p.ComponentesCurricularSubstitutos.Any(w => this.SeqsComponenteCurricularAssunto.Contains(w.Seq)));
            AddExpression(DescricaoGrupoCurricular, p => p.GrupoCurricularComponente.GrupoCurricular.Descricao.Contains(DescricaoGrupoCurricular));
            AddExpression(SeqTipoComponente, p => p.GrupoCurricularComponente.ComponenteCurricular.SeqTipoComponenteCurricular == SeqTipoComponente);

            return GetExpression();
        }
    }
}