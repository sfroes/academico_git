using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoFilterSpecification : SMCSpecification<Curso>
    {
        public long? Seq { get; set; }

        public string Nome { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public List<long> SeqItensHierarquiaEntidade { get; set; }

        public List<long> SeqEntidade { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        public List<long> SeqsCursos { get; set; }

        public override Expression<Func<Curso, bool>> SatisfiedBy()
        {
            DateTime agora = DateTime.Now; 

            AddExpression(SeqItensHierarquiaEntidade, w => SeqItensHierarquiaEntidade.Any(x => x == w.Seq));
            AddExpression(Seq, w => w.Seq == Seq);
            AddExpression(Nome, w => w.Nome.Contains(Nome));
            AddExpression(SeqNivelEnsino, w => this.SeqNivelEnsino.Any(x => x == w.SeqNivelEnsino));
            AddExpression(SeqsCursos, w => this.SeqsCursos.Any(x => x == w.Seq));
            AddExpression(SeqEntidade, w => w.HierarquiasEntidades.Any(c => this.SeqEntidade.Contains(c.SeqItemSuperior.Value)));
            AddExpression(SeqsEntidadesResponsaveis, w => w.HierarquiasEntidades.Any(a => this.SeqsEntidadesResponsaveis.Contains(a.SeqItemSuperior)));
            AddExpression(SeqSituacaoAtual, w => w.HistoricoSituacoes.FirstOrDefault(h => agora >= h.DataInicio && (!h.DataFim.HasValue || agora <= h.DataFim.Value)).SituacaoEntidade.Seq == this.SeqSituacaoAtual);

            return GetExpression();
        }
    }
}