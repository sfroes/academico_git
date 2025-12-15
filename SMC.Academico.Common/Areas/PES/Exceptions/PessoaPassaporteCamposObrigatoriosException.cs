using SMC.Academico.Common.Areas.PES.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.PES.Exceptions
{
    public class PessoaPassaporteCamposObrigatoriosException : SMCApplicationException
    {
        public PessoaPassaporteCamposObrigatoriosException()
            : base(ExceptionsResource.ERR_PessoaPassaporteCamposObrigatoriosException)
        {
        }
    }
}