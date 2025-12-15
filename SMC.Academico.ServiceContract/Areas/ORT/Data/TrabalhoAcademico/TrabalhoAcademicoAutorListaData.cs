using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
	public class TrabalhoAcademicoAutorListaData : ISMCMappable
	{
		public long SeqAluno { get; set; }

		public long NumeroRegistroAcademico { get; set; }

		public string Nome { get; set; }

		public string NomeSocial { get; set; }

		public string CursoOfertaLocalidade { get; set; }

		public string Turno { get; set; }

		public string CicloLetivo { get; set; }

		public string TipoSituacao { get; set; }
	}
}
