using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SMC.Academico.Domain.Areas.ORT.Specifications
{
	public class PublicacaoBdpFilterSpecification : SMCSpecification<PublicacaoBdp>
	{
		public long? SeqAluno { get; set; }

		/* Filtros para o trabalho */

		public long? SeqInstituicaoLogada { get; set; }

		public long? SeqTrabalhoAcademico { get; set; }

		public List<long?> SeqsEntidadesResponsaveis { get; set; }

		public long? SeqCursoOfertaLocalidade { get; set; }

		public List<long> SeqsTurnos { get; set; }

		public long? SeqTurno { get; set; }

		public long? SeqCicloLetivo { get; set; }

		public long? SeqTipoSituacao { get; set; }

		public List<FiltroSituacaoTrabalhoAcademico> Situacao { get; set; }

		public override Expression<Func<PublicacaoBdp, bool>> SatisfiedBy()
		{
			AddExpression(this.SeqAluno, a => a.TrabalhoAcademico.Autores.Any(au => au.SeqAluno == this.SeqAluno));
			AddExpression(SeqTrabalhoAcademico, x => x.SeqTrabalhoAcademico == SeqTrabalhoAcademico);
			AddExpression(SeqInstituicaoLogada, x => x.TrabalhoAcademico.SeqInstituicaoEnsino == SeqInstituicaoLogada);
			AddExpression(SeqsEntidadesResponsaveis, p => p.TrabalhoAcademico.Autores.Any(a => a.Aluno.Historicos.Any(z => SeqsEntidadesResponsaveis.Contains(z.SeqEntidadeVinculo))));
			AddExpression(SeqCursoOfertaLocalidade, p => p.TrabalhoAcademico.Autores.Any(a => a.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Seq == this.SeqCursoOfertaLocalidade));
			AddExpression(SeqTurno, p => p.TrabalhoAcademico.Autores.Any(a => a.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.Turno.Seq == this.SeqTurno));
			AddExpression(SeqsTurnos, p => p.TrabalhoAcademico.Autores.Any(a => SeqsTurnos.Contains(a.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CursoOfertaLocalidadeTurno.Turno.Seq)));

			if (SeqCicloLetivo.HasValue)
			{
				AddExpression(SeqCicloLetivo, p => p.TrabalhoAcademico.Autores.Any(a => a.Aluno.Historicos.Any(w => w.Atual && w.HistoricosCicloLetivo.Any(h => h.SeqCicloLetivo == SeqCicloLetivo && !h.DataExclusao.HasValue))));
				AddExpression(SeqTipoSituacao, p => p.TrabalhoAcademico.Autores.Any(a => a.Aluno.Historicos.FirstOrDefault(w => w.Atual)
																		.HistoricosCicloLetivo.FirstOrDefault(o => o.SeqCicloLetivo == SeqCicloLetivo && !o.DataExclusao.HasValue)
																		.AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao)
																		.FirstOrDefault(o => o.DataInicioSituacao <= DateTime.Today && !o.DataExclusao.HasValue)
																		.SituacaoMatricula.SeqTipoSituacaoMatricula == this.SeqTipoSituacao));
			}
			else
			{
				AddExpression(SeqTipoSituacao, p => p.TrabalhoAcademico.Autores.Any(a => a.Aluno.Historicos.FirstOrDefault(w => w.Atual)
														.HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(o => !o.DataExclusao.HasValue)
														.AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao)
														.FirstOrDefault(o => o.DataInicioSituacao <= DateTime.Today && !o.DataExclusao.HasValue)
														.SituacaoMatricula.SeqTipoSituacaoMatricula == this.SeqTipoSituacao));
			}

			//AddExpression(SeqCicloLetivo, p => p.TrabalhoAcademico.Autores.Any(a => a.Aluno.Historicos.Where(w => w.Atual).FirstOrDefault().CicloLetivo.Seq == this.SeqCicloLetivo));
			//         AddExpression(SeqTipoSituacao, p => p.TrabalhoAcademico.Autores.Any(a => a.Aluno.Historicos.FirstOrDefault(w => w.Atual).HistoricosCicloLetivo.OrderByDescending(o => o.CicloLetivo.Ano).ThenByDescending(o => o.CicloLetivo.Numero).FirstOrDefault(o => !o.DataExclusao.HasValue).AlunoHistoricoSituacao.OrderByDescending(o => o.DataInicioSituacao).FirstOrDefault(o => o.DataInicioSituacao <= DateTime.Today && !o.DataExclusao.HasValue).SituacaoMatricula.SeqTipoSituacaoMatricula == this.SeqTipoSituacao));

			if (Situacao != null && Situacao.Count > 0)
			{
				// Converte a lista de situações do filtro para as situações do trabalho.
				var situacoes = new List<SituacaoTrabalhoAcademico>();
				foreach (var situacao in Situacao)
				{
					if (situacao == FiltroSituacaoTrabalhoAcademico.AguardandoAutorizacaoAluno)
					{
						situacoes.Add(SituacaoTrabalhoAcademico.AguardandoCadastroAluno);
						situacoes.Add(SituacaoTrabalhoAcademico.CadastradaAluno);
					}
					else if (situacao == FiltroSituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria)
					{
						situacoes.Add(SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria);
					}
					else if (situacao == FiltroSituacaoTrabalhoAcademico.LiberadaBiblioteca)
					{
						situacoes.Add(SituacaoTrabalhoAcademico.LiberadaBiblioteca);
					}
					else if (situacao == FiltroSituacaoTrabalhoAcademico.LiberadaConsulta)
					{
						situacoes.Add(SituacaoTrabalhoAcademico.LiberadaConsulta);
					}
				}

				AddExpression(p => situacoes.Contains(p.HistoricoSituacoes.OrderByDescending(o => o.DataInclusao)
																	.FirstOrDefault().SituacaoTrabalhoAcademico));
			}

			return GetExpression();
		}
	}
}