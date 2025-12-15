using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{  
    public class SolicitacaoServicoDisciplinaIsoladaSemMatriculaException : SMCApplicationException
    {
        public SolicitacaoServicoDisciplinaIsoladaSemMatriculaException()
            : base(ExceptionsResource.ERR_SolicitacaoServicoDisciplinaIsoladaSemMatriculaException)
        { }
    }
}
