using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class InstituicaoTipoEventoFilterSpecification : SMCSpecification<InstituicaoTipoEvento>
    {
        public long? SeqTipoAgenda { get; set; }

        public long? SeqTipoEventoAgd { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public bool? Ativo { get; set; }

        public AbrangenciaEvento? AbrangenciaEvento { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public string Token { get; set; }

        public override Expression<Func<InstituicaoTipoEvento, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqTipoAgenda, p => p.SeqTipoAgenda == this.SeqTipoAgenda.Value);
            AddExpression(this.SeqTipoEventoAgd, p => p.SeqTipoEventoAgd == this.SeqTipoEventoAgd.Value);
            AddExpression(this.SeqCicloLetivo, p => p.CiclosLetivosTipoEvento.Any(c => c.SeqCicloLetivo == this.SeqCicloLetivo.Value));
            AddExpression(this.SeqInstituicaoEnsino, p => p.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino.Value);
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo.Value);
            AddExpression(this.AbrangenciaEvento, p => p.AbrangenciaEvento == this.AbrangenciaEvento.Value);
            AddExpression(this.Token, p => p.Token == this.Token);

            return GetExpression();
        }
    }
}