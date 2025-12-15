using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class QuantidadeGrupoExcedidoException: SMCApplicationException
    {
        public QuantidadeGrupoExcedidoException()
            : base(ExceptionsResource.ERR_QuantidadeGrupoExcedidoException)
        {
        }
    }
}
