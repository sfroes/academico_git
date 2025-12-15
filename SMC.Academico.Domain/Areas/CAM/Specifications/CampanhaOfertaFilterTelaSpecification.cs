using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class CampanhaOfertaFilterTelaSpecification : SMCSpecification<CampanhaOferta>
    {
        public long? SeqCampanha { get; set; }

        public long? SeqTipoOferta { get; set; }

        public string Oferta { get; set; }

        public long[] SeqsCampanhaOfertas { get; set; }

        public override Expression<Func<CampanhaOferta, bool>> SatisfiedBy()
        {
            AddExpression(SeqCampanha, x => x.SeqCampanha == SeqCampanha);
            AddExpression(SeqTipoOferta, x => x.SeqTipoOferta == SeqTipoOferta);
            AddExpression(Oferta, x => x.Descricao.ToLower().Contains(Oferta.ToLower()));
            AddExpression(SeqsCampanhaOfertas, x => SeqsCampanhaOfertas.Contains(x.Seq));

            return GetExpression();
        }
    }
}
