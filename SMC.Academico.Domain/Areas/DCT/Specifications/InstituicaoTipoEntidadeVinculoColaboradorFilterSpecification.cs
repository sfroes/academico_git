using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class InstituicaoTipoEntidadeVinculoColaboradorFilterSpecification : SMCSpecification<InstituicaoTipoEntidadeVinculoColaborador>
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoTipoEntidade { get; set; }

        public long? SeqTipoVinculoColaborador { get; set; }

        public long? SeqEntidadeInstituicao { get; set; }

        public long? SeqTipoEntidade { get; set; }

        public bool? PermiteInclusaoManualVinculo { get; set; }

        public bool? IntegraCorpoDocente { get; set; }

        public long[] SeqsTipoEntidade { get; set; }

        public bool? CriaVinculoInstitucional { get; set; }
        public List<long> SeqsEntidadeInstituicao { get; set; }

        public override Expression<Func<InstituicaoTipoEntidadeVinculoColaborador, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqInstituicaoTipoEntidade, x => x.SeqInstituicaoTipoEntidade == SeqInstituicaoTipoEntidade.Value);
            AddExpression(SeqTipoVinculoColaborador, x => x.SeqTipoVinculoColaborador == SeqTipoVinculoColaborador.Value);
            AddExpression(SeqEntidadeInstituicao, x => x.InstituicaoTipoEntidade.SeqInstituicaoEnsino == SeqEntidadeInstituicao.Value);
            AddExpression(SeqTipoEntidade, x => x.InstituicaoTipoEntidade.SeqTipoEntidade == SeqTipoEntidade.Value);
            AddExpression(PermiteInclusaoManualVinculo, x => x.TipoVinculoColaborador.PermiteInclusaoManualVinculo == PermiteInclusaoManualVinculo.Value);
            AddExpression(IntegraCorpoDocente, x => x.TipoVinculoColaborador.IntegraCorpoDocente == IntegraCorpoDocente.Value);
            AddExpression(SeqsTipoEntidade, x => SeqsTipoEntidade.Contains(x.InstituicaoTipoEntidade.SeqTipoEntidade));
            AddExpression(CriaVinculoInstitucional, x => x.TipoVinculoColaborador.CriaVinculoInstitucional == CriaVinculoInstitucional);
            AddExpression(SeqsEntidadeInstituicao, x => SeqsEntidadeInstituicao.Contains(x.InstituicaoTipoEntidade.SeqInstituicaoEnsino));

            return GetExpression();
        }
    }
}