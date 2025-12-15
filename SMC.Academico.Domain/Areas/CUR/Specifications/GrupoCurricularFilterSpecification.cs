using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CUR.Specifications
{
    public class GrupoCurricularFilterSpecification : SMCSpecification<GrupoCurricular>
    {
        public long? Seq { get; set; }

        public long? SeqCurriculo { get; set; }

        public bool? DesconsiderarGruposTodosItensObrigatorios { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public List<long> SeqsFormacoesEspecificas { get; set; }

        public override Expression<Func<GrupoCurricular, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => this.Seq.Value == p.Seq);
            AddExpression(this.SeqCurriculo, p => this.SeqCurriculo.Value == p.SeqCurriculo);
            AddExpression(this.DesconsiderarGruposTodosItensObrigatorios, p => !p.TipoConfiguracaoGrupoCurricular.Token.Equals(TOKEN_TIPO_CONFIGURACAO_GRUPO_CURRICULAR.TKN_TODOS_OBRIGATORIOS));
            AddExpression(this.SeqFormacaoEspecifica, x => x.SeqFormacaoEspecifica == SeqFormacaoEspecifica);
            AddExpression(this.SeqsFormacoesEspecificas, x => !x.SeqFormacaoEspecifica.HasValue || SeqsFormacoesEspecificas.Contains(x.SeqFormacaoEspecifica.Value));
            return GetExpression();
        }
    }
}