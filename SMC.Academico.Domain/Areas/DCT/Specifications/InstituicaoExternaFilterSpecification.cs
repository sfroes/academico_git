using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class InstituicaoExternaFilterSpecification : SMCSpecification<InstituicaoExterna>
    {
        public bool? RetornarInstituicaoEnsinoLogada { get; set; }

        public bool ListarSomenteInstituicoesEnsino { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public string Sigla { get; set; }

        public string Nome { get; set; }

        public int? CodigoPais { get; set; }

        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        public long? SeqCategoriaInstituicaoEnsino { get; set; }

        public bool? Ativo { get; set; }

        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public override Expression<Func<InstituicaoExterna, bool>> SatisfiedBy()
        {
            // Caso seja informado o campo RetornarInstituicaoEnsinoLogada, utiliza o SeqInstituicaoEnsino para filtrar ou não a instiuição logada
            if (RetornarInstituicaoEnsinoLogada.HasValue)
            {
                AddExpression(RetornarInstituicaoEnsinoLogada, x => RetornarInstituicaoEnsinoLogada.Value || x.SeqInstituicaoEnsino != SeqInstituicaoEnsino);
            }
            // Caso contrário, filtra apenas a instiuição informada
            else
            {
                AddExpression(SeqInstituicaoEnsino, x => x.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            }

            if (ListarSomenteInstituicoesEnsino)
            {
                AddExpression(x => x.EhInstituicaoEnsino);
            }

            AddExpression(Sigla, x => x.Sigla.Contains(Sigla));
            AddExpression(Nome, x => x.Nome.Contains(Nome));
            AddExpression(CodigoPais, x => x.CodigoPais == CodigoPais);
            AddExpression(TipoInstituicaoEnsino, x => x.TipoInstituicaoEnsino == TipoInstituicaoEnsino);
            AddExpression(SeqCategoriaInstituicaoEnsino, x => x.SeqCategoriaInstituicaoEnsino == SeqCategoriaInstituicaoEnsino);
            AddExpression(Ativo, x => x.Ativo == Ativo);
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(Seqs, x => Seqs.Contains(x.Seq));

            return GetExpression();
        }
    }
}