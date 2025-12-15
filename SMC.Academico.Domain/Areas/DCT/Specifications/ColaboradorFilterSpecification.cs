using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.DCT.Specifications
{
    public class ColaboradorFilterSpecification : SMCSpecification<Colaborador>
    {
        public ColaboradorFilterSpecification()
        {
            SetOrderBy(o => o.DadosPessoais.Nome);
        }
        public long? Seq { get; set; }
        public long[] Seqs { get; set; }
        public long? SeqPessoa { get; set; }
        public long? SeqInstituicaoExterna { get; set; }
        public SituacaoColaborador? SituacaoColaboradorNaInstituicao { get; set; }
        /// <summary>
        /// Entidade referenciada pela instituição externa
        /// </summary>
        public long? SeqEntidadeInstituicaoExterna { get; set; }
        /// <summary>
        /// Sequencial do usuario logado no Sass
        /// </summary>
        public long? SeqUsuarioSAS { get; set; }
        /// <summary>
        /// Filtra por múltiplas entidades externas
        /// </summary>
        public long[] SeqsInstituicoesExternas { get; set; }
        /// <summary>
        /// Filtra por instituição de ensino no portal
        /// </summary>
        public long? SeqInstituicaoEnsino { get; set; }
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public string Cpf { get; set; }
        public string NumeroPassaporte { get; set; }
        public long[] SeqsColaboradorVinculo { get; set; }
        public long? SeqCompomenteAptoLecionar { get; set; }
        public long? SeqTipoVinculoColaborador { get; set; }
        public long[] SeqsEntidadesResponsaveis { get; set; }

        public override Expression<Func<Colaborador, bool>> SatisfiedBy()
        {
            AddExpression(Seq, x => x.Seq == Seq);

            if (!string.IsNullOrEmpty(Cpf))
                Cpf = Cpf.SMCRemoveNonDigits();

            AddExpression(Seqs, p => Seqs.Contains(p.Seq));
            AddExpression(SeqPessoa, x => x.SeqPessoa == SeqPessoa);
            AddExpression(SeqInstituicaoExterna, x => x.InstituicoesExternas.Any(a => a.SeqInstituicaoExterna == SeqInstituicaoExterna && (!SituacaoColaboradorNaInstituicao.HasValue || a.Ativo == (SituacaoColaboradorNaInstituicao == SituacaoColaborador.Ativo))));
            AddExpression(SeqsInstituicoesExternas, x => x.InstituicoesExternas.Any(a => SeqsInstituicoesExternas.Contains(a.SeqInstituicaoExterna) && (!SituacaoColaboradorNaInstituicao.HasValue || a.Ativo == (SituacaoColaboradorNaInstituicao == SituacaoColaborador.Ativo))));
            AddExpression(SeqUsuarioSAS, x => x.Pessoa.SeqUsuarioSAS == SeqUsuarioSAS);
            AddExpression(SeqInstituicaoEnsino, x => x.Pessoa.SeqInstituicaoEnsino == SeqInstituicaoEnsino);
            AddExpression(Nome, x => x.DadosPessoais.Nome.Contains(Nome) || x.DadosPessoais.NomeSocial.Contains(Nome));
            AddExpression(NomeSocial, x => x.DadosPessoais.Nome.Contains(NomeSocial) || x.DadosPessoais.NomeSocial.Contains(NomeSocial));
            AddExpression(Cpf, x => x.Pessoa.Cpf == Cpf);
            AddExpression(NumeroPassaporte, x => x.Pessoa.NumeroPassaporte.Contains(NumeroPassaporte));
            // Considera também uma lista vazia
            if (SeqsColaboradorVinculo != null)
                AddExpression(x => x.Vinculos.Any(a => SeqsColaboradorVinculo.Contains(a.Seq)));
            AddExpression(SeqCompomenteAptoLecionar, x => x.ComponentesAptoLecionar.Any(a => a.SeqComponenteCurricular == SeqCompomenteAptoLecionar));

            if (SeqTipoVinculoColaborador.HasValue)
                AddExpression(SeqTipoVinculoColaborador, x => x.Vinculos.Any(c => c.SeqTipoVinculoColaborador == this.SeqTipoVinculoColaborador));

            if (SeqsEntidadesResponsaveis != null)
                AddExpression(SeqsEntidadesResponsaveis, w => w.Vinculos.Any(v => SeqsEntidadesResponsaveis.Contains(v.SeqEntidadeVinculo)));

            return GetExpression();
        }
    }
}