using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class ComponenteCurricularIgualFilterSpecification : SMCSpecification<ComponenteCurricular>
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public short? CargaHoraria { get; set; }

        public short? Credito { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqTipoComponenteCurricular { get; set; }

        public short? NumeroVersaoCarga { get; set; }

        public override Expression<Func<ComponenteCurricular, bool>> SatisfiedBy()
        {
            // Caso tenha sido informado o seq, não deve considerar o próprio registro para verificar se existe alguem com os mesmos valores
            if (Seq.HasValue && Seq.Value != 0)
                AddExpression(Seq, x => x.Seq != Seq);

            AddExpression(x => x.Descricao == Descricao);
            AddExpression(x => x.CargaHoraria == CargaHoraria);
            AddExpression(x => x.Credito == Credito);
            AddExpression(x => x.EntidadesResponsaveis.All(e => SeqsEntidadesResponsaveis.Contains(e.SeqEntidade)) && x.EntidadesResponsaveis.Count == SeqsEntidadesResponsaveis.Count);
            AddExpression(x => x.SeqTipoComponenteCurricular == SeqTipoComponenteCurricular);
            AddExpression(x => x.NumeroVersaoCarga == NumeroVersaoCarga);

            return GetExpression();
        }
    }
}