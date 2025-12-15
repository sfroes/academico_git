using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class InstituicaoNivelSituacaoMatriculaFilterSpecification : SMCSpecification<InstituicaoNivelSituacaoMatricula>
    {
        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public List<long> SeqsTipoVinculoAluno { get; set; }

        public List<string> Tokens { get; set; }

        public bool? VinculoAtivo { get; set; }

        public override Expression<Func<InstituicaoNivelSituacaoMatricula, bool>> SatisfiedBy()
        {
            AddExpression(SeqNivelEnsino, p => p.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(SeqsNivelEnsino, p => SeqsNivelEnsino.Contains(p.InstituicaoNivelTipoVinculoAluno.InstituicaoNivel.SeqNivelEnsino));
            AddExpression(SeqTipoVinculoAluno, p => p.InstituicaoNivelTipoVinculoAluno.SeqTipoVinculoAluno == SeqTipoVinculoAluno);
            AddExpression(SeqsTipoVinculoAluno, p => SeqsTipoVinculoAluno.Contains(p.InstituicaoNivelTipoVinculoAluno.SeqTipoVinculoAluno));
            AddExpression(Tokens, p => Tokens.Contains(p.SituacaoMatricula.Token));
            AddExpression(VinculoAtivo, a => a.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo == VinculoAtivo);

            return GetExpression();
        }
    }
}