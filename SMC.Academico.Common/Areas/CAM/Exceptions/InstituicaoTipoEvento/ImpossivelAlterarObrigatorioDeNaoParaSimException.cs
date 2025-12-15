using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ImpossivelAlterarObrigatorioDeNaoParaSimException : SMCApplicationException
    {
        public ImpossivelAlterarObrigatorioDeNaoParaSimException(string descricaoTipoEvento)
            : base(string.Format(ExceptionsResource.ERR_ImpossivelAlterarObrigatorioDeNaoParaSimException, descricaoTipoEvento))
        {
        }
    }
}