using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class PeriodoIntercambioFilterSpecification : SMCSpecification<PeriodoIntercambio>
    {

        public long? Seq { get; set; }

        public long? SeqAluno { get; set; }

        public DateTime? DataInicioSituacao { get; set; }

        public override Expression<Func<PeriodoIntercambio, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqAluno, p => p.PessoaAtuacaoTermoIntercambio.SeqPessoaAtuacao == this.SeqAluno);
            AddExpression(this.DataInicioSituacao, p => p.DataInicio <= DataInicioSituacao && p.DataFim >= DataInicioSituacao);

            return GetExpression();
        }
    }
}