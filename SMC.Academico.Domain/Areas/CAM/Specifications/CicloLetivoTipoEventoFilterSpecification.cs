using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CicloLetivoTipoEventoFilterSpecification : SMCSpecification<CicloLetivoTipoEvento>
    {
        public long? Seq { get; set; }
        public long? SeqInstituicaoEnsino { get; set; }
        public long? SeqCicloLetivo { get; set; }
        public long? SeqTipoAgenda { get; set; }
        public long? SeqTipoEventoAgd { get; set; }
        public TipoParametroEvento? TipoParametroEvento { get; set; }

        public override Expression<Func<CicloLetivoTipoEvento, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq.Value);
            AddExpression(this.SeqInstituicaoEnsino, p => p.InstituicaoTipoEvento.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino.Value);
            AddExpression(this.SeqCicloLetivo, p => p.SeqCicloLetivo == this.SeqCicloLetivo.Value);
            AddExpression(this.SeqTipoAgenda, p => p.InstituicaoTipoEvento.SeqTipoAgenda == this.SeqTipoAgenda.Value);
            AddExpression(this.SeqTipoEventoAgd, p => p.InstituicaoTipoEvento.SeqTipoEventoAgd == this.SeqTipoEventoAgd.Value);
            AddExpression(this.TipoParametroEvento, p => p.Parametros.Any(x => x.InstituicaoTipoEventoParametro.TipoParametroEvento == this.TipoParametroEvento.Value));

            return GetExpression();
        }
    }
}