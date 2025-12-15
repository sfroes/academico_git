using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class OrientacaoConvocadosException : SMCApplicationException
    {
        public OrientacaoConvocadosException()
            : base(ExceptionsResource.ERR_OrientacaoConvocadosException)
        {
        }
    }
}