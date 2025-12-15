using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaFilterSpecification : SMCSpecification<Campanha>
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public string DescricaoDuplicada { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long[] SeqsCicloLetivo { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public override Expression<Func<Campanha, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqProcessoSeletivo, p => p.ProcessosSeletivos.Any(c => c.Seq == SeqProcessoSeletivo));
            AddExpression(this.Descricao, p => p.Descricao.Contains(this.Descricao));
            AddExpression(this.DescricaoDuplicada, p => p.Descricao.ToLower().Equals(this.DescricaoDuplicada.ToLower()));
            AddExpression(this.SeqCicloLetivo, p => p.CiclosLetivos.Any(a => a.SeqCicloLetivo == this.SeqCicloLetivo));
            AddExpression(this.SeqEntidadeResponsavel, p => p.SeqEntidadeResponsavel == this.SeqEntidadeResponsavel);
            AddExpression(this.SeqNivelEnsino, p => p.CiclosLetivos.Any(c => c.CicloLetivo.NiveisEnsino.Any(n => n.Seq == this.SeqNivelEnsino)));
            AddExpression(this.SeqsCicloLetivo, p => p.CiclosLetivos.Any(c => this.SeqsCicloLetivo.Contains(c.SeqCicloLetivo)));

            return GetExpression();
        }
    }
}