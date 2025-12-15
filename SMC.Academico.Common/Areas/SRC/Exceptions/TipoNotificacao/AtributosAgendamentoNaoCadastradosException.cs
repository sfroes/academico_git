using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class AtributosAgendamentoNaoCadastradosException : SMCApplicationException
    {
        public AtributosAgendamentoNaoCadastradosException()
          : base(ExceptionsResource.ERR_AtributosAgendamentoNaoCadastrados)
        {
        }
    }
}
