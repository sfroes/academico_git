using SMC.Framework.UI.Mvc;
using System;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Models
{
	public class ConsultaDadosAlunoTermoIntercambioViewModel : SMCViewModelBase
	{
		public DateTime DataInicio { get; set; }

		public DateTime DataFim { get; set; }

		public string DescricaoTipoIntercambio { get; set; }

		public string DescricaoTermoIntercambio { get; set; }

		public string InstituicaoExterna { get; set; }
	}
}