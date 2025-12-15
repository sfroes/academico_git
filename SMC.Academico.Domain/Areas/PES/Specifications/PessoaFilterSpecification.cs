using SMC.Academico.Domain.Areas.PES.Models;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class PessoaFilterSpecification : SMCSpecification<Pessoa>
    {
        public TipoNacionalidade? TipoNacionalidade { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Cpf { get; set; }

        public string Nome { get; set; }

        public string NumeroPassaporte { get; set; }

        public bool? DadosPessoaisCadastrados { get; set; }

        public long? SeqUsuarioSAS { get; set; }

        public long? Seq { get; set; }

        public string NomeAtuacaoInicio { get; set; }

        public string[] NomesFiliacaoInicio { get; set; }

        public override Expression<Func<Pessoa, bool>> SatisfiedBy()
        {
            if (!string.IsNullOrEmpty(Cpf))
            {
                Cpf = Cpf.SMCRemoveNonDigits();
            }

            if(!string.IsNullOrEmpty(this.NumeroPassaporte))
                AddExpression(this.NumeroPassaporte, p => p.NumeroPassaporte.Contains(this.NumeroPassaporte));
            
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.TipoNacionalidade, p => p.TipoNacionalidade == this.TipoNacionalidade);
            AddExpression(this.DataNascimento, p => p.DataNascimento == this.DataNascimento);
            AddExpression(this.Cpf, p => p.Cpf == this.Cpf);
            AddExpression(this.Nome, p => p.DadosPessoais.OrderByDescending(o => o.Seq).FirstOrDefault().Nome.Contains(this.Nome)
                                       || p.DadosPessoais.OrderByDescending(o => o.Seq).FirstOrDefault().NomeSocial.Contains(this.Nome));
            AddExpression(this.SeqUsuarioSAS, p => p.SeqUsuarioSAS == this.SeqUsuarioSAS);
            AddExpression(this.DadosPessoaisCadastrados, p => p.DadosPessoais.Any() == this.DadosPessoaisCadastrados);
            AddExpression(this.NomeAtuacaoInicio, p => p.Atuacoes.Any(a => a.DadosPessoais.Nome.StartsWith(this.NomeAtuacaoInicio)
                                                                  || a.DadosPessoais.NomeSocial.StartsWith(this.NomeAtuacaoInicio)));
            AddExpression(this.NomesFiliacaoInicio, p => p.Filiacao.Any(a => NomesFiliacaoInicio.Any(an => a.Nome.StartsWith(an))));

            return GetExpression();
        }
    }
}