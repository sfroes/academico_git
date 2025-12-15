using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeFilterSpecification : SMCSpecification<Entidade>
    {
        public bool? Externada { get; set; }

        public long? Seq { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqTipoEntidade { get; set; }

        public long[] SeqsTipoEntidade { get; set; }

        public string Nome { get; set; }

        public string NomeReduzido { get; set; }

        public string Sigla { get; set; }
        
        public string Token { get; set; }

        public bool? ApenasAtivos { get; set; }

        public bool? PossuiSeqUnidadeResponsavelNotificacao { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public bool? PermiteAtoNormativo { get; set; }

        public int? CodigoUnidadeSeo { get; set; }

        public override Expression<Func<Entidade, bool>> SatisfiedBy()
        {
            DateTime agora = DateTime.Now;

            if (Seqs == null)
                Seqs = new long[] { };

            if (this.CodigoUnidadeSeo == 0)
                this.CodigoUnidadeSeo = null;

            AddExpression(this.Externada, p => p.TipoEntidade.EntidadeExternada == this.Externada.Value);
            AddExpression(this.Seq, p => this.Seq.Value == default(long) || this.Seq.Value == p.Seq);
            AddExpression(this.Seqs, p => Seqs.Contains(p.Seq));
            AddExpression(this.SeqTipoEntidade, p => this.SeqTipoEntidade.Value == p.SeqTipoEntidade);
            AddExpression(this.SeqsTipoEntidade, p => SeqsTipoEntidade.Contains(p.SeqTipoEntidade));
            AddExpression(this.Nome, p => p.Nome.Contains(this.Nome));
            AddExpression(this.NomeReduzido, p => p.NomeReduzido.Equals(this.NomeReduzido));
            AddExpression(this.Sigla, p => p.Sigla.Equals(this.Sigla));
            AddExpression(this.Token, p => p.TipoEntidade.Token.Equals(this.Token));
            AddExpression(this.ApenasAtivos, p => !this.ApenasAtivos.Value
                                                  || (this.ApenasAtivos.Value && p.HistoricoSituacoes.Any(h => agora >= h.DataInicio &&
                                                                                                                  (!h.DataFim.HasValue || agora <= h.DataFim.Value) &&
                                                                                                                  h.SituacaoEntidade.CategoriaAtividade != CategoriaAtividade.Inativa)));
            AddExpression(this.PossuiSeqUnidadeResponsavelNotificacao, p => p.SeqUnidadeResponsavelNotificacao > 0);
            AddExpression(this.SeqInstituicaoEnsino, a => a.SeqInstituicaoEnsino == this.SeqInstituicaoEnsino);
            AddExpression(this.PermiteAtoNormativo, a => a.TipoEntidade.PermiteAtoNormativo == this.PermiteAtoNormativo);
            AddExpression(this.CodigoUnidadeSeo, a => a.CodigoUnidadeSeo == this.CodigoUnidadeSeo);

            return GetExpression();
        }
    }
}