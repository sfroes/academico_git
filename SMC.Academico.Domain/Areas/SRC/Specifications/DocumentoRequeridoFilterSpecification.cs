using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class DocumentoRequeridoFilterSpecification : SMCSpecification<DocumentoRequerido>
    {
        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public Sexo? Sexo { get; set; }

        public long? SeqTipoDocumento { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public long? SeqConfiguracaoEtapa { get; set; }

        public bool? Obrigatorio { get; set; }

        public bool? ObrigatorioUpload { get; set; }

        public bool? PermiteUploadArquivo { get; set; }

        public long? SeqConfiguracaoProcesso { get; set; }

        public string Token { get; set; }

        public long? SeqProcessoAtivo { get; set; }

        public bool? ProcessoAtivo { get; set; }

        public SituacaoEntregaDocumento? SituacaoEntregaDocumento { get; set; }

        public override Expression<Func<DocumentoRequerido, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(Seqs, a => Seqs.Contains(a.Seq));
            AddExpression(Sexo, w => !w.Sexo.HasValue || w.Sexo == DadosMestres.Common.Areas.PES.Enums.Sexo.Nenhum || w.Sexo == this.Sexo);
            AddExpression(SeqSolicitacaoServico, w => w.SolicitacoesDocumentoRequerido.Any(s => s.SeqSolicitacaoServico == this.SeqSolicitacaoServico));
            AddExpression(SeqTipoDocumento, w => w.SeqTipoDocumento == this.SeqTipoDocumento.Value);
            AddExpression(SeqProcessoEtapa, w => w.ConfiguracaoEtapa.SeqProcessoEtapa == this.SeqProcessoEtapa.Value);
            AddExpression(SeqConfiguracaoEtapa, w => w.SeqConfiguracaoEtapa == this.SeqConfiguracaoEtapa.Value);
            AddExpression(Obrigatorio, w => w.Obrigatorio == this.Obrigatorio.Value);
            AddExpression(ObrigatorioUpload, w => w.ObrigatorioUpload == this.ObrigatorioUpload.Value);
            AddExpression(PermiteUploadArquivo, w => w.PermiteUploadArquivo == this.PermiteUploadArquivo.Value);
            AddExpression(SeqConfiguracaoProcesso, w => w.ConfiguracaoEtapa.SeqConfiguracaoProcesso == this.SeqConfiguracaoProcesso);
            AddExpression(Token, w => w.ConfiguracaoEtapa.ConfiguracaoProcesso.Processo.Servico.Token == this.Token);
            AddExpression(SeqProcessoAtivo, w => w.ConfiguracaoEtapa.ConfiguracaoProcesso.Processo.Seq == this.SeqProcessoAtivo);
            AddExpression(SituacaoEntregaDocumento, w => w.SolicitacoesDocumentoRequerido.Any(y => y.SituacaoEntregaDocumento == this.SituacaoEntregaDocumento));

            return GetExpression();
        }
    }
}