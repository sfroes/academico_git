using Microsoft.IdentityModel.Protocols.WSTrust.Bindings;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.MAT.Specifications
{
    public class SolicitacaoMatriculaItemFilterSpecification : SMCSpecification<SolicitacaoMatriculaItem>
    {
        public long? Seq { get; set; }

        public long? SeqSolicitacaoMatricula { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public List<long> SeqsDivisoesTurma { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqTurma { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public bool? ExibirTurma { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }

        public ClassificacaoSituacaoFinal[] ClassificacaoSituacoesFinais { get; set; }

        public ClassificacaoSituacaoFinal[] ClassificacaoSituacoesFinaisDiferentes { get; set; }

        public bool RegistroAtividade { get; set; }

        public long[] SeqSolicitacoesMatriculaItem { get; set; }

        public long[] SeqsSolicitacoesServicos { get; set; }

        public MotivoSituacaoMatricula? MotivoSituacaoMatriculaDiferente { get; set; }

        public MotivoSituacaoMatricula[] MotivosSituacaoMatriculaDiferente { get; set; }
        public MotivoSituacaoMatricula[] MotivosSituacaoMatriculaIgual { get; set; }

        public MotivoSituacaoMatricula? MotivoSituacaoMatricula { get; set; }

        public bool? SolicitacaoPlanoEstudoAlterar { get; set; }

        public bool? SolicitacaoMatriculaAtiva { get; set; }

        public bool? PertencePlanoEstudo { get; set; }
        public bool? RegraSelecaoTurma { get; set; }
        public bool? RegraListagemTurma { get; set; }

        public bool? DiferentePertencePlanoEstudo { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        /// <summary>
        /// Quando um item pertence ao plano de estudo, verifica se (SituacaoFinal == Cancelado && Motipo == PelaInstituicao) == ValorInformado
        /// </summary>
        public bool? ItemDoPlanoCanceladoPelaInstituicao { get; set; }

        /// <summary>
        /// Filtrar os itens (que pertencem ao plano de estudo) ou (que não pertencem ao plano de estudo e motivo for diferente de pelo solicitante)
        /// </summary>
        public bool? ItemDoPlanoPertenceAoPlanoDeEstudoOuNaoPertenceEMotivoCancelamentoNaoEPeloSolicitante { get; set; }

        /// <summary>
        /// Quando um item não pertence ao plano de estudo, verifica se (SituacaoFinal == Cancelado && Motipo == PelaInstituicao) == ValorInformado
        /// </summary>
        public bool? ItemDaMatriculaCanceladoPelaInstituicao { get; set; }

        public IEnumerable<long> SeqsSituacoesItemMatricula { get; set; }

        public bool DesconsiderarEtapa { get; set; }

        public override Expression<Func<SolicitacaoMatriculaItem, bool>> SatisfiedBy()
        {
            var arrayMotivos = new Common.Areas.MAT.Enums.MotivoSituacaoMatricula[]
            {
                Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PeloSolicitante, Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PorTrocaDeGrupo
            };

            var classificacoesDiferenteTurma = new ClassificacaoSituacaoFinal[]
            {
               Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.Cancelado, Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoSemSucesso
            };

            var motivosDiferenteTurma = new Common.Areas.MAT.Enums.MotivoSituacaoMatricula[]
            {
                Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PelaInstituicao, Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PorTrocaDeGrupo, Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PorDispensaAprovacao
            };


            AddExpression(Seq, w => w.Seq == this.Seq.Value);
            AddExpression(SeqSolicitacaoMatricula, w => w.SeqSolicitacaoMatricula == this.SeqSolicitacaoMatricula);
            AddExpression(SeqsDivisoesTurma, w => w.SeqDivisaoTurma.HasValue && SeqsDivisoesTurma.Contains(w.SeqDivisaoTurma.Value));
            AddExpression(SeqDivisaoTurma, w => w.SeqDivisaoTurma == this.SeqDivisaoTurma);
            AddExpression(SeqConfiguracaoComponente, w => w.SeqConfiguracaoComponente == this.SeqConfiguracaoComponente);
            AddExpression(SeqTurma, w => w.DivisaoTurma.SeqTurma == this.SeqTurma);
            AddExpression(SituacaoTurmaAtual, w => w.DivisaoTurma.Turma.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoTurma == this.SituacaoTurmaAtual);
            AddExpression(ExibirTurma, w => w.SeqDivisaoTurma.HasValue == this.ExibirTurma);
            AddExpression(ClassificacaoSituacaoFinal, w => w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == this.ClassificacaoSituacaoFinal);

            AddExpression(() => SolicitacaoMatriculaAtiva.GetValueOrDefault(), w => w.SolicitacaoMatricula.SituacaoAtual.CategoriaSituacao == CategoriaSituacao.EmAndamento || w.SolicitacaoMatricula.SituacaoAtual.CategoriaSituacao == CategoriaSituacao.Novo);


            AddExpression(ClassificacaoSituacoesFinais, w => this.ClassificacaoSituacoesFinais.Contains(w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal.Value));
            AddExpression(ClassificacaoSituacoesFinaisDiferentes, w => !this.ClassificacaoSituacoesFinaisDiferentes.Contains(w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal.Value));
            AddExpression(() => RegistroAtividade, w => !w.SeqDivisaoTurma.HasValue);
            AddExpression(SeqSolicitacoesMatriculaItem, w => this.SeqSolicitacoesMatriculaItem.Contains(w.Seq));
            AddExpression(SeqsSolicitacoesServicos, w => this.SeqsSolicitacoesServicos.Contains(w.SeqSolicitacaoMatricula));

            AddExpression(MotivosSituacaoMatriculaDiferente, w => !this.MotivosSituacaoMatriculaDiferente.Any(x => x == w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula));
            AddExpression(MotivosSituacaoMatriculaIgual, w => this.MotivosSituacaoMatriculaIgual.Any(x => x == w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula));
            AddExpression(MotivoSituacaoMatriculaDiferente, w => w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula != this.MotivoSituacaoMatriculaDiferente);
            AddExpression(MotivoSituacaoMatricula, w => w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula == this.MotivoSituacaoMatricula);
            //AddExpression(SolicitacaoPlanoEstudoAlterar, w =>
            //!(
            //    w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoSemSucesso
            //    || (
            //            w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.Cancelado
            //         && w.PertencePlanoEstudo == false
            //        )
            //    || (
            //           w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.Cancelado
            //        && w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula == Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PorTrocaDeGrupo
            //       )
            //) || (w.PertencePlanoEstudo == true && !SeqTurma.HasValue));
            AddExpression(SolicitacaoPlanoEstudoAlterar, w =>
            (w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.Cancelado
            && w.PertencePlanoEstudo
            && w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula == Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PeloSolicitante || w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula == Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PorTrocaDeGrupo)
            || w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.NaoAlterado
            || w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoComSucesso);

            AddExpression(PertencePlanoEstudo, w => w.PertencePlanoEstudo == PertencePlanoEstudo);
            AddExpression(DiferentePertencePlanoEstudo, w => w.PertencePlanoEstudo != DiferentePertencePlanoEstudo);
            AddExpression(SeqProcessoEtapa, w => w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.SeqProcessoEtapa == SeqProcessoEtapa);
            AddExpression(ItemDoPlanoCanceladoPelaInstituicao, w => w.PertencePlanoEstudo != true ||
                (w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.Cancelado &&
                 w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula == Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PelaInstituicao
                ) == ItemDoPlanoCanceladoPelaInstituicao);

            if (ItemDoPlanoPertenceAoPlanoDeEstudoOuNaoPertenceEMotivoCancelamentoNaoEPeloSolicitante.GetValueOrDefault())
                AddExpression(x => x.PertencePlanoEstudo == true || (x.PertencePlanoEstudo != true && !arrayMotivos.Any(a => a == x.HistoricosSituacao.Where(h => DesconsiderarEtapa || SeqsSituacoesItemMatricula.Contains(h.SeqSituacaoItemMatricula)).OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula)));

            if (SeqsSituacoesItemMatricula != null)
                AddExpression(SeqsSituacoesItemMatricula, x => x.HistoricosSituacao.Any(h => DesconsiderarEtapa || SeqsSituacoesItemMatricula.Contains(h.SeqSituacaoItemMatricula)));

            AddExpression(ItemDaMatriculaCanceladoPelaInstituicao, w => w.PertencePlanoEstudo == true ||
                (w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.Cancelado &&
                 w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula == Common.Areas.MAT.Enums.MotivoSituacaoMatricula.PelaInstituicao
                ) == ItemDaMatriculaCanceladoPelaInstituicao);
            
            
            AddExpression(RegraSelecaoTurma, w => 
                             w.PertencePlanoEstudo == false && w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal.Value == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.FinalizadoComSucesso
                             || w.PertencePlanoEstudo == true
                             && w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal.Value == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.NaoAlterado);


            AddExpression(RegraListagemTurma, w => w.PertencePlanoEstudo == false && !classificacoesDiferenteTurma.Contains(w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal.Value)
            || w.PertencePlanoEstudo == true 
            && w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal.Value == Formularios.Common.Areas.TMP.Enums.ClassificacaoSituacaoFinal.Cancelado
            && !motivosDiferenteTurma.Contains(w.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().MotivoSituacaoMatricula.Value)
            || w.PertencePlanoEstudo == true );

            return GetExpression();
        }
    }
}