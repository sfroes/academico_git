using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.APR.Specifications
{
	public class MaterialFilterSpecification : SMCSpecification<Material>
	{
		public long? Seq { get; set; }

		public long? SeqOrigemMaterial { get; set; }

		public TipoAtuacao? TipoAtuacao { get; set; }

		public TipoOrigemMaterial? TipoOrigemMaterial { get; set; }

		public string DescricaoExata { get; set; }

		public long? SeqSuperior { get; set; }

		//Este não vai para o AddExpression
		public long? SeqOrigem { get; set; }

		//Este não vai para o AddExpression
		public string DescricaoOrigem { get; set; }

		public long? SeqNivelEnsino { get; set; }

		public List<long> SeqsNiveisEnsino { get; set; }

		public override Expression<Func<Material, bool>> SatisfiedBy()
		{
			AddExpression(SeqOrigemMaterial, w => w.SeqOrigemMaterial == SeqOrigemMaterial);
			AddExpression(Seq, a => a.Seq == Seq.Value);
			AddExpression(TipoOrigemMaterial, w => w.OrigemMaterial.TipoOrigemMaterial == TipoOrigemMaterial.Value);
			AddExpression(DescricaoExata, a => a.Descricao == DescricaoExata);
			AddExpression(SeqSuperior, a => a.SeqSuperior == SeqSuperior.Value);

			AddExpression(TipoAtuacao, x => !x.Visualizacoes.Any() || x.Visualizacoes.Any(v => v.TipoAtuacao == TipoAtuacao));
			AddExpression(SeqNivelEnsino, x => !x.Visualizacoes.Any() || x.Visualizacoes.Any(v => v.SeqNivelEnsino == SeqNivelEnsino));
			AddExpression(SeqsNiveisEnsino, x => !x.Visualizacoes.Any() || x.Visualizacoes.Any(v => SeqsNiveisEnsino.Contains(v.SeqNivelEnsino)));
			return GetExpression();
		}
	}
}