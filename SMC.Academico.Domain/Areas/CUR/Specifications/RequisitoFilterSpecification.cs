using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class RequisitoFilterSpecification : SMCSpecification<Requisito>
    {
        public long? Seq { get; set; }

        public long? SeqMatrizCurricular { get; set; }

        public long? SeqCurriculoCursoOferta { get; set; }

        public long? SeqDivisaoCurricularItem { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public TipoRequisitoAssociado? Associado { get; set; }

        public TipoRequisito? TipoRequisito { get; set; }

        public TipoRequisitoItem? TipoRequisitoItem { get; set; }

        public long? ItemSeqDivisaoCurricularItem { get; set; }

        public long? ItemSeqComponenteCurricular { get; set; }

        public long? ItemSeqGrupoCurricular { get; set; }

        public OutroRequisito? OutroRequisitoItem { get; set; }

        public short? QuantidadeOutroRequisito { get; set; }

        public long? QuantidadeItensCadastrado { get; set; }

        public override Expression<Func<Requisito, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqDivisaoCurricularItem, p => this.SeqDivisaoCurricularItem.Value == p.SeqDivisaoCurricularItem);
            AddExpression(this.SeqComponenteCurricular, p => this.SeqComponenteCurricular.Value == p.SeqComponenteCurricular);
            AddExpression(this.Associado, p => this.Associado == TipoRequisitoAssociado.Nenhum
                            || (this.Associado == TipoRequisitoAssociado.Associado && p.MatrizesCurriculares.Count(c => c.SeqMatrizCurricular == this.SeqMatrizCurricular) > 0)
                            || (this.Associado == TipoRequisitoAssociado.NaoAssociado && p.MatrizesCurriculares.Count(c => c.SeqMatrizCurricular == this.SeqMatrizCurricular) == 0)
            );
            AddExpression(this.TipoRequisito, p => this.TipoRequisito == Common.Areas.CUR.Enums.TipoRequisito.Nenhum || p.Itens.Count(c => c.TipoRequisito == this.TipoRequisito) > 0);
            AddExpression(this.TipoRequisitoItem, p => this.TipoRequisitoItem == Common.Areas.CUR.Enums.TipoRequisitoItem.Nenhum || p.Itens.Count(c => c.TipoRequisitoItem == this.TipoRequisitoItem) > 0);
            AddExpression(this.ItemSeqDivisaoCurricularItem, p => p.Itens.Count(c => c.SeqDivisaoCurricularItem == this.ItemSeqDivisaoCurricularItem) > 0);
            AddExpression(this.ItemSeqComponenteCurricular, p => p.Itens.Count(c => c.SeqComponenteCurricular == this.ItemSeqComponenteCurricular) > 0);
            AddExpression(this.ItemSeqGrupoCurricular, p => p.Itens.Count(c => c.SeqGrupoCurricular == this.ItemSeqGrupoCurricular) > 0);
            AddExpression(this.OutroRequisitoItem, p => this.OutroRequisitoItem == OutroRequisito.Nenhum || p.Itens.Count(c => c.OutroRequisito == this.OutroRequisitoItem) > 0);
            AddExpression(this.QuantidadeOutroRequisito, p => p.Itens.Count(c => c.QuantidadeOutroRequisito == this.QuantidadeOutroRequisito) > 0);
            AddExpression(this.QuantidadeItensCadastrado, p => this.QuantidadeItensCadastrado == p.Itens.Count());

            return GetExpression();
        }

    }
}
