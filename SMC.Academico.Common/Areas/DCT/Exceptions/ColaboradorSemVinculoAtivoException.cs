using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;
using System.Collections.Generic;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
	public class ColaboradorSemVinculoAtivoException : SMCApplicationException
	{
		public ColaboradorSemVinculoAtivoException(List<string> colaboradores)
			: base(string.Format(ExceptionsResource.ERR_ColaboradorSemVinculoAtivoException, string.Join("<br />", colaboradores ?? new List<string>())))
		{
		}
	}
}