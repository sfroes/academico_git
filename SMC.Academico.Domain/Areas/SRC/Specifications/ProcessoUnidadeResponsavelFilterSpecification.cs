using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ProcessoUnidadeResponsavelFilterSpecification : SMCSpecification<ProcessoUnidadeResponsavel>
    {
        public long? Seq { get; set; }

        public long? SeqProcesso { get; set; }

        public long? SeqServico { get; set; }
        public long? SeqCicloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public TipoUnidadeResponsavel? TipoUnidadeResponsavel { get; set; }

        public long[] SeqsProcessos { get; set; }
        public long[] SeqsEntidadesResponsaveis { get; set; }
        public long? SeqProcessoDiferenteDe { get; set; }

        public override Expression<Func<ProcessoUnidadeResponsavel, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq.Value);
            AddExpression(this.SeqProcesso, a => a.SeqProcesso == this.SeqProcesso);
            AddExpression(this.SeqServico, a => a.Processo.SeqServico == this.SeqServico);
            AddExpression(this.TipoUnidadeResponsavel, a => a.TipoUnidadeResponsavel == this.TipoUnidadeResponsavel);
            AddExpression(this.SeqEntidadeResponsavel, a => a.SeqEntidadeResponsavel == this.SeqEntidadeResponsavel);
            AddExpression(this.SeqsProcessos, a => SeqsProcessos.Contains(a.SeqProcesso));
            AddExpression(SeqCicloLetivo, w => w.Processo.CicloLetivo.Seq == SeqCicloLetivo.Value);
            AddExpression(SeqsEntidadesResponsaveis, w => SeqsEntidadesResponsaveis.Contains(w.SeqEntidadeResponsavel));
            AddExpression(this.SeqProcessoDiferenteDe, a => a.SeqProcesso != this.SeqProcessoDiferenteDe);

            return GetExpression();
        }
    }
}