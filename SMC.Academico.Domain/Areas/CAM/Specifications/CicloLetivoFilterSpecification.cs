using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CicloLetivoFilterSpecification : SMCSpecification<CicloLetivo>
    {
        public long? Seq { get; set; }

        public long? SeqRegimeLetivo { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public short? Ano { get; set; }

        public short? Numero { get; set; }

        public string Descricao { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public List<long> SeqsNiveisEnsino { get; set; }

        public List<long> SeqsCiclosLetivos { get; set; }

        public override Expression<Func<CicloLetivo, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq.Value);
            AddExpression(this.SeqsCiclosLetivos, p => SeqsCiclosLetivos.Contains(p.Seq));
            AddExpression(this.SeqRegimeLetivo, p => p.SeqRegimeLetivo == this.SeqRegimeLetivo.Value);
            AddExpression(this.SeqNivelEnsino, p => p.NiveisEnsino.Any(a => a.Seq == this.SeqNivelEnsino.Value));
            AddExpression(this.Ano, p => p.Ano == this.Ano.Value);
            AddExpression(this.Numero, p => p.Numero == this.Numero.Value);
            AddExpression(this.SeqsNiveisEnsino, p => p.NiveisEnsino.Any(x => SeqsNiveisEnsino.Contains(x.Seq)));
            AddExpression(this.Descricao, p => p.Descricao.StartsWith(this.Descricao));
            AddExpression(this.SeqInstituicaoEnsino, p => p.SeqInstituicaoEnsino == SeqInstituicaoEnsino);

            return GetExpression();
        }
    }
}