using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.CNC.Specifications
{
    public class TipoDocumentoAcademicoFilterSpecification : SMCSpecification<TipoDocumentoAcademico>
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public string Token { get; set; }

        public List<string> Tokens { get; set; }

        public List<GrupoDocumentoAcademico> GruposDocumentoAcademico { get; set; }

        public override Expression<Func<TipoDocumentoAcademico, bool>> SatisfiedBy()
        {         
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(Token, x => x.Token == Token);
            AddExpression(Tokens, x => Tokens.Contains(x.Token));
            if(SeqInstituicaoEnsino.HasValue)
                AddExpression(SeqInstituicaoEnsino, x => x.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(GruposDocumentoAcademico, w => GruposDocumentoAcademico.Contains(w.GrupoDocumentoAcademico));

            return GetExpression();
        }
    }
}
