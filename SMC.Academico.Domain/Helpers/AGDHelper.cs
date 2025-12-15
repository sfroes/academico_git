using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Framework;
using SMC.Framework.Exceptions;
using System.Linq;

namespace SMC.Academico.Domain.Helpers
{
    public static class AGDHelper
    {
        public static void TratarErroAGD(RetornoOperacaoEventoData retorno)
        {
            if (retorno.OperacoesErro.SMCAny())
            {
                var mensagem = string.Join("<br/>", retorno.OperacoesErro.Select(s => s.Mensagem));
                throw new SMCApplicationException(mensagem);
            }
        }
    }
}
