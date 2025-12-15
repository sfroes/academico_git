using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORG.Specifications.AtoNormativo
{
    public class AtoNormativoFilterSpecification : SMCSpecification<Models.AtoNormativo>
    {
        public long? Seq { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqAssuntoNormativo { get; set; }

        public long? SeqTipoAtoNormativo { get; set; }

        public bool? Vigente { get; set; }

        public bool? AssuntoNormativoHabilitaEmissaoDocumentoConclusao { get; set; }

        public DateTime? DataDocumento { get; set; }

        public string NumeroDocumento { get; set; }

        public long? SeqEntidade { get; set; }

        public long? SeqTipoEntidade { get; set; }

        public string NomeEntidade { get; set; }

        public long? SeqAtoNormativoEntidade { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public List<long> Seqs { get; set; }

        public List<long> SeqsEntidades { get; set; }

        public override Expression<Func<Models.AtoNormativo, bool>> SatisfiedBy()
        {
            var dataAtual = DateTime.Now;

            AddExpression(Seq, p => p.Seq == Seq);
            AddExpression(Seqs, p => Seqs.Contains(p.Seq));
            AddExpression(SeqInstituicaoEnsino, p => p.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(SeqAssuntoNormativo, p => p.SeqAssuntoNormativo == SeqAssuntoNormativo);
            AddExpression(SeqTipoAtoNormativo, p => p.SeqTipoAtoNormativo == SeqTipoAtoNormativo);
            AddExpression(Vigente, p => p.DataPrazoValidade >= dataAtual && p.DataPublicacao <= dataAtual);
            AddExpression(AssuntoNormativoHabilitaEmissaoDocumentoConclusao, x => x.AssuntoNormativo.HabilitaEmissaoDocumentoConclusao == AssuntoNormativoHabilitaEmissaoDocumentoConclusao);
            AddExpression(NumeroDocumento, x => x.NumeroDocumento == NumeroDocumento);
            AddExpression(DataDocumento, x => x.DataDocumento == DataDocumento);
            AddExpression(SeqsEntidades, x => x.Entidades.Any(a => SeqsEntidades.Contains(a.SeqEntidade)));
            AddExpression(SeqTipoEntidade, x => x.Entidades.Any(a => a.Entidade.SeqTipoEntidade == SeqTipoEntidade));
            AddExpression(SeqAtoNormativoEntidade, x => x.Entidades.Select(a => a.SeqEntidade).ToList().Contains(SeqAtoNormativoEntidade.Value));
            AddExpression(NomeEntidade, x => x.Entidades.Select(a => a.Entidade.Nome).ToList().Any(a => a.ToLower().Contains(NomeEntidade.ToLower())));

            if (SeqEntidade.HasValue && SeqGrauAcademico.HasValue)
            {
                AddExpression(x => x.Entidades.Any(a => a.SeqGrauAcademico == SeqGrauAcademico && a.Entidade.Seq == SeqEntidade));
            }
            else
            {
                AddExpression(SeqEntidade, x => x.Entidades.Any(a => a.Entidade.Seq == SeqEntidade));
                AddExpression(SeqGrauAcademico, x => x.Entidades.Any(a => a.SeqGrauAcademico == SeqGrauAcademico));
            }

            return GetExpression();
        }
    }
}