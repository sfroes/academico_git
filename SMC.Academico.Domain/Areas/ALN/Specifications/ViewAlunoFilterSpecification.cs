using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ALN.Specifications
{
    public class ViewAlunoFilterSpecification : SMCSpecification<ViewAluno>, ISMCMappable
    {
        public long? NumeroRegistroAcademico { get; set; }

        public List<long> NumerosRegistrosAcademicos { get; set; }

        public string Nome { get; set; }

        public long? SeqSituacaoMatricula { get; set; }
        
        public List<long> SeqsSituacaoMatricula { get; set; }

        public List<long> SeqEntidadesResponsaveis { get; set; }

        /// <summary>
        /// Sequencial do item de hierarquia que representa a localidade
        /// </summary>
        public long? SeqLocalidade { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public long? SeqCursoOferta { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqTurno { get; set; }

        public long? SeqTipoVinculoAluno { get; set; }

        public long[] Seqs { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public string Cpf { get; set; }

        public string NumeroPassaporte { get; set; }

        public long? SeqFormaIngresso { get; set; }

        public long? SeqCicloLetivoIngresso { get; set; }

        public long? SeqTipoTermoIntercambio { get; set; }

        public bool? VinculoAlunoAtivo { get; set; }

		public bool? AlunoDI { get; set; }

        public  int? CodigoAlunoMigracao { get; set; }

        public override Expression<Func<ViewAluno, bool>> SatisfiedBy()
        {
            Cpf = Cpf?.SMCRemoveNonDigits();
            AddExpression(NumeroRegistroAcademico, p => p.NumeroRegistroAcademico == NumeroRegistroAcademico);
            AddExpression(NumerosRegistrosAcademicos, p => NumerosRegistrosAcademicos.Contains(p.NumeroRegistroAcademico));
            AddExpression(Nome, p => p.Nome.Contains(Nome));
            AddExpression(SeqSituacaoMatricula, p => p.SeqSituacaoMatricula == SeqSituacaoMatricula);
            AddExpression(SeqsSituacaoMatricula, p => SeqsSituacaoMatricula.Contains(p.SeqSituacaoMatricula));
            AddExpression(SeqEntidadesResponsaveis, p => SeqEntidadesResponsaveis.Contains(p.SeqEntidadeVinculo));
            AddExpression(SeqLocalidade, p => p.SeqLocalidade == SeqLocalidade);
            AddExpression(SeqNivelEnsino, p => p.SeqNivelEnsino == SeqNivelEnsino);
            AddExpression(SeqCursoOferta, p => p.SeqCursoOferta == SeqCursoOferta);
            AddExpression(SeqFormacaoEspecifica, p => p.SeqsFormacaoEspecifica.Contains(":" + SeqFormacaoEspecifica + ":"));
            AddExpression(SeqTurno, p => (p.SeqTurno == SeqTurno));
            AddExpression(SeqTipoVinculoAluno, p => p.SeqTipoVinculoAluno == SeqTipoVinculoAluno);
            AddExpression(Seqs, p => Seqs.Contains(p.SeqPessoaAtuacao));
            AddExpression(SeqCicloLetivo, p => p.SeqCicloLetivo == SeqCicloLetivo);
            AddExpression(Cpf, p => p.Cpf == Cpf);
            AddExpression(NumeroPassaporte, p => p.NumeroPassaporte == NumeroPassaporte);
            AddExpression(SeqFormaIngresso, p => p.SeqFormaIngresso == SeqFormaIngresso);
            AddExpression(SeqCicloLetivoIngresso, p => p.SeqCicloLetivoIngresso == SeqCicloLetivoIngresso);
            AddExpression(this.SeqTipoTermoIntercambio, p => p.SeqTipoTermoIntercambio == this.SeqTipoTermoIntercambio);
            AddExpression(VinculoAlunoAtivo, p => p.VinculoAlunoAtivo == VinculoAlunoAtivo.Value);
			AddExpression(AlunoDI, p => p.AlunoDI == AlunoDI);
            AddExpression(CodigoAlunoMigracao, p => p.CodigoAlunoMigracao == CodigoAlunoMigracao);
            return GetExpression();
        }
    }
}