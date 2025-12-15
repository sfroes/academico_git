using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ProcessoEtapaConfiguracaoNotificacaoSpecification : SMCSpecification<ProcessoEtapaConfiguracaoNotificacao>
    {
        public long? Seq { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public long? SeqProcesso { get; set; }

        public long? SeqTipoNotificacao { get; set; }
        public long[] SeqsTipoNotificacao { get; set; }
        public long? SeqServico{ get; set; }

        public long? SeqProcessoUnidadeResponsavel { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public bool? PermiteAgendamento { get; set; }

        public List<long> SeqsGrupoEscalonamento { get; set; }

        public override Expression<Func<ProcessoEtapaConfiguracaoNotificacao, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, a => a.Seq == this.Seq);
            AddExpression(this.SeqProcessoEtapa, a => a.SeqProcessoEtapa == this.SeqProcessoEtapa.Value);
            AddExpression(this.SeqProcesso, a => a.ProcessoEtapa.SeqProcesso == this.SeqProcesso);
            AddExpression(this.SeqTipoNotificacao, a => a.SeqTipoNotificacao == this.SeqTipoNotificacao);
            AddExpression(this.SeqsTipoNotificacao, a => this.SeqsTipoNotificacao.Contains(a.SeqTipoNotificacao));
            AddExpression(this.SeqProcessoUnidadeResponsavel, a => a.SeqProcessoUnidadeResponsavel == this.SeqProcessoUnidadeResponsavel);
            AddExpression(this.SeqEntidadeResponsavel, a => a.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == this.SeqEntidadeResponsavel);
            AddExpression(this.PermiteAgendamento, a => a.TipoNotificacao.PermiteAgendamento == this.PermiteAgendamento);
            AddExpression(this.SeqsGrupoEscalonamento, x => x.ParametrosEnvioNotificacao.Any(a => SeqsGrupoEscalonamento.Contains(a.SeqGrupoEscalonamento.Value)));
            AddExpression(this.SeqServico, a => a.ProcessoEtapa.Processo.SeqServico == this.SeqServico);

            return GetExpression();
        }
    }
}
