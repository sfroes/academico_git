using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data.SolicitacaoMatricula
{
	public class DadosSolicitacaoMatriculaRenovacaoFiltroData : ISMCMappable
	{
		public long? SeqPessoaAtuacao { get; set; }
		public long? SeqPessoa { get; set; }
	}
}