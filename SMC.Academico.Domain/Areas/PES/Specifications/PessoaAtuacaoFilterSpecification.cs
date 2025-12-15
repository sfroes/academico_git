using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaAtuacaoFilterSpecification : SMCSpecification<PessoaAtuacao>
    {
        public long? Seq { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string NumeroPassaporte { get; set; }
        public TipoAtuacao? TipoAtuacao { get; set; }
        public long? SeqPessoa { get; set; }
        public string Descricao { get; set; }
        public PessoaAtuacaoFilterSpecification()
        {
            SetOrderBy(x => x.DadosPessoais.Nome);
        }
        public long? SeqUsuarioSAS { get; set; }

        public override Expression<Func<PessoaAtuacao, bool>> SatisfiedBy()
        {
            if (!string.IsNullOrEmpty(Cpf))
                Cpf = Cpf.SMCRemoveNonDigits();

            if (Seq == 0)
                Seq = null;

            if (SeqPessoa == 0)
                SeqPessoa = null;

            AddExpression(Seq, w => w.Seq == Seq.Value);
            AddExpression(TipoAtuacao, w => TipoAtuacao == w.TipoAtuacao);
            AddExpression(SeqPessoa, w => SeqPessoa == w.SeqPessoa);
            AddExpression(Nome, w => w.DadosPessoais.Nome.Contains(Nome) || w.DadosPessoais.NomeSocial.Contains(Nome));
            AddExpression(Cpf, w => w.Pessoa.Cpf.Equals(Cpf));
            AddExpression(Descricao, w => w.Descricao.Contains(Descricao));
            AddExpression(NumeroPassaporte, w => w.Pessoa.NumeroPassaporte.Equals(NumeroPassaporte));
            AddExpression(SeqUsuarioSAS, p => p.Pessoa.SeqUsuarioSAS == SeqUsuarioSAS);

            return GetExpression();
        }
    }
}