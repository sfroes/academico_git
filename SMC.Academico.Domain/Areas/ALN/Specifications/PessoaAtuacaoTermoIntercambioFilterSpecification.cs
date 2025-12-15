using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class PessoaAtuacaoTermoIntercambioFilterSpecification : SMCSpecification<PessoaAtuacaoTermoIntercambio>
    {
        public long? Seq { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public long? SeqTermoIntercambio { get; set; }

        public bool? Ativo { get; set; }

        public long[] SeqsTermoIntercambio { get; set; }

		public long? SeqTipoTermoIntercambio { get; set; }

        public long? SeqTipoVinculo { get; set; }

        public TipoAtuacao? TipoAtuacao { get; set; }
        
        public string Nome { get; set; }

        public List<long> SeqEntidadesResponsaveis { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqInstituicaoEnsino { get; set; }

        public TipoMobilidade? TipoMobilidade { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public override Expression<Func<PessoaAtuacaoTermoIntercambio, bool>> SatisfiedBy()
        {
            AddExpression(this.Seq, p => p.Seq == this.Seq);
            AddExpression(this.SeqPessoaAtuacao, p => p.SeqPessoaAtuacao == this.SeqPessoaAtuacao);
            AddExpression(this.SeqTermoIntercambio, p => p.SeqTermoIntercambio == this.SeqTermoIntercambio);
            AddExpression(this.Ativo, p => p.Ativo == this.Ativo);
            AddExpression(this.SeqsTermoIntercambio, p => this.SeqsTermoIntercambio.Contains(p.SeqTermoIntercambio));
			AddExpression(this.SeqTipoTermoIntercambio, p => p.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio == SeqTipoTermoIntercambio);
            AddExpression(this.SeqTipoVinculo, p => (p.PessoaAtuacao as Aluno).TipoVinculoAluno.Seq == this.SeqTipoVinculo || (p.PessoaAtuacao as Ingressante).TipoVinculoAluno.Seq == this.SeqTipoVinculo);
            AddExpression(this.TipoAtuacao, p => p.PessoaAtuacao.TipoAtuacao == TipoAtuacao);            
            AddExpression(this.Nome, p => p.PessoaAtuacao.DadosPessoais.Nome.Contains(Nome) || p.PessoaAtuacao.DadosPessoais.NomeSocial.Contains(Nome));
            AddExpression(this.SeqEntidadesResponsaveis, p => (p.PessoaAtuacao as Aluno).Historicos.Where(c => c.Atual).Any(a => SeqEntidadesResponsaveis.Contains(a.SeqEntidadeVinculo)) || this.SeqEntidadesResponsaveis.Contains((p.PessoaAtuacao as Ingressante).SeqEntidadeResponsavel.Value));
            AddExpression(this.SeqNivelEnsino, p => (p.PessoaAtuacao as Aluno).Historicos.Where(c => c.Atual).Any(a => a.SeqNivelEnsino == SeqNivelEnsino) || (p.PessoaAtuacao as Ingressante).SeqNivelEnsino == this.SeqNivelEnsino);
          
            //[ UC_ALN_004_03 ] NV07 2.2 -> Se as datas forem informadas, não será exibido registro que não possui período de intercâmbio. 
            AddExpression(this.DataInicio, p => p.Periodos.All(d => d.DataInicio >= this.DataInicio.Value) && p.Periodos.Count > 0);
            AddExpression(this.DataFim, p => p.Periodos.All(d => d.DataFim <= this.DataFim.Value) && p.Periodos.Count > 0);
            
            AddExpression(this.SeqInstituicaoEnsino, p => p.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.SeqInstituicaoExterna == this.SeqInstituicaoEnsino);
            AddExpression(this.TipoMobilidade, p => p.TipoMobilidade == this.TipoMobilidade);

            return GetExpression();
        }
    }
}