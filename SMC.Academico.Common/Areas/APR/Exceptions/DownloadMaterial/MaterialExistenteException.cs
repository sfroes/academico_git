using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class MaterialExistenteException : SMCApplicationException
    {
        public MaterialExistenteException(string tipo, string nome)
            : base(string.Format(ExceptionsResource.ERR_MaterialExistenteException, tipo, nome))
        {
        }
    }
}