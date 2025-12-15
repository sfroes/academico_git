using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaAtuacaoEnderecoFilterSpecification : SMCSpecification<PessoaAtuacaoEndereco>
    {
        public long[] Seqs { get; set; }

        public long? SeqPessoa { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }

        public bool? EnderecoCorrespondencia { get; set; }

        public override Expression<Func<PessoaAtuacaoEndereco, bool>> SatisfiedBy()
        {
            AddExpression(Seqs, w => Seqs.Contains(w.Seq));
            AddExpression(SeqPessoa, w => w.PessoaAtuacao.SeqPessoa == this.SeqPessoa);
            AddExpression(SeqPessoaAtuacao, w => w.SeqPessoaAtuacao == this.SeqPessoaAtuacao);
            AddExpression(TipoAtuacao, w => w.PessoaAtuacao.TipoAtuacao == this.TipoAtuacao);
            AddExpression(EnderecoCorrespondencia, w => w.EnderecoCorrespondencia != Common.Areas.PES.Enums.EnderecoCorrespondencia.Nao && w.EnderecoCorrespondencia != Common.Areas.PES.Enums.EnderecoCorrespondencia.Nenhum);

            return GetExpression();
        }
    }
}