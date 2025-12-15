using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaAtuacaoDocumentoFilterSpecification : SMCSpecification<PessoaAtuacaoDocumento>
    {
        public long? SeqPessoaAtuacao { get; set; }
        public long? SeqTipoDocumento { get; set; }
        public long[] ListaSeqsTipoDocumento { get; set; }
        public long? SeqSolicitacaoDocumentoRequerido { get; set; }
        public long? SeqSolicitacaoServico { get; set; }
        public string TokenTipoServico { get; set; }
        public bool? EntreguePorSolicitacao { get; set; }

        public override Expression<Func<PessoaAtuacaoDocumento, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqPessoaAtuacao, w => w.SeqPessoaAtuacao == this.SeqPessoaAtuacao);
            AddExpression(this.SeqTipoDocumento, w => w.SeqTipoDocumento == this.SeqTipoDocumento);
            AddExpression(this.SeqSolicitacaoDocumentoRequerido, w => w.SeqSolicitacaoDocumentoRequerido == this.SeqSolicitacaoDocumentoRequerido);
            AddExpression(this.ListaSeqsTipoDocumento, p => this.ListaSeqsTipoDocumento.Contains(p.SeqTipoDocumento));
            AddExpression(this.TokenTipoServico, p => p.SolicitacaoDocumentoRequerido.DocumentoRequerido.ConfiguracaoEtapa.ProcessoEtapa.Processo.Servico.Token == TokenTipoServico);
            if (EntreguePorSolicitacao.HasValue && EntreguePorSolicitacao.Value)
            {
                AddExpression(x => !string.IsNullOrEmpty(x.SolicitacaoDocumentoRequerido.SolicitacaoServico.NumeroProtocolo));        
            }
            if (EntreguePorSolicitacao.HasValue && !EntreguePorSolicitacao.Value)
            {
                AddExpression(x => string.IsNullOrEmpty(x.SolicitacaoDocumentoRequerido.SolicitacaoServico.NumeroProtocolo));
            }

            return GetExpression();
        }
    }
}