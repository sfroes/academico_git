using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.PES.Specifications
{
    public class FuncionarioFilterSpecification : SMCSpecification<Funcionario>
    {
        public FuncionarioFilterSpecification()
        {
            SetOrderBy(o => o.DadosPessoais.Nome);
        }

        public long? Seq { get; set; }

        public List<long> SeqFuncionarios { get; set; }
        /// <summary>
        /// Filtra por instituição de ensino no portal
        /// </summary>
        public long? SeqInstituicaoEnsino { get; set; }
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string Cpf { get; set; }
        public string NumeroPassaporte { get; set; }
        public long? SeqTipoFuncionario { get; set; }
        public long? SeqEntidade { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string Token { get; set; }
        public DateTime? Data { get; set; }
        public override Expression<Func<Funcionario, bool>> SatisfiedBy()
        {
            if (!string.IsNullOrEmpty(Cpf))
            {
                Cpf = Cpf.SMCRemoveNonDigits();
            }
            AddExpression(Seq, x => x.Seq == Seq);
            AddExpression(SeqFuncionarios, x => SeqFuncionarios.Contains(x.Seq));
            AddExpression(SeqInstituicaoEnsino, x => x.Pessoa.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(Nome, x => x.DadosPessoais.Nome.Contains(Nome) || x.DadosPessoais.NomeSocial.Contains(Nome));
            AddExpression(NomeSocial, x => x.DadosPessoais.Nome.Contains(NomeSocial) || x.DadosPessoais.NomeSocial.Contains(NomeSocial));
            AddExpression(Cpf, x => x.Pessoa.Cpf == Cpf);
            AddExpression(NumeroPassaporte, x => x.Pessoa.NumeroPassaporte.Contains(NumeroPassaporte));
            AddExpression(SeqTipoFuncionario, x => x.Vinculos.Any(a => a.SeqTipoFuncionario == SeqTipoFuncionario));
            AddExpression(DataInicio, x => x.Vinculos.Any(a => a.DataInicio == DataInicio));
            AddExpression(DataFim, x => x.Vinculos.Any(a => a.DataFim == DataFim));
            AddExpression(SeqEntidade, x => x.Vinculos.Any(a => a.SeqEntidadeVinculo == SeqEntidade));

            AddExpression(Token, x => x.Vinculos.Any(a => a.TipoFuncionario.Token == Token));
            AddExpression(Data, x => x.Vinculos.Any(a => a.DataInicio <= Data && (!a.DataFim.HasValue || a.DataFim >= Data)));

            return GetExpression();
        }
    }
}