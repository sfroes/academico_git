using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class GrupoEscalonamentoItemParcelaFilterSpecification : SMCSpecification<GrupoEscalonamentoItemParcela>
    {        
        public long? Seq { get; set; }
        public long? SeqGrupoEscalonamentoItem { get; set; }
                    
        public override Expression<Func<GrupoEscalonamentoItemParcela, bool>> SatisfiedBy()
        {           
            AddExpression(Seq, w => w.Seq == Seq.Value);            
            AddExpression(SeqGrupoEscalonamentoItem, w => w.SeqGrupoEscalonamentoItem == SeqGrupoEscalonamentoItem.Value);            
           
            return GetExpression();
        }
    }
}