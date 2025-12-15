using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoDispensaFilterSpecification : SMCSpecification<SolicitacaoDispensa>
    {
        public long? Seq { get; set; }

        public List<long> SeqsPessoaAtuacao { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqHistoricoEscolar { get; set; }

        public long? SeqSituacaoEtapaAtual { get; set; }

        public List<long> SeqsSituacaoEtapaAtual { get; set; }

        public long? SeqAlunoHistorico { get; set; }

        public override Expression<Func<SolicitacaoDispensa, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => this.Seq.Value == x.Seq);
            AddExpression(SeqPessoaAtuacao, x => this.SeqPessoaAtuacao == x.SeqPessoaAtuacao);
            AddExpression(SeqsPessoaAtuacao, x => this.SeqsPessoaAtuacao.Contains(x.SeqPessoaAtuacao));
            AddExpression(SeqHistoricoEscolar, x => x.OrigensInternas.Any(i => i.SeqHistoricoEscolar == this.SeqHistoricoEscolar.Value));
            AddExpression(SeqSituacaoEtapaAtual, x => x.SituacaoAtual.SeqSituacaoEtapaSgf == this.SeqSituacaoEtapaAtual);
            AddExpression(SeqsSituacaoEtapaAtual, x => SeqsSituacaoEtapaAtual.Contains(x.SituacaoAtual.SeqSituacaoEtapaSgf));
            AddExpression(SeqAlunoHistorico, x => x.SeqAlunoHistorico == this.SeqAlunoHistorico);

            return GetExpression();
        }
    }
}