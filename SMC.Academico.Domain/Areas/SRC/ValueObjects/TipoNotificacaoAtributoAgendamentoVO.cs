using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class TipoNotificacaoAtributoAgendamentoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoNotificacao { get; set; }

        public AtributoAgendamento AtributoAgendamento { get; set; }
    }
}