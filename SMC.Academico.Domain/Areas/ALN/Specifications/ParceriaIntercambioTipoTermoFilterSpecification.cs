using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class ParceriaIntercambioTipoTermoFilterSpecification : SMCSpecification<ParceriaIntercambioTipoTermo>
    {
        public long? Seq { get; set; }

        public long? SeqParceriaIntercambio { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public bool? Ativo { get; set; }
        public long[] SeqsParceriaIntercambioTipoTermo { get; set; }

        public override Expression<Func<ParceriaIntercambioTipoTermo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq);
            AddExpression(this.Ativo, a => a.Ativo == this.Ativo);
            AddExpression(this.SeqParceriaIntercambio, a => a.SeqParceriaIntercambio == this.SeqParceriaIntercambio);
            AddExpression(this.SeqNivelEnsino, a => a.TermosIntercambio.Any(at => at.SeqNivelEnsino == this.SeqNivelEnsino));
            AddExpression(this.SeqsParceriaIntercambioTipoTermo, a => SeqsParceriaIntercambioTipoTermo.Contains(a.Seq));

            return GetExpression();
        }
    }
}