using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions.Matricula
{
    public class TermoDeAdesaoNaoPendente : SMCApplicationException
    {
        public TermoDeAdesaoNaoPendente()
            : base(ExceptionsResource.ERR_TermoDeAdesaoNaoPendente)
        {
        }
    }
}