using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
	public class TrabalhoAcademicoAutoriaViewModel : SMCViewModelBase
	{
		[SMCHidden]
		public long SeqTrabalhoAcademicoAutoria { get; set; }

		/// <summary>
		/// Vínculo Ativo
		/// </summary>
		[SMCHidden]
		public bool VinculoAlunoAtivoReadOnly { get => VinculoAlunoAtivo; }

		[SMCHidden]
		public bool AlunoDI { get; set; } = false;

		[SMCHidden]
		public bool VinculoAlunoAtivo { get => true; }

		[AlunoLookup]
		[SMCRequired]
		[SMCUnique]
		[SMCDependency(nameof(AlunoDI))]
		[SMCMapProperty(nameof(AlunoLookupViewModel.Nome))]
		[SMCDependency(nameof(TrabalhoAcademicoDynamicModel.SeqNivelEnsino))]
		[SMCDependency(nameof(VinculoAlunoAtivo))]
		[SMCDependency(nameof(VinculoAlunoAtivoReadOnly))]
		[SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
		public AlunoLookupViewModel SeqAluno { get; set; }

		[SMCHidden]
		[SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid14_24)]
		public string NomeAutor { get => SeqAluno?.Nome; }

		[SMCHidden]
		public bool ManterReadOnly => true;

		[SMCReadOnly]
		[SMCConditionalReadonly(nameof(ManterReadOnly), true)]
		[SMCDependency(nameof(SeqAluno), nameof(TrabalhoAcademicoController.FormatarNome), "TrabalhoAcademico", false)]
		[SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid10_24)]
		public string NomeAutorFormatado { get; set; }

		[SMCHidden]
		[SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
		public string NomeAutorFormatadoOLD
		{
			get
			{
				if (nomeFormatado == null)
					return FormatarNome(NomeAutor);
				else
					return nomeFormatado;
			}
			set => nomeFormatado = value;
		}

		private string nomeFormatado;

		/// <summary>
		/// RN_ORT_006 - Gerar nome formatado
		/// O Nome Formatado será gerado de acordo com as normas ABNT: "Sobrenome" + "," + "Prenome".
		/// Exceções:
		/// *	Sobrenome indicativo de parentesco acompanham o último sobrenome: "Sobrenome" + "Último Sobrenome" + ", " + "Prenome".
		/// *	Sobrenome composto, expressão composta: "Sobrenome composto" + "," + "Prenome".
		/// *	Sobrenome contendo partículas como "de", "da" e "e", a partícula é citada posteriormente ao prenome: "Sobrenome" + "," + "Prenome"(até à última partícula).
		/// </summary>
		/// <param name="nome"></param>
		/// <returns>Nome formatado</returns>
		public string FormatarNome(string nome)
		{   ///TODO: Melhorar implementação.
			if (!string.IsNullOrEmpty(nome))
			{
				string[] nomes = nome.Split(' ');

				if (nomes.Length == 1) { return Convert.ToString(nomes[0]); }

				return string.Format($"{nomes[nomes.Length - 1]}, {nomes[0]}");
			}
			return string.Empty;
		}
	}
}