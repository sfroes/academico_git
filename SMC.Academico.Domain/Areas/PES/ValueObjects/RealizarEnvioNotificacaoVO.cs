using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Jobs;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
	public class RealizarEnvioNotificacaoVO : ISMCMappable

    {
		public long SeqEnvioNotificacaoDestinatario { get; set; }

		public long SeqEnvioNotificacao { get; set; }
		public long SeqPessoaAtuacao { get; set; }
		public long SeqConfiguracaoTipoNotificacao { get; set; }
		public long? SeqLayoutMensagemEmail { get; set; }
		public TipoAtuacao TipoAtuacao { get; set; }
		
	}
}