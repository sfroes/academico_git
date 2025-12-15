using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class InstituicaoNivelTipoMensagemFilterSpecification : SMCSpecification<InstituicaoNivelTipoMensagem>
    {
        public long? Seq { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public long? SeqTipoMensagem { get; set; }

        public bool? PermiteCadastroManual { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public long? SeqMotivoBloqueio { get; set; }

        //Este não vai para o AddExpression
        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqInstituicaoNivel { get; set; }

        public override Expression<Func<InstituicaoNivelTipoMensagem, bool>> SatisfiedBy()
        {
            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(SeqMotivoBloqueio, w => w.SeqMotivoBloqueio == SeqMotivoBloqueio.Value);
            AddExpression(TipoAtuacao, w => w.TipoMensagem.TiposAtuacao.Any(x => x.TipoAtuacao == TipoAtuacao.Value));
            AddExpression(SeqTipoMensagem, w => w.SeqTipoMensagem == SeqTipoMensagem.Value);
            AddExpression(PermiteCadastroManual, w => w.TipoMensagem.PermiteCadastroManual == PermiteCadastroManual.Value);
            AddExpression(SeqNivelEnsino, a => a.InstituicaoNivel.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(SeqInstituicaoEnsino, a => a.InstituicaoNivel.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(SeqInstituicaoNivel, x => x.SeqInstituicaoNivel == SeqInstituicaoNivel.Value);

            return GetExpression();
        }
    }
}
