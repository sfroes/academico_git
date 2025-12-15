using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.Specifications
{
    public class EntidadeTokenTipoSpecification : SMCSpecification<Entidade>
    {
        public string Token { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }
                
        public CategoriaAtividade[] CategoriaAtividadeIguais { get; set; }

        public override System.Linq.Expressions.Expression<Func<Entidade, bool>> SatisfiedBy()
        {
            DateTime dataAtual = DateTime.Today;
            
            AddExpression(SeqInstituicaoEnsino, x => x.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(Token, x => x.TipoEntidade.Token == Token);
            AddExpression(CategoriaAtividadeIguais, x => x.HistoricoSituacoes.FirstOrDefault(p => dataAtual >= p.DataInicio && (!p.DataFim.HasValue || dataAtual <= p.DataFim.Value)).SituacaoEntidade == null 
            || CategoriaAtividadeIguais.Contains(x.HistoricoSituacoes.FirstOrDefault(p => dataAtual >= p.DataInicio && (!p.DataFim.HasValue || dataAtual <= p.DataFim.Value)).SituacaoEntidade.CategoriaAtividade));

            return GetExpression();
        }
    }
}
