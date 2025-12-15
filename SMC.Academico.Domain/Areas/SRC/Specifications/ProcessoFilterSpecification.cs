using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ProcessoFilterSpecification : SMCSpecification<Processo>
    {
        public long? Seq { get; set; }

        public long? SeqUnidadeResponsavel { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqTipoServico { get; set; }

        public long? SeqServico { get; set; }

        public string Descricao { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public DateTime? DataInicio { get; set; }

        private DateTime? DataInicioMax { get => (DataInicio.HasValue) ? DataInicio.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999) : DataInicio; }

        public DateTime? DataFim { get; set; }

        private DateTime? DataFimMax { get => (DataFim.HasValue) ? DataFim.Value.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999) : DataFim; }

        public OrigemSolicitacaoServico? OrigemSolicitacaoServico { get; set; }

        public long? SeqSituacaoAluno { get; set; }

        public PermissaoServico? PermissaoServico { get; set; }

        public long? SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public bool? Com1EtapaAtiva { get; set; }

        public long[] SeqsServicos { get; set; }

        public long[] SeqsEntidadesResponsaveis { get; set; }

        public long[] SeqsProcesso { get; set; }

        public TipoUnidadeResponsavel? TipoUnidadeResponsavel { get; set; }

        public bool? ListarProcessosEncerrados { get; set; }

        public string TokenServico { get; set; }

        public bool? ProcessoAtivo { get; set; }

        public override Expression<Func<Processo, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq.Value);
            AddExpression(SeqUnidadeResponsavel, w => w.UnidadesResponsaveis.Any(a => a.SeqEntidadeResponsavel == SeqUnidadeResponsavel));
            AddExpression(SeqCicloLetivo, w => w.CicloLetivo.Seq == SeqCicloLetivo.Value);
            AddExpression(SeqTipoServico, w => w.Servico.SeqTipoServico == SeqTipoServico.Value);
            AddExpression(SeqServico, w => w.Servico.Seq == SeqServico.Value);
            AddExpression(Descricao, w => w.Descricao.Contains(Descricao));
            AddExpression(OrigemSolicitacaoServico, w => w.Servico.OrigemSolicitacaoServico == OrigemSolicitacaoServico);
            AddExpression(TipoAtuacao, w => w.Servico.TipoAtuacao == TipoAtuacao);
            AddExpression(SeqInstituicaoNivelTipoVinculoAluno, w => w.Servico.InstituicaoNivelServicos.Any(i => i.SeqInstituicaoNivelTipoVinculoAluno == SeqInstituicaoNivelTipoVinculoAluno));
            AddExpression(SeqsServicos, w => SeqsServicos.Contains(w.SeqServico));
            AddExpression(SeqsEntidadesResponsaveis, w => w.UnidadesResponsaveis.Any(a => SeqsEntidadesResponsaveis.Contains(a.SeqEntidadeResponsavel)));
            AddExpression(SeqsProcesso, w => SeqsProcesso.Contains(w.Seq));
            AddExpression(TipoUnidadeResponsavel, w => w.UnidadesResponsaveis.Any(u => u.TipoUnidadeResponsavel == TipoUnidadeResponsavel));
            AddExpression(TokenServico, w => w.Servico.Token == TokenServico);

            //Ao informar as duas datas, Inicio e Fim, deve ser feito o intervalo
            if (DataInicio.HasValue && DataFim.HasValue)
            {
                AddExpression(DataInicio, w => (w.DataInicio >= DataInicio && w.DataFim <= DataFimMax));
            }
            else
            {
                //Tratar como campos independentes
                AddExpression(DataInicio, w => w.DataInicio >= DataInicio && w.DataInicio <= DataInicioMax);
                AddExpression(DataFim, w => w.DataFim >= DataFim && w.DataFim <= DataFimMax);
            }

            if (SeqSituacaoAluno.HasValue)
            {
                if (PermissaoServico.HasValue)
                {
                    AddExpression(w => w.Servico.SituacoesAluno.Any(s => s.SeqSituacaoMatricula == SeqSituacaoAluno.Value && s.PermissaoServico == PermissaoServico));
                }
                else
                {
                    AddExpression(w => w.Servico.SituacoesAluno.Any(s => s.SeqSituacaoMatricula == SeqSituacaoAluno.Value));
                }
            }
            else
            {
                AddExpression(PermissaoServico, w => w.Servico.SituacoesAluno.Any(s => s.PermissaoServico == PermissaoServico));
            }

            if (Com1EtapaAtiva.HasValue && Com1EtapaAtiva.Value)
            {
                AddExpression(w => DateTime.Now >= w.DataInicio &&
                                   (!w.DataFim.HasValue || DateTime.Now <= w.DataFim) &&
                                   w.Etapas.Any(e => e.Ordem == 1 &&
                                                e.SituacaoEtapa == SituacaoEtapa.Liberada &&
                                                (!e.DataInicio.HasValue || e.DataInicio.Value <= DateTime.Now) &&
                                                (!e.DataFim.HasValue || e.DataFim.Value >= DateTime.Now))
                );
            }

            if (ListarProcessosEncerrados.HasValue && !ListarProcessosEncerrados.Value)
            {
                AddExpression(w => !w.DataEncerramento.HasValue || (w.DataEncerramento.HasValue && w.DataEncerramento.Value >= DateTime.Now));
            }

            if (ProcessoAtivo.HasValue && ProcessoAtivo.Value)
            {
                AddExpression(w => DateTime.Now >= w.DataInicio &&
                                   (!w.DataFim.HasValue || DateTime.Now <= w.DataFim));
            }

            return GetExpression();
        }
    }
}