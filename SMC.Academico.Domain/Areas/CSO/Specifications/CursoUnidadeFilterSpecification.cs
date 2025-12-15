using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoUnidadeFilterSpecification : SMCSpecification<CursoUnidade>
    {
        public long? SeqCurso { get; set; }

        /// <summary>
        /// Sequencial do HierarquiaEntidadeItem que represente a unidade
        /// </summary>
        public long? SeqUnidade { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqModalidade { get; set; }

        public int? CodigoOrgaoRegulador { get; set; }

        public List<long> SeqsNivelEnsino { get; set; }

        public override Expression<Func<CursoUnidade, bool>> SatisfiedBy()
        {
            AddExpression(SeqCurso, w => w.SeqCurso == this.SeqCurso);
            AddExpression(SeqUnidade, w => w.HierarquiasEntidades.Any(a => a.SeqItemSuperior == this.SeqUnidade));
            AddExpression(SeqLocalidade, w => w.CursosOfertaLocalidade.Any(a => a.HierarquiasEntidades.FirstOrDefault().ItemSuperior.SeqEntidade == this.SeqLocalidade));
            AddExpression(SeqModalidade, w => w.CursosOfertaLocalidade.Any(a => a.SeqModalidade == this.SeqModalidade));
            AddExpression(SeqCursoOferta, p => p.CursosOfertaLocalidade.Any(a => a.SeqCursoOferta == SeqCursoOferta));
            AddExpression(SeqsNivelEnsino, p => SeqsNivelEnsino.Contains(p.Curso.SeqNivelEnsino));
            AddExpression(CodigoOrgaoRegulador, p => p.CursosOfertaLocalidade.Any(a => a.CodigoOrgaoRegulador == CodigoOrgaoRegulador));

            return GetExpression();
        }
    }
}