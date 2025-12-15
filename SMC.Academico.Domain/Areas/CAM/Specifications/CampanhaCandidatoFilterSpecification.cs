using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaCandidatoFilterSpecification : SMCSpecification<Campanha>
    {
        public long? SeqCampanha { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqConvocacao { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long? SeqChamada { get; set; }

        public TipoChamada? TipoChamada { get; set; }

        public string OfertaCampanha { get; set; }

        public override Expression<Func<Campanha, bool>> SatisfiedBy()
        {
            AddExpression(this.SeqCampanha, p => p.Seq == this.SeqCampanha.Value);
            AddExpression(this.SeqTipoProcessoSeletivo, p => p.ProcessosSeletivos.Any(a => a.SeqTipoProcessoSeletivo == this.SeqTipoProcessoSeletivo.Value));
            AddExpression(this.SeqProcessoSeletivo, p => p.ProcessosSeletivos.Any(a => a.Seq == this.SeqProcessoSeletivo.Value));
            AddExpression(this.SeqConvocacao, p => p.ProcessosSeletivos.Any(a => a.Convocacoes.Any(co => co.Seq == this.SeqConvocacao.Value)));
            AddExpression(this.SeqCicloLetivo, p => p.CiclosLetivos.Any(cl => cl.SeqCicloLetivo == this.SeqCicloLetivo.Value));
            AddExpression(this.SeqChamada, p => p.ProcessosSeletivos.Any(a => a.Convocacoes.Any(co => co.Chamadas.Any(ch => ch.Seq == this.SeqChamada.Value))));
            AddExpression(this.TipoChamada, p => p.ProcessosSeletivos.Any(a => a.Convocacoes.Any(co => co.Chamadas.Any(ch => ch.TipoChamada == this.TipoChamada))));
            AddExpression(this.OfertaCampanha, p => p.Ofertas.Any(a => a.Descricao.Contains(this.OfertaCampanha)));

            return GetExpression();
        }
    }
}