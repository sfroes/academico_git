using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.SRC.Specifications
{
    public class SolicitacaoDocumentoRequeridoFilterSpecification : SMCSpecification<SolicitacaoDocumentoRequerido>
    {
        public long? Seq { get; set; }
      
        public long? SeqSolicitacaoServico { get; set; }

        public long? SeqDocumentoRequerido { get; set; }
        public long? SeqTipoDocumento { get; set; }
        public long? SeqArquivoAnexado { get; set; }

        public long[] SeqsDocumentosRequeridos { get; set; }

        public SituacaoEntregaDocumento? SituacaoEntregaDocumento { get; set; }

        public override Expression<Func<SolicitacaoDocumentoRequerido, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == this.Seq);         
            AddExpression(SeqSolicitacaoServico, w => w.SeqSolicitacaoServico == this.SeqSolicitacaoServico);
            AddExpression(SeqDocumentoRequerido, w => w.SeqDocumentoRequerido == this.SeqDocumentoRequerido);
            AddExpression(SeqsDocumentosRequeridos, a => SeqsDocumentosRequeridos.Contains(a.SeqDocumentoRequerido));
            AddExpression(SituacaoEntregaDocumento, a => a.SituacaoEntregaDocumento == this.SituacaoEntregaDocumento);
            AddExpression(SeqTipoDocumento, w => w.DocumentoRequerido.SeqTipoDocumento == this.SeqTipoDocumento);
            AddExpression(SeqArquivoAnexado, w => w.SeqArquivoAnexado == this.SeqArquivoAnexado);

            return GetExpression();
        }
    }
}