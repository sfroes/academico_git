using SMC.Framework.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.WebApi.Models
{
    public class RealizarEnvioNotificacaoSATModel : ISMCWebJobFilterModel
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqEnvioNotificacao { get; set; }
    }
}
