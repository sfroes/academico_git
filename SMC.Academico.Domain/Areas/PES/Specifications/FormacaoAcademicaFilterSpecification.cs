using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class FormacaoAcademicaFilterSpecification : SMCSpecification<FormacaoAcademica>
    {
        public long? SeqPessoaAtuacao { get; set; }
        public long? SeqTitulacao { get; set; }
        public int? AnoInicio { get; set; }
        public int? AnoObtencaoTitulo { get; set; }
        public bool? TitulacaoMaxima { get; set; }

        public override Expression<Func<FormacaoAcademica, bool>> SatisfiedBy()
        {
            AddExpression(SeqPessoaAtuacao, x => x.SeqPessoaAtuacao == SeqPessoaAtuacao);
            AddExpression(SeqTitulacao, x => x.SeqTitulacao == SeqTitulacao);
            AddExpression(AnoInicio, x => x.AnoInicio == AnoInicio);
            AddExpression(AnoObtencaoTitulo, x => x.AnoObtencaoTitulo == AnoObtencaoTitulo);
            AddExpression(TitulacaoMaxima, x => x.TitulacaoMaxima == TitulacaoMaxima); 

            return GetExpression();
        }
    }
}
