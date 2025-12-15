using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class MensagemFilterSpecification : SMCSpecification<Mensagem>
    {
        public long SeqPessoaAtuacao { get; set; }

        public bool? MensagensValidas { get; set; }

        public CategoriaMensagem? CategoriaMensagem { get; set; }

        public TipoUsoMensagem? TipoUsoMensagem { get; set; }

        public override Expression<Func<Mensagem, bool>> SatisfiedBy()
        {
            AddExpression(x => x.Pessoas.Any(f => f.SeqPessoaAtuacao == SeqPessoaAtuacao));
            AddExpression(CategoriaMensagem, x => x.TipoMensagem.CategoriaMensagem == CategoriaMensagem);
            AddExpression(TipoUsoMensagem, x => x.TipoMensagem.TiposUso.Any(a => a.TipoUsoMensagem == TipoUsoMensagem.Value));

            if (MensagensValidas.GetValueOrDefault())
            {
                AddExpression(x => x.DataInicioVigencia <= DateTime.Now && (x.DataFimVigencia >= DateTime.Today || x.DataFimVigencia == null));
            }

            return GetExpression();
        }
    }
}
