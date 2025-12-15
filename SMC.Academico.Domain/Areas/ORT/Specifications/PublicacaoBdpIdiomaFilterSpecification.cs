using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
	public class PublicacaoBdpIdiomaFilterSpecification : SMCSpecification<PublicacaoBdpIdioma>
	{

        public long? Seq {get; set; }
        public long? SeqTrabalhoAcademico { get; set; }

		public override Expression<Func<PublicacaoBdpIdioma, bool>> SatisfiedBy()
		{

            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqTrabalhoAcademico, x => x.PublicacaoBdp.SeqTrabalhoAcademico == SeqTrabalhoAcademico);
            
            return GetExpression();
		}
	}
}