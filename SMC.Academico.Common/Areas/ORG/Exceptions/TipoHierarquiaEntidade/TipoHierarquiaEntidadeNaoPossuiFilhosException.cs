using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class TipoHierarquiaEntidadeNaoPossuiFilhosException : SMCApplicationException
    {
       public TipoHierarquiaEntidadeNaoPossuiFilhosException() 
            : base(Resources.ExceptionsResource.ERR_TipoHierarquiaEntidadeNaoPossuiFilhos) { }
    }
}
