using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class InstituicaoNivelTipoFormacaoEspecificaFilterSpecification : SMCSpecification<InstituicaoNivelTipoFormacaoEspecifica>
    {
        public bool? Ativo { get; set; } = true;

        public List<long> SeqNivelEnsino { get; set; }

        public TipoCurso? TipoCurso { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public bool? PermiteEmitirDocumentoConclusao { get; set; }

        public override Expression<Func<InstituicaoNivelTipoFormacaoEspecifica, bool>> SatisfiedBy()
        { 
            AddExpression(SeqInstituicaoEnsino, w => w.InstituicaoNivel.SeqInstituicaoEnsino == SeqInstituicaoEnsino.Value);
            AddExpression(SeqNivelEnsino, w => SeqNivelEnsino.Contains(w.InstituicaoNivel.SeqNivelEnsino));
            AddExpression(Ativo, w => w.TipoFormacaoEspecifica.Ativo == Ativo.Value);
            AddExpression(TipoCurso, w => w.TipoFormacaoEspecifica.TiposCurso.Any(t => t.TipoCurso == TipoCurso.Value));
            AddExpression(SeqInstituicaoNivel, w => w.SeqInstituicaoNivel == SeqInstituicaoNivel.Value);
            AddExpression(PermiteEmitirDocumentoConclusao, w => w.TipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao == PermiteEmitirDocumentoConclusao.Value);

            return GetExpression();
        }
    }
}