using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class ServicoFilterSpecification : SMCSpecification<Servico>
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqTipoServico { get; set; }

        public long[] SeqsTiposServicos { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public long? SeqSituacaoAluno { get; set; }

        public PermissaoServico? PermissaoServico { get; set; }

        public long? SeqInstituicaoNivelTipoVinculoAluno { get; set; }

        public OrigemSolicitacaoServico? OrigemSolicitacaoServico { get; set; }

        public bool? Com1EtapaAtiva { get; set; }

        public string Token { get; set; }

        public long? SeqEntidadeResponsavelProcesso { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public TipoUnidadeResponsavel? TipoUnidadeResponsavel { get; set; }

        public override Expression<Func<Servico, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(OrigemSolicitacaoServico, w => w.OrigemSolicitacaoServico == OrigemSolicitacaoServico);
            AddExpression(Descricao, w => w.Descricao.Contains(Descricao));
            AddExpression(SeqTipoServico, w => w.SeqTipoServico == SeqTipoServico.Value);
            AddExpression(SeqsTiposServicos, w => this.SeqsTiposServicos.Contains(w.SeqTipoServico));
            AddExpression(SeqInstituicaoEnsino, w => w.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(TipoAtuacao, w => w.TipoAtuacao == TipoAtuacao);
            AddExpression(SeqInstituicaoNivelTipoVinculoAluno, w => w.InstituicaoNivelServicos.Any(i => i.SeqInstituicaoNivelTipoVinculoAluno == SeqInstituicaoNivelTipoVinculoAluno));
            AddExpression(Token, w => w.Token == Token);
            AddExpression(SeqCicloLetivo, w => w.Processos.Any(p => p.SeqCicloLetivo == SeqCicloLetivo));

            if (SeqSituacaoAluno.HasValue)
            {
                if (PermissaoServico.HasValue)
                {
                    AddExpression(w => w.SituacoesAluno.Any(s => s.SeqSituacaoMatricula == SeqSituacaoAluno.Value && s.PermissaoServico == PermissaoServico));
                }
                else
                {
                    AddExpression(w => w.SituacoesAluno.Any(s => s.SeqSituacaoMatricula == SeqSituacaoAluno.Value));
                }
            }
            else
            {
                AddExpression(PermissaoServico, w => w.SituacoesAluno.Any(s => s.PermissaoServico == PermissaoServico));
            }

            // Verifica se o processo vigente tem entidade responsável do aluno
            AddExpression(SeqEntidadeResponsavelProcesso, w => w.Processos.Any(p => p.UnidadesResponsaveis.Any(u => u.SeqEntidadeResponsavel == SeqEntidadeResponsavelProcesso)));

            if (Com1EtapaAtiva.HasValue && Com1EtapaAtiva.Value)
            {
                AddExpression(w => w.Processos.Any(p => DateTime.Now >= p.DataInicio &&
                                                        (!p.DataFim.HasValue || DateTime.Now <= p.DataFim) &&
                                                        p.Etapas.Any(e => e.Ordem == 1 &&
                                                                            e.SituacaoEtapa == SituacaoEtapa.Liberada &&
                                                                            (!e.DataInicio.HasValue || e.DataInicio.Value <= DateTime.Now) &&
                                                                            (!e.DataFim.HasValue || e.DataFim.Value >= DateTime.Now))
                                                        ));
            }

            AddExpression(TipoUnidadeResponsavel, w => w.Processos.Any(p => p.UnidadesResponsaveis.Any(u => u.TipoUnidadeResponsavel == TipoUnidadeResponsavel)));

            return GetExpression();
        }
    }
}