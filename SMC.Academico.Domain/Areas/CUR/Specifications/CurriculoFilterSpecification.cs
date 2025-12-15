using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class CurriculoFilterSpecification : SMCSpecification<Curriculo>
    {
        public long? Seq { get; set; }

        public long? SeqCurso { get; set; }

        public bool? Ativo { get; set; }

        public string Codigo { get; set; }

        public int? CodigoCurriculoMigracao { get; set; }

        public bool? GerarDocumentoDigital { get; set; }

        public override Expression<Func<Curriculo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqCurso, p => p.SeqCurso == this.SeqCurso);
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);
            AddExpression(this.Codigo, p => p.Codigo.StartsWith(this.Codigo));
            AddExpression(this.CodigoCurriculoMigracao, p => p.CodigoCurriculoMigracao == this.CodigoCurriculoMigracao);
            AddExpression(this.GerarDocumentoDigital, p => p.GerarDocumentoDigital == this.GerarDocumentoDigital);

            return GetExpression();
        }
    }
}