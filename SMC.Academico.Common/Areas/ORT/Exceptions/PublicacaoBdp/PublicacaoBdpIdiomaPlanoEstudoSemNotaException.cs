using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpIdiomaPlanoEstudoSemNotaException : SMCApplicationException
    {
        public PublicacaoBdpIdiomaPlanoEstudoSemNotaException(string turmasSemNota) 
            : base(string.Format(ExceptionsResource.ERR_PublicacaoBdpIdiomaPlanoEstudoSemNotaException, turmasSemNota))
        { }
    }
}
