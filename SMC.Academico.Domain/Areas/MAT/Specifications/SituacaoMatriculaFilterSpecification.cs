using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.MAT.Specifications
{
    public class SituacaoMatriculaFilterSpecification : SMCSpecification<SituacaoMatricula>
    {
        public long? SeqTipoSituacaoMatricula { get; set; }

        public string Token { get; set; }

        public List<long> SeqsTipoSituacaoMatricula { get; set; }

        public bool? VinculoAtivo { get; set; }

        public override Expression<Func<SituacaoMatricula, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqTipoSituacaoMatricula, a => a.SeqTipoSituacaoMatricula == this.SeqTipoSituacaoMatricula);
            AddExpression(Token, a => a.Token == Token);
            AddExpression(this.SeqsTipoSituacaoMatricula, a => SeqsTipoSituacaoMatricula.Contains(a.SeqTipoSituacaoMatricula));
            AddExpression(VinculoAtivo, a => a.TipoSituacaoMatricula.VinculoAlunoAtivo == VinculoAtivo);

            return GetExpression();
        }
    }
}