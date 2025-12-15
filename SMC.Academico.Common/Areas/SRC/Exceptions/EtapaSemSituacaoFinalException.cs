using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
	public class EtapaSemSituacaoFinalException : SMCApplicationException
	{
		public EtapaSemSituacaoFinalException(string etapa)
			: base(string.Format(ExceptionsResource.ERR_EtapaSemSituacaoFinalException, etapa))
		{ }
	}
}