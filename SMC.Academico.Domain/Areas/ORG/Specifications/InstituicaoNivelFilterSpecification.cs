using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class InstituicaoNivelFilterSpecification : SMCSpecification<InstituicaoNivel>
    {
        public long? Seq { get; set; }
        public long? SeqInstituicaoEnsino { get; set; }
        public long? SeqNivelEnsino { get; set; }
        public bool? ReconhecidoLDB { get; set; }
        public long? SeqCicloLetivo { get; set; }
        public TipoOrgaoRegulador? TipoOrgaoRegulador { get; set; }

        public override Expression<Func<InstituicaoNivel, bool>> SatisfiedBy()
        {
            AddExpression(Seq, p => p.Seq == Seq);
            AddExpression(SeqInstituicaoEnsino, p => p.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(SeqNivelEnsino, p => p.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(ReconhecidoLDB, p => p.NivelEnsino.ReconhecidoLDB == ReconhecidoLDB);
            AddExpression(TipoOrgaoRegulador, p => p.TipoOrgaoRegulador == TipoOrgaoRegulador);
            AddExpression(SeqCicloLetivo, p => p.RegimesLetivos.Any(r => r.RegimeLetivo.CiclosLetivos.Any(cl => cl.Seq == SeqCicloLetivo)));

            return GetExpression();
        }
    }
}