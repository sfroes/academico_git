using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class FormacaoEspecificaMesmaDescricaoProgramaException : SMCApplicationException
    {
        public FormacaoEspecificaMesmaDescricaoProgramaException()
            : base(ExceptionsResource.ERR_FormacaoEspecificaMesmaDescricaoProgramaException)
        { }
    }
}