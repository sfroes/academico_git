using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoOfertaFilterSpecification : SMCSpecification<CursoOferta>
    {
        public long? Seq { get; set; }

        public long? SeqCurso { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqLocalidade { get; set; }

        public List<long?> SeqItensSuperior { get; set; }

        public long? SeqTipoFormacaoEspecifica { get; set; }

        public bool? Ativo { get; set; }

        public string Descricao { get; set; }

        public string Nome { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public List<long> SeqNivelEnsino { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public List<long?> SeqsEntidadesResponsaveis { get; set; }

        public override Expression<Func<CursoOferta, bool>> SatisfiedBy()
        {
            DateTime agora = DateTime.Today;

            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(SeqCurso, w => w.SeqCurso == this.SeqCurso);
            AddExpression(SeqFormacaoEspecifica, w => w.SeqFormacaoEspecifica == this.SeqFormacaoEspecifica);
            AddExpression(SeqLocalidade, w => w.CursosOfertaLocalidade.Any(c => c.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade == this.SeqLocalidade));
            AddExpression(SeqItensSuperior, w => w.Curso.HierarquiasEntidades.Any(a => this.SeqItensSuperior.Contains(a.SeqItemSuperior)));
            AddExpression(SeqTipoFormacaoEspecifica, w => w.FormacaoEspecificaCursoOferta.SeqTipoFormacaoEspecifica == this.SeqTipoFormacaoEspecifica);
            AddExpression(Ativo, w => w.Ativo == this.Ativo);
            AddExpression(Nome, w => w.Curso.Nome.Contains(this.Nome));
            AddExpression(Descricao, w => w.Descricao.Contains(this.Descricao));
            AddExpression(SeqEntidadeResponsavel, w => w.Curso.HierarquiasEntidades.Any(a => this.SeqEntidadeResponsavel == a.SeqItemSuperior));
            AddExpression(SeqsEntidadesResponsaveis, w => w.Curso.HierarquiasEntidades.Any(a => this.SeqsEntidadesResponsaveis.Contains(a.SeqItemSuperior)));
            AddExpression(SeqNivelEnsino, w => this.SeqNivelEnsino.Contains(w.Curso.SeqNivelEnsino));
            AddExpression(SeqSituacaoAtual, w => (w.Curso.HistoricoSituacoes.Count() > 0
                   && w.Curso.HistoricoSituacoes.FirstOrDefault(h => agora >= h.DataInicio && (!h.DataFim.HasValue || agora <= h.DataFim.Value)).SituacaoEntidade.Seq == this.SeqSituacaoAtual));

            return GetExpression();
        }
    }
}