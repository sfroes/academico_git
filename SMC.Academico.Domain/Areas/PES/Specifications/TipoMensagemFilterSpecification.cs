using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class TipoMensagemFilterSpecification : SMCSpecification<TipoMensagem>
    {
        public string Descricao { get; set; }

        public CategoriaMensagem CategoriaMensagem { get; set; }

        public bool? PermiteCadastroManual { get; set; }

        public string Token { get; set; }

        public override Expression<Func<TipoMensagem, bool>> SatisfiedBy()
        {
            AddExpression(PermiteCadastroManual, w => w.PermiteCadastroManual == PermiteCadastroManual);
            AddExpression(w => w.CategoriaMensagem == CategoriaMensagem || CategoriaMensagem == Common.Areas.PES.Enums.CategoriaMensagem.Nenhum);
            AddExpression(Descricao, w => w.Descricao.Contains(Descricao));
            AddExpression(Token, w => w.Token == Token);

            return GetExpression();
        }
    }
}
