using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class GrupoCurricularComponenteFilterSpecification : SMCSpecification<GrupoCurricularComponente>
    {
        public long? Seq { get; set; }
        public List<long> SeqGruposCurriculares { get; set; } = new List<long>();
        public List<long> SeqComponentesCurriculares { get; set; } = new List<long>();
        public List<long> seqsGrupoCurricularComponente { get; set; }
        public string DescricaoGrupoCurricular { get; set; }
        public long? SeqTipoComponente { get; set; }
        public bool? ComponenteAtivo { get; set; }
        public string SiglaComponente { get; set; }
        public long? SeqCurriculoCursoOferta { get; set; }


        public override Expression<Func<GrupoCurricularComponente, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqGruposCurriculares, p => this.SeqGruposCurriculares.Contains(p.SeqGrupoCurricular));
            AddExpression(this.SeqComponentesCurriculares, p => this.SeqComponentesCurriculares.Contains(p.SeqComponenteCurricular));
            AddExpression(seqsGrupoCurricularComponente, p => seqsGrupoCurricularComponente.Contains(p.Seq));
            AddExpression(DescricaoGrupoCurricular, p => p.GrupoCurricular.Descricao.Contains(DescricaoGrupoCurricular));
            AddExpression(SeqTipoComponente, p => p.ComponenteCurricular.SeqTipoComponenteCurricular == SeqTipoComponente);
            AddExpression(ComponenteAtivo, p => p.ComponenteCurricular.Ativo == ComponenteAtivo);
            AddExpression(SiglaComponente, p => p.ComponenteCurricular.TipoComponente.Sigla == SiglaComponente);
            AddExpression(SeqCurriculoCursoOferta, p => p.GrupoCurricular.Curriculo.CursosOferta.Any(c => c.Seq == SeqCurriculoCursoOferta));

            return GetExpression();
        }
    }
}
