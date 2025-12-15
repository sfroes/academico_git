using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.MAT.Specifications
{
    public class ChancelaFilterSpecification : SMCSpecification<SolicitacaoMatricula>
    {       
        public Guid? Codigo { get; set; }

        public long? SeqColaborador { get; set; }

        public bool ApenasProcessoVigente { get; set; }

        public long? SeqProcesso { get; set; }

        public long[] SeqSituacoesPermitidas { get; set; }

        public FiltroDado? FiltroDadoProcesso { get; set; }

        public bool ApenasAguardandoChancela { get; set; }

        public override Expression<Func<SolicitacaoMatricula, bool>> SatisfiedBy()
        {
            AddExpression(SeqColaborador, w =>
                          w.PessoaAtuacao.TipoAtuacao == Common.Areas.PES.Enums.TipoAtuacao.Ingressante ?
                          w.PessoaAtuacao.OrientacoesPessoaAtuacao.Any(o => o.Orientacao.OrientacoesColaborador.Any(op => op.Colaborador.Pessoa.SeqUsuarioSAS == SeqColaborador && (!op.DataInicioOrientacao.HasValue || op.DataInicioOrientacao <= DateTime.Now) && (!op.DataFimOrientacao.HasValue || op.DataFimOrientacao >= DateTime.Now))):
                          w.PessoaAtuacao.OrientacoesPessoaAtuacao.Any(o => o.Orientacao.TipoOrientacao.Token == TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO && o.Orientacao.OrientacoesColaborador.Any(op => op.Colaborador.Pessoa.SeqUsuarioSAS == SeqColaborador && op.DataInicioOrientacao <= DateTime.Now && (!op.DataFimOrientacao.HasValue || op.DataFimOrientacao >= DateTime.Now))));
            //AddExpression(SeqColaborador, w => w.PessoaAtuacao.OrientacoesPessoaAtuacao.Any(o => o.Orientacao.OrientacoesColaborador.Any(op => op.Colaborador.Pessoa.SeqUsuarioSAS == SeqColaborador)));

            AddExpression(Codigo, w => w.CodigoAdesao == Codigo);
            AddExpression(SeqSituacoesPermitidas, w => SeqSituacoesPermitidas.Contains(w.SituacaoAtual.SeqSituacaoEtapaSgf));
            AddExpression(()=>ApenasProcessoVigente, w => w.ConfiguracaoProcesso.Processo.DataInicio <= DateTime.Now && (!w.ConfiguracaoProcesso.Processo.DataFim.HasValue || w.ConfiguracaoProcesso.Processo.DataFim >= DateTime.Now));
            AddExpression(SeqProcesso, w => w.ConfiguracaoProcesso.SeqProcesso == SeqProcesso);
            AddExpression(FiltroDadoProcesso, w => w.ConfiguracaoProcesso.Processo.Etapas.Any(a => a.FiltrosDados.Any(f => f.FiltroDado == FiltroDadoProcesso)));

            return GetExpression();
        }
    }
}