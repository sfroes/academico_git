using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
	public class PublicacaoBdpListarDynamicModel : SMCDynamicViewModel
	{
        [SMCHidden]
        public override long Seq { get; set; }

		public string GroupBy { get => EntidadeResponsavel + OfertaCursoLocalidadeTurno; }

		[SMCSize(Framework.SMCSize.Grid12_24)]
		public string EntidadeResponsavel { get; set; }

		[SMCSize(Framework.SMCSize.Grid12_24)]
		public string OfertaCursoLocalidadeTurno { get; set; }

		//[SMCSize(Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid4_24, Framework.SMCSize.Grid4_24)]
		//public string TipoTrabalho { get; set; }

		//[SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid9_24, Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid10_24)]
		//public string Titulo { get; set; }

		//[SMCSize(Framework.SMCSize.Grid8_24, Framework.SMCSize.Grid7_24, Framework.SMCSize.Grid7_24, Framework.SMCSize.Grid8_24)]
		//public List<string> Autores { get; set; }

		[SMCSize(Framework.SMCSize.Grid3_24)]
		public string TipoTrabalho { get; set; }

		[SMCSize(SMCSize.Grid10_24)]
		public string Titulo { get; set; }

		[SMCSize(SMCSize.Grid3_24)]
		[SMCDateTimeMode(SMCDateTimeMode.Date)]
		public DateTime? DataDefesa { get; set; }

		[SMCSize(Framework.SMCSize.Grid7_24)]
		[SMCCssClass("col-sm-offset-1")]
		public List<string> Autores { get; set; }

		public SituacaoTrabalhoAcademico? Situacao { get; set; }

		[SMCSize(Framework.SMCSize.Grid1_24, Framework.SMCSize.Grid1_24, Framework.SMCSize.Grid1_24, Framework.SMCSize.Grid1_24)]
		public FiltroSituacaoTrabalhoAcademico Legenda
		{
			get
			{
				if (Situacao.HasValue)
				{
					switch (Situacao.Value)
					{
						case SituacaoTrabalhoAcademico.AguardandoCadastroAluno:
						case SituacaoTrabalhoAcademico.CadastradaAluno:
							return FiltroSituacaoTrabalhoAcademico.AguardandoAutorizacaoAluno;

						case SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria:
							return FiltroSituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria;

						case SituacaoTrabalhoAcademico.LiberadaBiblioteca:
							return FiltroSituacaoTrabalhoAcademico.LiberadaBiblioteca;

						case SituacaoTrabalhoAcademico.LiberadaConsulta:
							return FiltroSituacaoTrabalhoAcademico.LiberadaConsulta;
					}
				}
				return FiltroSituacaoTrabalhoAcademico.Nenhum;
			}
		}

		public bool DisponivelEdicao
		{
			get => Situacao.GetValueOrDefault() == SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria ||
					Situacao.GetValueOrDefault() == SituacaoTrabalhoAcademico.LiberadaBiblioteca ||
					Situacao.GetValueOrDefault() == SituacaoTrabalhoAcademico.LiberadaConsulta;
		}

        [SMCHidden]
        public bool DisponivelImpressaoAutorizacao
        {
            get => Situacao.GetValueOrDefault() == SituacaoTrabalhoAcademico.AutorizadaLiberadaSecretaria ||
					Situacao.GetValueOrDefault() == SituacaoTrabalhoAcademico.LiberadaBiblioteca || 
					Situacao.GetValueOrDefault() == SituacaoTrabalhoAcademico.LiberadaConsulta;
        }

        [SMCHidden]
        public long SeqInstituicaoNivel { get; set; }

        [SMCHidden]
        public long SeqPublicacaoBdp { get; set; }
    }
}