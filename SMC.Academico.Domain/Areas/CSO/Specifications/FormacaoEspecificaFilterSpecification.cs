using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class FormacaoEspecificaFilterSpecification : SMCSpecification<FormacaoEspecifica>
    {
        public long? Seq { get; set; }

        public List<long> Seqs { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public bool? NodeRaiz { get; set; }

        public string TokenTipoFormacaoEspecifica { get; set; }

        public List<long> SeqEntidades { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqCursoOfertaLocalidade { get; set; }

        public override Expression<Func<FormacaoEspecifica, bool>> SatisfiedBy()
        {
            if (SeqEntidades==null)
            {
                SeqEntidades = new List<long>();
            }

            if (Seqs == null)
            {
                Seqs = new List<long>();
            }
            
            AddExpression(Seq, w => w.Seq == Seq);
            AddExpression(Seqs, w => this.Seqs.Contains(w.Seq));
            AddExpression(SeqEntidadeResponsavel, w => w.SeqEntidadeResponsavel == this.SeqEntidadeResponsavel);
            AddExpression(NodeRaiz, w => !w.SeqFormacaoEspecificaSuperior.HasValue && this.NodeRaiz.Value);
            AddExpression(TokenTipoFormacaoEspecifica, w => w.TipoFormacaoEspecifica.Token == this.TokenTipoFormacaoEspecifica);
            AddExpression(Ativo, w => w.Ativo == this.Ativo);
            AddExpression(SeqEntidades, w => this.SeqEntidades.Contains(w.SeqEntidadeResponsavel.Value));
            AddExpression(SeqCursoOfertaLocalidade, w => w.CursosOfertaLocalidade.Any(a => a.SeqCursoOfertaLocalidade == this.SeqCursoOfertaLocalidade));

            return GetExpression();
        }
    }
}