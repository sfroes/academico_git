using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
	public class TrabalhoAcademicoAutorListaViewModel : SMCViewModelBase
	{
		[SMCHidden]
		public long SeqAluno { get; set; }

		public long NumeroRegistroAcademico { get; set; }

		public string NomeExibicao
		{
			get
			{
				return Nome ?? NomeSocial;
			}
		}

		[SMCHidden]
		public string Nome { get; set; }

		[SMCHidden]
		public string NomeSocial { get; set; }

		public string CursoOfertaLocalidade { get; set; }

		public string Turno { get; set; }
        [SMCHidden]
		public string CicloLetivo { get; set; }
        [SMCHidden]
        public string TipoSituacao { get; set; }

        public string SituacaoAtualMatricula
        {
            get
            {
                return $"{CicloLetivo} - {TipoSituacao}";
            }
        }
    }
}