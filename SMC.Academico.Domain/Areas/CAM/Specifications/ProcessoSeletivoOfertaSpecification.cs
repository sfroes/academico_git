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
    public class ProcessoSeletivoOfertaSpecification : SMCSpecification<ProcessoSeletivoOferta>
    {
        public long? SeqProcessoSeletivo { get; set; }

        public long? SeqTipoOferta { get; set; }

        public string Oferta { get; set; }

        public long[] Seqs { get; set; }

        public override Expression<Func<ProcessoSeletivoOferta, bool>> SatisfiedBy()
        {
            AddExpression(SeqProcessoSeletivo, x => x.SeqProcessoSeletivo == SeqProcessoSeletivo);
            AddExpression(SeqTipoOferta, x => x.CampanhaOferta.SeqTipoOferta == SeqTipoOferta);
            AddExpression(Oferta, x => x.CampanhaOferta.Descricao.ToLower().Contains(Oferta.ToLower()));
            AddExpression(Seqs, x => Seqs.Contains(x.Seq));

            return GetExpression();
        }
    }
}
