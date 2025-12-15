using System;
using System.Linq;
using System.Linq.Expressions;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class EnvioNotificacaoFilterSpecification : SMCSpecification<EnvioNotificacao>
    {
        public long? Seq { get; set; }
        public string Assunto { get; set; }
        public string Remetente { get; set; }
        public string UsuarioEnvio { get; set; }
        public TipoAtuacao? TipoAtuacao { get; set; }
        public DateTime? DataEnvio { get; set; }
        public long? SeqPessoaAtuacao { get; set; }

        public override Expression<Func<EnvioNotificacao, bool>> SatisfiedBy()
        {
            AddExpression(Remetente, x => x.NomeOrigem.Contains(Remetente));
            AddExpression(UsuarioEnvio, x => x.UsuarioInclusao.Contains(UsuarioEnvio));
            AddExpression(TipoAtuacao, x => x.TipoAtuacao == this.TipoAtuacao);
            AddExpression(Assunto, x => x.Assunto.Contains(Assunto));
            if (Seq.HasValue && Seq.Value > 0)
            {
                AddExpression(this.Seq, p => p.Seq == Seq);
            }
            if (DataEnvio.HasValue && DataEnvio.Value != DateTime.MinValue)
                AddExpression(DataEnvio, x => x.DataInclusao.Day == this.DataEnvio.Value.Day && x.DataInclusao.Month == this.DataEnvio.Value.Month && x.DataInclusao.Year == this.DataEnvio.Value.Year);

            if (SeqPessoaAtuacao.HasValue && SeqPessoaAtuacao > 0)            
                AddExpression(SeqPessoaAtuacao, x => x.Destinatarios.Any(en => en.SeqPessoaAtuacao == SeqPessoaAtuacao.Value));
            

            return GetExpression();
        }
    }
}
