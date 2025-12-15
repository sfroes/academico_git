using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorVinculoObrigatorioException : SMCApplicationException
    {
        public ColaboradorVinculoObrigatorioException()
            : base(ExceptionsResource.ERR_ColaboradorVinculoObrigatorioException)
        { }
    }
}