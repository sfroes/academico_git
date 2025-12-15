using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class TipoNotificacaoAtributoAgendamentoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public AtributoAgendamento AtributoAgendamento { get; set; }
    }
}