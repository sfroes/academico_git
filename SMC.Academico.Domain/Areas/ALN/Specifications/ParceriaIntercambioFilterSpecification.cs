using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class ParceriaIntercambioFilterSpecification : SMCSpecification<ParceriaIntercambio>
    {
        public long? Seq { get; set; }

        public string Descricao { get; set; }

        public TipoParceriaIntercambio? TipoParceriaIntercambio { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public List<long> SeqsTiposTermosIntercambio { get; set; }

        public long? SeqInstituicaoExterna { get; set; }

        public bool? ProcessoNegociacao { get; set; }

        public long? SeqParceriaIntercambioInstituicaoExterna { get; internal set; }

        public override Expression<Func<ParceriaIntercambio, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => Seq == w.Seq);
            AddExpression(Descricao, w => w.Descricao.Contains(Descricao));
            AddExpression(TipoParceriaIntercambio, w => TipoParceriaIntercambio == w.TipoParceriaIntercambio);
            AddExpression(SeqTipoTermoIntercambio, w => w.TiposTermo.Any(t => t.SeqTipoTermoIntercambio == SeqTipoTermoIntercambio));
            AddExpression(SeqsTiposTermosIntercambio, w => w.TiposTermo.Any(t => SeqsTiposTermosIntercambio.Contains(t.SeqTipoTermoIntercambio)));
            AddExpression(SeqInstituicaoExterna, w => w.InstituicoesExternas.Any(i => i.SeqInstituicaoExterna == SeqInstituicaoExterna));
            AddExpression(ProcessoNegociacao, w => ProcessoNegociacao == w.ProcessoNegociacao);
            AddExpression(SeqParceriaIntercambioInstituicaoExterna, w => w.InstituicoesExternas.Any(i => i.Seq == SeqParceriaIntercambioInstituicaoExterna.Value));

            return GetExpression();
        }
    }
}