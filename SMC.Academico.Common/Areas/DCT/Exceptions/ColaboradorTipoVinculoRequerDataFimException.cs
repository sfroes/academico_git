using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorTipoVinculoRequerDataFimException : SMCApplicationException
    {
        public ColaboradorTipoVinculoRequerDataFimException()
            : base(ExceptionsResource.ERR_ColaboradorTipoVinculoRequerDataFimException)
        {
        }
    }
}