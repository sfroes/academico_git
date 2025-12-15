using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class ClassificacaoInvalidadeDocumentoFilterSpecification : SMCSpecification<ClassificacaoInvalidadeDocumento>
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public TipoInvalidade? TipoInvalidade { get; set; }

        public bool? Ativo { get; set; }

        public string Token { get; set; }

        public override Expression<Func<ClassificacaoInvalidadeDocumento, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqInstituicaoEnsino, x => x.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(TipoInvalidade, x => x.TipoInvalidade == TipoInvalidade);
            AddExpression(Ativo, x => x.Ativo == Ativo);
            AddExpression(Token, x => x.Token == Token);

            return GetExpression();
        }
    }
}
