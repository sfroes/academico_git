using System;
using System.Linq;
using System.Linq.Expressions;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class EnvioNotificacaoDestinatarioFilterSpecification : SMCSpecification<EnvioNotificacaoDestinatario>
    {
        public long? Seq { get; set; }

        public long? SeqEnvioNotificacao { get; set; }

        public string Assunto { get; set; }

        public string Remetente { get; set; }

        public string UsuarioEnvio { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public DateTime? DataEnvio { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public override Expression<Func<EnvioNotificacaoDestinatario, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, x => x.Seq == this.Seq);
            AddExpression(this.SeqEnvioNotificacao, x => x.SeqEnvioNotificacao == this.SeqEnvioNotificacao);
            AddExpression(this.Assunto, x => x.EnvioNotificacao.Assunto.Contains(this.Assunto));
            AddExpression(this.Remetente, x => x.EnvioNotificacao.NomeOrigem.Contains(this.Remetente));
            AddExpression(this.UsuarioEnvio, x => x.UsuarioInclusao.Contains(this.UsuarioEnvio));
            AddExpression(this.TipoAtuacao, x => x.EnvioNotificacao.TipoAtuacao == this.TipoAtuacao);
            if (DataEnvio.HasValue && DataEnvio.Value != DateTime.MinValue)
                AddExpression(x => x.EnvioNotificacao.DataInclusao.Day == this.DataEnvio.Value.Day && x.EnvioNotificacao.DataInclusao.Month == this.DataEnvio.Value.Month && x.EnvioNotificacao.DataInclusao.Year == this.DataEnvio.Value.Year);
            AddExpression(SeqPessoaAtuacao, x => x.SeqPessoaAtuacao == this.SeqPessoaAtuacao.Value);          

            return GetExpression();
        }
    }
}
