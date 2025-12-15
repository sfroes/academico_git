using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.TUR.Resources;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaSemMatrizPrincipalException : SMCApplicationException
    {
        public TurmaSemMatrizPrincipalException(string descricao) 
            : base(string.Format(ExceptionsResource.ERR_TurmaSemMatrizPrincipalException, descricao))
        {}
    }
}
