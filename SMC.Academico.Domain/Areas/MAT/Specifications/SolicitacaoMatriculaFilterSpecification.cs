using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.MAT.Specifications
{
    public class SolicitacaoMatriculaFilterSpecification : SMCSpecification<SolicitacaoMatricula>
    {
        public Guid? CodigoAdesao { get; set; }

        public long? Seq { get; set; }

        public long? SeqColaborador { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public bool? ApenasAptosParaMatricula { get; set; }

        public string[] TokensTiposServico { get; set; }

		public bool? ApenasProcessosAtuais { get; set; }

		public bool? ApenasProcessosAtuaisIngressante { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public CategoriaSituacao[] CategoriasSituacao { get; set; }

        public string[] TokensServico { get; set; }

        public long? SeqUsuarioSAS { get; set; }

        public AcaoLiberacaoTrabalho? AcaoLiberacaoTrabalho { get; set; }

        public long? SeqCicloLetivoProcesso { get; set; }

        public long? SeqDivisaoTurma { get; set; }

        public bool? ItemAtivoDivisao { get; set; }

        public override Expression<Func<SolicitacaoMatricula, bool>> SatisfiedBy()
        {
            AddExpression(CodigoAdesao, w => w.CodigoAdesao == this.CodigoAdesao.Value);
            AddExpression(SeqColaborador, w =>
                          w.PessoaAtuacao.TipoAtuacao == Common.Areas.PES.Enums.TipoAtuacao.Ingressante ?
                          w.PessoaAtuacao.OrientacoesPessoaAtuacao.Any(o => o.Orientacao.OrientacoesColaborador.Any(op => op.Colaborador.Pessoa.SeqUsuarioSAS == SeqColaborador && (!op.DataInicioOrientacao.HasValue || op.DataInicioOrientacao <= DateTime.Now) && (!op.DataFimOrientacao.HasValue || op.DataFimOrientacao >= DateTime.Now))) :
                          w.PessoaAtuacao.OrientacoesPessoaAtuacao.Any(o => o.Orientacao.TipoOrientacao.Token == TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO && o.Orientacao.OrientacoesColaborador.Any(op => op.Colaborador.Pessoa.SeqUsuarioSAS == SeqColaborador && op.DataInicioOrientacao <= DateTime.Now && (!op.DataFimOrientacao.HasValue || op.DataFimOrientacao >= DateTime.Now))));
            //AddExpression(SeqColaborador, w => w.PessoaAtuacao.OrientacoesPessoaAtuacao.Any(o => o.Orientacao.OrientacoesColaborador.Any(op => op.Colaborador.Pessoa.SeqUsuarioSAS == SeqColaborador)));

            AddExpression(Seq, w => w.Seq == this.Seq.Value);
            AddExpression(SeqPessoa, w => w.PessoaAtuacao.SeqPessoa == this.SeqPessoa.Value);
            AddExpression(SeqPessoaAtuacao, w => w.PessoaAtuacao.Seq == this.SeqPessoaAtuacao);
            AddExpression(ApenasAptosParaMatricula, w =>
                            w.PessoaAtuacao.TipoAtuacao == Common.Areas.PES.Enums.TipoAtuacao.Ingressante ?
							// Caso seja ingressante, considera solicitações de pessoa atuação com situação apto para matrícula e
							// Se for informado a flag ApenasProcessosAtuaisIngressante = true, filtra a datafim do processo
							(((w.PessoaAtuacao as Ingressante).HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante == SituacaoIngressante.AptoMatricula && (!ApenasProcessosAtuaisIngressante.HasValue || !ApenasProcessosAtuaisIngressante.Value || (ApenasProcessosAtuaisIngressante.Value && (!w.ConfiguracaoProcesso.Processo.DataFim.HasValue || w.ConfiguracaoProcesso.Processo.DataFim >= DateTime.Now))))) :
							//(new SituacaoIngressante[] { SituacaoIngressante.AptoMatricula, SituacaoIngressante.Matriculado }.Contains((w.PessoaAtuacao as Ingressante).HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante) && (!ApenasProcessosAtuaisIngressante.HasValue || !ApenasProcessosAtuaisIngressante.Value || (ApenasProcessosAtuaisIngressante.Value && (!w.ConfiguracaoProcesso.Processo.DataFim.HasValue || w.ConfiguracaoProcesso.Processo.DataFim >= DateTime.Now)))) :
							(new string[] { "APTO_MATRICULA", "MATRICULADO" }.Contains((w.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.OrderByDescending(h => h.CicloLetivo.Ano).ThenByDescending(h => h.CicloLetivo.Numero).FirstOrDefault(h => !h.DataExclusao.HasValue).AlunoHistoricoSituacao.OrderByDescending(h => h.DataInicioSituacao).FirstOrDefault(h => h.DataInicioSituacao <= DateTime.Today && !h.DataExclusao.HasValue).SituacaoMatricula.Token)));
            AddExpression(TokensTiposServico, x => TokensTiposServico.Contains(x.ConfiguracaoProcesso.Processo.Servico.TipoServico.Token));
			//x.ConfiguracaoProcesso.Processo.DataInicio <= DateTime.Now && 
			AddExpression(ApenasProcessosAtuais, x => !x.ConfiguracaoProcesso.Processo.DataFim.HasValue || x.ConfiguracaoProcesso.Processo.DataFim >= DateTime.Now);
            AddExpression(SeqCicloLetivo, x => x.AlunosHistoricosCicloLetivo.Any(ah=>ah.SeqCicloLetivo == this.SeqCicloLetivo));
            AddExpression(CategoriasSituacao, x => CategoriasSituacao.Contains(x.SituacaoAtual.CategoriaSituacao));
            AddExpression(TokensServico, x => TokensServico.Contains(x.ConfiguracaoProcesso.Processo.Servico.Token));
            AddExpression(SeqUsuarioSAS, x => x.PessoaAtuacao.Pessoa.SeqUsuarioSAS == SeqUsuarioSAS);
            AddExpression(AcaoLiberacaoTrabalho, x => x.ConfiguracaoProcesso.Processo.Servico.AcaoLiberacaoTrabalho == AcaoLiberacaoTrabalho);
            AddExpression(SeqCicloLetivoProcesso, x => x.ConfiguracaoProcesso.Processo.SeqCicloLetivo == SeqCicloLetivoProcesso);
            AddExpression(SeqDivisaoTurma, x => x.Itens.Any(a => a.SeqDivisaoTurma == SeqDivisaoTurma));
            AddExpression(ItemAtivoDivisao, x => x.Itens.Any(a => a.SeqDivisaoTurma == SeqDivisaoTurma && (a.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso || a.HistoricosSituacao.OrderByDescending(o => o.Seq).FirstOrDefault().SituacaoItemMatricula.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado)));
           
            return GetExpression();
        }
    }
}