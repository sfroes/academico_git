using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.CAM.Specifications
{
    public class ProcessoSeletivoFilterSpecification : SMCSpecification<ProcessoSeletivo>
    {
        public long? Seq { get; set; }

        public long? SeqCampanha { get; set; }

        public long? SeqTipoProcessoSeletivo { get; set; }

        public bool? IngressoDireto { get; set; }

        public string TokenTipoProcessoSeletivo { get; set; }

        public long[] SeqsProcessosSeletivos { get; set; }

        public long[] SeqsConvocacoes { get; set; }

        public string Descricao { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public override Expression<Func<ProcessoSeletivo, bool>> SatisfiedBy()
        {
            AddExpression(SeqCampanha, p => p.SeqCampanha == SeqCampanha);
            AddExpression(SeqTipoProcessoSeletivo, p => p.SeqTipoProcessoSeletivo == SeqTipoProcessoSeletivo);
            AddExpression(Descricao, p => p.Descricao.ToLower().Contains(Descricao.ToLower()));
            AddExpression(SeqNivelEnsino, p => p.NiveisEnsino.Any(f => f.SeqNivelEnsino == SeqNivelEnsino));
            AddExpression(IngressoDireto, p => p.TipoProcessoSeletivo.IngressoDireto == IngressoDireto);
            AddExpression(TokenTipoProcessoSeletivo, p => p.TipoProcessoSeletivo.Token == TokenTipoProcessoSeletivo);
            AddExpression(SeqsProcessosSeletivos, p => SeqsProcessosSeletivos.Contains(p.Seq));
            AddExpression(Seq, p => p.Seq == Seq);
            AddExpression(SeqsConvocacoes, p => p.Convocacoes.Any(x => SeqsConvocacoes.Contains(x.Seq)));


            return GetExpression();
        }
    }
}