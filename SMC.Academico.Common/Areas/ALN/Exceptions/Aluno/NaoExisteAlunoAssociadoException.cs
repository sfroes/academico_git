using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class NaoExisteAlunoAssociadoException : SMCApplicationException
    {
        public NaoExisteAlunoAssociadoException() 
            : base(ExceptionsResource.ERR_NaoExisteAlunoAssociadoException)
        { }
    }
}