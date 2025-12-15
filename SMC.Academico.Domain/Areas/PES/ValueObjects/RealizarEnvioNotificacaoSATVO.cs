using SMC.Framework.Jobs;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
	public class RealizarEnvioNotificacaoSATVO : ISMCWebJobFilterModel

    {
		public long SeqHistoricoAgendamento { get; set; }

		public long SeqEnvioNotificacao { get; set; }

		
	}
}