using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class SolicitacaoDocumentoConclusaoEntregaDigitalFilterSpecification : SMCSpecification<SolicitacaoDocumentoConclusaoEntregaDigital>
    {
        public long? SeqSolicitacaoDocumentoConclusao { get; set; }

        public List<TipoArquivoDigital> TiposArquivoDigital { get; set; }

        public override Expression<Func<SolicitacaoDocumentoConclusaoEntregaDigital, bool>> SatisfiedBy()
        {
            AddExpression(SeqSolicitacaoDocumentoConclusao, x => x.SeqSolicitacaoDocumentoConclusao == SeqSolicitacaoDocumentoConclusao);
            AddExpression(TiposArquivoDigital, x => TiposArquivoDigital.Contains(x.TipoArquivoDigital));

            return GetExpression();
        }
    }
}
