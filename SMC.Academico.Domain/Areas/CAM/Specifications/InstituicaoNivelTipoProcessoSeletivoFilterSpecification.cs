using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class InstituicaoNivelTipoProcessoSeletivoFilterSpecification : SMCSpecification<InstituicaoNivelTipoProcessoSeletivo>
    {
        public InstituicaoNivelTipoProcessoSeletivoFilterSpecification(long seqTipoProcessoSeletivo)
        {
            SeqTipoProcessoSeletivo = seqTipoProcessoSeletivo;
        }

        public InstituicaoNivelTipoProcessoSeletivoFilterSpecification()
        { }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public long? SeqFormaIngresso { get; set; }
        public List<long> SeqsNiveisEnsino { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public override Expression<Func<InstituicaoNivelTipoProcessoSeletivo, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqTipoProcessoSeletivo, p => p.SeqTipoProcessoSeletivo == this.SeqTipoProcessoSeletivo);
            AddExpression(this.SeqFormaIngresso, p => p.InstituicaoNivelFormaIngresso.SeqFormaIngresso == this.SeqFormaIngresso);
            AddExpression(this.SeqsNiveisEnsino, p => this.SeqsNiveisEnsino.Contains(p.InstituicaoNivelFormaIngresso.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino));
            AddExpression(this.SeqInstituicaoEnsino, p => p.InstituicaoNivelFormaIngresso.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(this.SeqTipoVinculoAluno, p => p.InstituicaoNivelFormaIngresso.InstituicaoNivelTipoVinculoAluno.SeqTipoVinculoAluno == this.SeqTipoVinculoAluno);

            return GetExpression();
        }
    }
}