using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class TrabalhoAcademicoComPublicacaoBdpException : SMCApplicationException
    {
        public TrabalhoAcademicoComPublicacaoBdpException()
            : base(ExceptionsResource.ERR_TrabalhoAcademicoComPublicacaoBdpException)
        {
        }
    }
}