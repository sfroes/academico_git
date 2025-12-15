using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CSO.Specifications
{
    public class CursoOfertaLocalidadeFilterSpecification : SMCSpecification<CursoOfertaLocalidade>
    {
        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqCurso { get; set; }

        public string NomeCurso { get; set; }

        public string DescricaoOferta { get; set; }

        public bool? CursoOfertaAtivo { get; set; }

        /// <summary>
        /// Sequenciais das hierarquias item que representão as entidades
        /// </summary>
        public List<long> SeqsEntidades { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsNiveisEnsino { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public long? SeqTipoFormacaoEspecifica { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqModalidade { get; set; }

        public long[] SeqsCursos { get; set; }

        public bool? Ativa { get; set; }

        public bool? AssociadaAtoNormativo { get; set; }

        public int? CodigoOrgaoRegulador { get; set; }

        public int? Codigo { get; set; }

        public override Expression<Func<CursoOfertaLocalidade, bool>> SatisfiedBy()
        {
            var agora = DateTime.Today;

            AddExpression(Seq, w => w.Seq == this.Seq);
            AddExpression(Seqs, w => this.Seqs.Contains(w.Seq));
            AddExpression(SeqCursoOferta, w => w.SeqCursoOferta == this.SeqCursoOferta);
            AddExpression(SeqCurso, w => w.CursoOferta.SeqCurso == this.SeqCurso);
            AddExpression(NomeCurso, w => w.CursoOferta.Curso.Nome.Contains(this.NomeCurso));
            AddExpression(DescricaoOferta, w => w.CursoOferta.Descricao.Contains(this.DescricaoOferta));
            AddExpression(CursoOfertaAtivo, w => w.CursoOferta.Ativo == this.CursoOfertaAtivo);

            AddExpression(SeqsEntidades, w => SeqsEntidades.Contains(w.CursoOferta.SeqCurso));

            AddExpression(SeqNivelEnsino, w => w.CursoOferta.Curso.SeqNivelEnsino == this.SeqNivelEnsino);
            AddExpression(SeqsNiveisEnsino, w => SeqsNiveisEnsino.Contains(w.CursoOferta.Curso.SeqNivelEnsino));

            AddExpression(SeqSituacaoAtual, w => w.HistoricoSituacoes.FirstOrDefault(h => agora >= h.DataInicio && (!h.DataFim.HasValue || agora <= h.DataFim.Value)).SituacaoEntidade.Seq == this.SeqSituacaoAtual);
            AddExpression(SeqTipoFormacaoEspecifica, w => w.CursoOferta.FormacaoEspecificaCursoOferta.SeqTipoFormacaoEspecifica == this.SeqTipoFormacaoEspecifica);
            AddExpression(SeqLocalidade, w => w.HierarquiasEntidades.Any(a => a.ItemSuperior.Entidade.Seq == this.SeqLocalidade));//HierarquiasEntidades?.FirstOrDefault()?.ItemSuperior?.Entidade
            AddExpression(SeqModalidade, w => w.SeqModalidade == this.SeqModalidade);
            AddExpression(SeqsCursos, w => SeqsCursos.Contains(w.CursoOferta.SeqCurso));
            AddExpression(Ativa, w => w
                .HistoricoSituacoes.FirstOrDefault(f => f.DataInicio <= DateTime.Today && (!f.DataFim.HasValue || f.DataFim >= DateTime.Today))
                .SituacaoEntidade.CategoriaAtividade == CategoriaAtividade.Ativa);

            if (AssociadaAtoNormativo.GetValueOrDefault())
                AddExpression(x => x.AtosNormativos.Any());

            AddExpression(CodigoOrgaoRegulador, w => w.CodigoOrgaoRegulador == this.CodigoOrgaoRegulador);
            AddExpression(Codigo, w => w.Codigo == this.Codigo);

            return GetExpression();
        }
    }
}