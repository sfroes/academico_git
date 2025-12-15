using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
	public class ColaboradorVinculoSupervisaoSobrepostaException : SMCApplicationException
	{
		public ColaboradorVinculoSupervisaoSobrepostaException()
			: base(ExceptionsResource.ERR_ColaboradorVinculoSupervisaoSobrepostaException)
		{
		}
	}
}