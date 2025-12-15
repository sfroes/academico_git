using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class MensagemPessoaAtuacaoFilterSpecification : SMCSpecification<MensagemPessoaAtuacao>
    {
        public long? SeqPessoaAtuacao { get; set; }
        public List<long> SeqsPessoaAtuacao { get; set; }
        public long? SeqTipoMensagem { get; set; }
        public string TokenTipoMensagem { get; set; }
        public bool? MensagensValidas { get; set; }
        public TipoUsoMensagem? TipoUsoMensagem { get; set; }
        public CategoriaMensagem? CategoriaMensagem { get; set; }

        public override Expression<Func<MensagemPessoaAtuacao, bool>> SatisfiedBy()
        {
            AddExpression(SeqPessoaAtuacao, w => w.Mensagem.Pessoas.Any(a => a.SeqPessoaAtuacao == SeqPessoaAtuacao));
            AddExpression(SeqTipoMensagem, w => w.Mensagem.SeqTipoMensagem == SeqTipoMensagem.Value);
            AddExpression(TokenTipoMensagem, w => w.Mensagem.TipoMensagem.Token == TokenTipoMensagem);
            AddExpression(TipoUsoMensagem, w => w.Mensagem.TipoMensagem.TiposUso.Any(a => a.TipoUsoMensagem == TipoUsoMensagem.Value));
            AddExpression(CategoriaMensagem, w => w.Mensagem.TipoMensagem.CategoriaMensagem == CategoriaMensagem);
            AddExpression(SeqsPessoaAtuacao, w => SeqsPessoaAtuacao.Contains(w.SeqPessoaAtuacao));

            if (MensagensValidas.GetValueOrDefault())
            {
                AddExpression(w => w.Mensagem.DataInicioVigencia <= DateTime.Now && (w.Mensagem.DataFimVigencia >= DateTime.Today || w.Mensagem.DataFimVigencia == null));
            }

            return GetExpression();
        }
    }
}
