using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ViewCentralSolicitacaoServicoFilterSpecification : SMCSpecification<ViewCentralSolicitacaoServico>
    {
        public long? Seq { get; set; }

        public string NumeroProtocolo { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqServico { get; set; }

        public List<long> SeqsServicos { get; set; }

        public long? SeqProcesso { get; set; }

        public List<long> SeqsProcessos { get; set; }

        public CategoriaSituacao? CategoriaSituacao { get; set; }

        public List<CategoriaSituacao> CategoriasSituacoes { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public long? SeqSituacaoEtapa { get; set; }

        public TipoFiltroCentralSolicitacao? TipoFiltroCentralSolicitacao { get; set; }

        public long? SeqUsuarioResponsavel { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public bool? DisponivelParaAtendimento { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        public long? SeqBloqueio { get; set; }

        public DateTime? DataInclusaoInicio { get; set; }

        public DateTime? DataInclusaoFim { get; set; }

        public bool? PossuiUsuarioResponsavel { get; set; }

        public bool? PossuiBloqueio { get; set; }

        public List<long> SeqsSituacoesEtapasSGF { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long? SeqSituacaoMatriculaAluno { get; set; }

        public long? SeqConfiguaracaoEtapa { get; set; }



        public override Expression<Func<ViewCentralSolicitacaoServico, bool>> SatisfiedBy()
        {
            var dataAtual = DateTime.Now;

            AddExpression(Seq, x => Seq == x.SeqSolicitacaoServico);

            AddExpression(SeqProcesso, x => SeqProcesso.Value == x.SeqProcesso);

            AddExpression(SeqsProcessos, x => SeqsProcessos.Contains(x.SeqProcesso));

            AddExpression(SeqPessoa, x => SeqPessoa.Value == x.SeqPessoaSolicitante);

            AddExpression(SeqServico, x => SeqServico.Value == x.SeqServico);

            AddExpression(SeqsServicos, x => SeqsServicos.Contains(x.SeqServico));

            AddExpression(CategoriaSituacao, x => CategoriaSituacao == x.CategoriaSituacao);

            AddExpression(CategoriasSituacoes, x => CategoriasSituacoes.Contains(x.CategoriaSituacao));


            if (DisponivelParaAtendimento.HasValue)
            {
                if (DisponivelParaAtendimento.Value)
                {
                    AddExpression(x => x.CentralAtendimento == DisponivelParaAtendimento);

                    AddExpression(x => SeqsSituacoesEtapasSGF.Contains(x.SeqSituacaoEtapaSGF));
                }
                else
                {
                    AddExpression(x => !x.CentralAtendimento || SeqsSituacoesEtapasSGF.Contains(x.SeqSituacaoEtapaSGF));
                }
            }
            else
            {
                AddExpression(DisponivelParaAtendimento, x => x.CentralAtendimento == DisponivelParaAtendimento);

                AddExpression(SeqsSituacoesEtapasSGF, x => SeqsSituacoesEtapasSGF.Contains(x.SeqSituacaoEtapaSGF));
            }

            AddExpression(() => PossuiUsuarioResponsavel.HasValue && PossuiUsuarioResponsavel.Value, x => x.SeqUsuarioResponsavelSAS.HasValue);

            AddExpression(SeqUsuarioResponsavel, x => x.SeqUsuarioResponsavelSAS == SeqUsuarioResponsavel);

            AddExpression(SeqGrupoEscalonamento, x => x.SeqGrupoEscalonamento == SeqGrupoEscalonamento.Value);

          
            if (!this.SeqsEntidadesResponsaveis.SMCIsNullOrEmpty())            
            {
                AddExpression(x => this.SeqsEntidadesResponsaveis.Any(s => s == x.SeqEntidadeResponsavel) ||
                                    (x.SeqEntidadeCompartilhada.HasValue &&
                                     this.SeqsEntidadesResponsaveis.Any(s => s == x.SeqEntidadeCompartilhada.Value)));

            }

            switch (TipoFiltroCentralSolicitacao.GetValueOrDefault())
            {
                case SMC.Academico.Common.Areas.SRC.Enums.TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao:

                    AddExpression(SeqSituacaoEtapa, x => x.SolicitacaoServico.SituacaoAtual.SeqSituacaoEtapaSgf == SeqSituacaoEtapa);
                    AddExpression(SeqProcessoEtapa, x => x.SolicitacaoServico.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf == SeqProcessoEtapa);

                    break;

                case SMC.Academico.Common.Areas.SRC.Enums.TipoFiltroCentralSolicitacao.SituacaoEtapaSelecionada:

                    AddExpression(SeqSituacaoEtapa, x => x.SolicitacaoServico.Etapas.Any(e => e.HistoricosSituacao.Any(h => h.SeqSituacaoEtapaSgf == SeqSituacaoEtapa)));
                    AddExpression(SeqProcessoEtapa, x => x.SolicitacaoServico.Etapas.Any(e => e.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf == SeqProcessoEtapa));

                    break;
            }

            AddExpression(() => SituacaoDocumentacao != SituacaoDocumentacao.Nenhum, x => x.SituacaoDocumentacao == SituacaoDocumentacao);

            //AddExpression(SeqBloqueio, x => x.SolicitacaoServico.PessoaAtuacao.Bloqueios.Where(b => x.SolicitacaoServico.GrupoEscalonamento.Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == x.SolicitacaoServico.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf).Escalonamento.ProcessoEtapa.ConfiguracoesEtapa.SelectMany(c => c.ConfiguracoesBloqueio.Select(cb => cb.SeqMotivoBloqueio)).Any(bb => bb == b.SeqMotivoBloqueio)).Any(b => b.SeqMotivoBloqueio == SeqBloqueio.Value && b.SituacaoBloqueio == SituacaoBloqueio.Bloqueado && b.DataBloqueio <= dataAtual));
            AddExpression(PossuiBloqueio, x => x.BloqueioFimEtapa.Value == PossuiBloqueio);
            AddExpression(SeqBloqueio, x => x.SolicitacaoServico.PessoaAtuacao.Bloqueios.Any(a => a.SeqMotivoBloqueio == SeqBloqueio && a.SituacaoBloqueio == SituacaoBloqueio.Bloqueado));

            AddExpression(NumeroProtocolo, x => x.NumeroProtocolo.Contains(NumeroProtocolo));

            AddExpression(() => TipoAtuacao.HasValue && TipoAtuacao != Common.Areas.PES.Enums.TipoAtuacao.Nenhum, x => x.TipoAtuacao == TipoAtuacao);

            AddExpression(SeqTipoVinculoAluno, x => x.SeqTipoVinculoAluno == SeqTipoVinculoAluno);

            if (SeqSituacaoMatriculaAluno.HasValue && TipoAtuacao.HasValue && TipoAtuacao.Value != Common.Areas.PES.Enums.TipoAtuacao.Nenhum)
            {
                switch (TipoAtuacao)
                {
                    case Common.Areas.PES.Enums.TipoAtuacao.Aluno:
                        AddExpression(SeqSituacaoMatriculaAluno, x => x.SeqSituacaoMatriculaAluno == SeqSituacaoMatriculaAluno);
                        break;

                    case Common.Areas.PES.Enums.TipoAtuacao.Ingressante:
                        AddExpression(SeqSituacaoMatriculaAluno, x => x.SituacaoIngressante == (SituacaoIngressante)SeqSituacaoMatriculaAluno);
                        break;
                }
            }

            if (DataInclusaoInicio.HasValue)
            {
                AddExpression(DataInclusaoInicio, x => x.DataInclusaoSolicitacao >= DataInclusaoInicio.Value);
            }

            if (DataInclusaoFim.HasValue)
            {
                var dataInclusaoFim = DataInclusaoFim.Value.AddDays(1).AddSeconds(-1);
                AddExpression(DataInclusaoFim, x => x.DataInclusaoSolicitacao <= dataInclusaoFim);
            }

            AddExpression(SeqConfiguaracaoEtapa, a => a.SeqConfiguracaoEtapa == SeqConfiguaracaoEtapa);

            return this.GetExpression();
        }
    }
}