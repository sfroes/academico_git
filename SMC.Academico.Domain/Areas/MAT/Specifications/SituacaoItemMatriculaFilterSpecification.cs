using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.MAT.Specifications
{
    public class SituacaoItemMatriculaFilterSpecification : SMCSpecification<SituacaoItemMatricula>
    {
        public long? SeqProcessoEtapa { get; set; }

        public bool? SituacaoInicial { get; set; }

        public bool? SituacaoFinal { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public override Expression<Func<SituacaoItemMatricula, bool>> SatisfiedBy()
        { 
            AddExpression(SeqProcessoEtapa, w => w.SeqProcessoEtapa == this.SeqProcessoEtapa);
            AddExpression(SituacaoInicial, w => w.SituacaoInicial == this.SituacaoInicial);
            AddExpression(SituacaoFinal, w => w.SituacaoFinal == this.SituacaoFinal);
            AddExpression(ClassificacaoSituacaoFinal, w => w.ClassificacaoSituacaoFinal == this.ClassificacaoSituacaoFinal);

            return GetExpression();
        }
    }
}