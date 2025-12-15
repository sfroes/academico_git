using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.FIN.Specifications
{
    public class PessoaAtuacaoBeneficioFilterSpecification : SMCSpecification<PessoaAtuacaoBeneficio>
    {
        public long[] Seqs { get; set; }

        public long? SeqBeneficio { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqConfiguracaoBeneficio { get; set; }

        public bool? IncideParcelaMatricula { get; set; }

        public SituacaoChancelaBeneficio? SituacaoChancelaBeneficio { get; set; }

        public DateTime? DataReferenciaVigencia { get; set; }

        public bool? RegistroAtivo { get; set; }

        public bool? Excluidos { get; set; }

        public bool? ConcessaoAteFinalCurso { get; set; }
        public bool? DesbloqueioTemporario { get; set; }
        public TipoAtuacao? TipoAtuacao { get; set; }
        public DateTime? DataInicioCicloLetivo { get; set; }

        public override Expression<Func<PessoaAtuacaoBeneficio, bool>> SatisfiedBy()
        {
            //Trazer todos exceto os excluidos quando false
            AddExpression(() => this.Excluidos.HasValue && !this.Excluidos.Value, a => a.HistoricoSituacoes.FirstOrDefault(f => f.Atual).SituacaoChancelaBeneficio != Common.Areas.FIN.Enums.SituacaoChancelaBeneficio.Excluido);
            AddExpression(this.Seqs, p => this.Seqs.Contains(p.Seq));
            AddExpression(this.SeqBeneficio, p => p.SeqBeneficio == this.SeqBeneficio);
            AddExpression(this.SeqPessoaAtuacao, p => p.SeqPessoaAtuacao == this.SeqPessoaAtuacao);

            if(TipoAtuacao.HasValue && TipoAtuacao.Value == Common.Areas.PES.Enums.TipoAtuacao.Ingressante)
            {
                AddExpression(this.IncideParcelaMatricula, p => p.IncideParcelaMatricula == this.IncideParcelaMatricula);
            }

            AddExpression(this.SituacaoChancelaBeneficio, p => p.HistoricoSituacoes.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault().SituacaoChancelaBeneficio == this.SituacaoChancelaBeneficio);
            AddExpression(this.SeqConfiguracaoBeneficio, p => p.SeqConfiguracaoBeneficio == this.SeqConfiguracaoBeneficio);
            //AddExpression(this.DataReferenciaVigencia, p => p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia <= DataReferenciaVigencia && DataReferenciaVigencia <= p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia);
            AddExpression(this.DataReferenciaVigencia, p => DataReferenciaVigencia >= p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataInicioVigencia && DataReferenciaVigencia <= p.HistoricoVigencias.FirstOrDefault(h => h.Atual).DataFimVigencia);
            AddExpression(this.RegistroAtivo, p => p.HistoricoSituacoes.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault().SituacaoChancelaBeneficio != Common.Areas.FIN.Enums.SituacaoChancelaBeneficio.Excluido);
            AddExpression(this.ConcessaoAteFinalCurso, p => p.Beneficio.ConcessaoAteFinalCurso == this.ConcessaoAteFinalCurso);
            AddExpression(this.DesbloqueioTemporario, p => p.Beneficio.IncluirDesbloqueioTemporario == this.DesbloqueioTemporario);
            
            if(TipoAtuacao.HasValue && this.TipoAtuacao.Value == Common.Areas.PES.Enums.TipoAtuacao.Aluno)
            {
                AddExpression(p => this.DataInicioCicloLetivo >= p.HistoricoVigencias.FirstOrDefault(hv => hv.Atual).DataInicioVigencia && this.DataInicioCicloLetivo <= p.HistoricoVigencias.FirstOrDefault(hv => hv.Atual).DataFimVigencia);
            }

            return GetExpression();
        }
    }
}