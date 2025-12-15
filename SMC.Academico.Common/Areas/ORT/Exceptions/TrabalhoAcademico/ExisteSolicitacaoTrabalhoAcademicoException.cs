using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;
using System;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class ExisteSolicitacaoTrabalhoAcademicoException : SMCApplicationException
    {
        public ExisteSolicitacaoTrabalhoAcademicoException()
            : base(string.Format(ExceptionsResource.ERR_ExisteSolicitacaoTrabalhoAcademicoException))
        {
        }
    }
}