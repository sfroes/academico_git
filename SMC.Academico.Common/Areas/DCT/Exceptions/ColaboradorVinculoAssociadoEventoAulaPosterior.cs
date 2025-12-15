using SMC.Academico.Common.Areas.DCT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.DCT.Exceptions
{
    public class ColaboradorVinculoAssociadoEventoAulaPosterior : SMCApplicationException
    {
        public ColaboradorVinculoAssociadoEventoAulaPosterior()
            : base(ExceptionsResource.ERR_ColaboradorVinculoAssociadoEventoAulaPosterior)
        {
        }
    }
}
