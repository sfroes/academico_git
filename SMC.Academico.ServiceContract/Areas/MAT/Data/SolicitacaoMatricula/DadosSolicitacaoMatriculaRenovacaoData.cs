using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data.SolicitacaoMatricula
{
	public class DadosSolicitacaoMatriculaRenovacaoData : ISMCMappable
	{
		public bool ExisteRematriculaAberta { get; set; }
		public long? SeqSolicitacaoServico { get; set; }
		public DateTime? DataInicioRematricula { get; set; }
		public DateTime? DataFimRematricula { get; set; }
		public TipoMatricula? TipoMatricula { get; set; }
		public string DescricaoProcesso { get; set; }
	}
}