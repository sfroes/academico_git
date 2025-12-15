using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaAtuacaoBloqueioFilterSpecification : SMCSpecification<PessoaAtuacaoBloqueio>
    {
        public List<long> SeqTipoBloqueio { get; set; }

        public List<long> SeqMotivoBloqueio { get; set; }

        public SituacaoBloqueio? SituacaoBloqueio { get; set; }

        public TipoDesbloqueio? TipoDesbloqueio { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public long? SeqPessoa { get; set; }

        public DateTime? DataBloqueioMaiorQue { get; set; }

        public DateTime? DataBloqueioMenorOuIgualA { get; set; }

        public DateTime? DataBloqueioInicio { get; set; }

        public DateTime? DataBloqueioFim { get; set; }

        public string TokenMotivoBloqueio { get; set; }

        public bool? BloqueadoOuDesbloqueadoTemporariamente { get; set; }

        public bool? BloqueioMatricula { get; set; }

        public string CodigoIntegracaoSistemaOrigem { get; set; }

        public List<string> SeqsTiposDocumentosBloqueio { get; set; }

        public PessoaAtuacaoBloqueioFilterSpecification()
        {
            SetOrderBy("PessoaAtuacao.DadosPessoais.Nome");
            SetOrderBy("PessoaAtuacao.TipoAtuacao");
            SetOrderBy("MotivoBloqueio.Descricao");
            SetOrderBy("DataInclusao");
        }

        public override Expression<Func<PessoaAtuacaoBloqueio, bool>> SatisfiedBy()
        {
            SeqMotivoBloqueio = SeqMotivoBloqueio ?? new List<long>();
            SeqTipoBloqueio = SeqTipoBloqueio ?? new List<long>();

            ///Pois tem data e hora assim acrescentamos o dia para zerar a hora
            if (DataBloqueioFim.HasValue)
            {
                DataBloqueioFim = DataBloqueioFim.Value.AddDays(1);
            }

            AddExpression(SeqTipoBloqueio, p => SeqTipoBloqueio.Any(x => x == p.MotivoBloqueio.SeqTipoBloqueio));
            AddExpression(SeqMotivoBloqueio, p => SeqMotivoBloqueio.Any(x => x == p.SeqMotivoBloqueio));
            AddExpression(SituacaoBloqueio, p => p.SituacaoBloqueio == SituacaoBloqueio);
            AddExpression(SeqPessoa, p => p.PessoaAtuacao.SeqPessoa == SeqPessoa);
            AddExpression(SeqSolicitacaoServico, p => p.SolicitacaoServico.Seq == SeqSolicitacaoServico);
            AddExpression(TipoDesbloqueio, p => p.TipoDesbloqueio == TipoDesbloqueio);
            AddExpression(SeqPessoaAtuacao, p => p.SeqPessoaAtuacao == SeqPessoaAtuacao);
            AddExpression(DataBloqueioMaiorQue, p => p.DataBloqueio > DataBloqueioMaiorQue.Value);
            AddExpression(DataBloqueioMenorOuIgualA, p => p.DataBloqueio <= DataBloqueioMenorOuIgualA.Value);
            AddExpression(DataBloqueioInicio, p => p.DataBloqueio >= DataBloqueioInicio.Value);
            AddExpression(DataBloqueioFim, p => p.DataBloqueio < DataBloqueioFim.Value);
            //AddExpression(() => DataBloqueioInicio.HasValue && DataBloqueioFim.HasValue, p => p.PessoaAtuacao.Bloqueios.Any(a => (a.DataBloqueio >= DataBloqueioInicio.Value || !DataBloqueioInicio.HasValue) && (a.DataBloqueio < DataBloqueioFim.Value || !DataBloqueioFim.HasValue)));
            AddExpression(TokenMotivoBloqueio, p => p.MotivoBloqueio.Token == TokenMotivoBloqueio);
            AddExpression(BloqueadoOuDesbloqueadoTemporariamente, p => p.SituacaoBloqueio == Common.Areas.PES.Enums.SituacaoBloqueio.Bloqueado || (p.SituacaoBloqueio == Common.Areas.PES.Enums.SituacaoBloqueio.Desbloqueado && p.TipoDesbloqueio == Common.Areas.PES.Enums.TipoDesbloqueio.Temporario));
            AddExpression(BloqueioMatricula, p => p.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.PARCELA_MATRICULA_PENDENTE || p.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.PARCELA_PRE_MATRICULA_PENDENTE);
            AddExpression(CodigoIntegracaoSistemaOrigem, p => p.Itens.Any(a => a.CodigoIntegracaoSistemaOrigem == CodigoIntegracaoSistemaOrigem));
            AddExpression(SeqsTiposDocumentosBloqueio, p => p.Itens.Any(a => SeqsTiposDocumentosBloqueio.Contains(a.CodigoIntegracaoSistemaOrigem)));

            return GetExpression();

            //return p => (!considerarTipos || SeqTipoBloqueio.Any(x => x == p.MotivoBloqueio.SeqTipoBloqueio)) &&
            //            (!considerarMotivos || SeqMotivoBloqueio.Any(x => x == p.SeqMotivoBloqueio)) &&
            //            (!SituacaoBloqueio.HasValue || p.SituacaoBloqueio == SituacaoBloqueio.Value) &&
            //            (!SeqPessoa.HasValue || p.PessoaAtuacao.SeqPessoa == SeqPessoa.Value) &&
            //            (!SeqSolicitacaoServico.HasValue || p.SolicitacaoServico.Seq == SeqSolicitacaoServico.Value) &&
            //            (!TipoDesbloqueio.HasValue || p.TipoDesbloqueio.Value == TipoDesbloqueio.Value) &&
            //            (!SeqPessoaAtuacao.HasValue || p.SeqPessoaAtuacao == SeqPessoaAtuacao.Value) &&
            //            (!DataBloqueioMaiorQue.HasValue || p.DataBloqueio > DataBloqueioMaiorQue.Value) &&
            //            (!DataBloqueioMenorOuIgualA.HasValue || p.DataBloqueio <= DataBloqueioMenorOuIgualA.Value) &&
            //            (!DataBloqueioInicio.HasValue && !DataBloqueioFim.HasValue || p.PessoaAtuacao.Bloqueios.Any(a => (a.DataBloqueio >= DataBloqueioInicio.Value || !DataBloqueioInicio.HasValue) && (a.DataBloqueio < DataBloqueioFim.Value || !DataBloqueioFim.HasValue)));
        }
    }
}