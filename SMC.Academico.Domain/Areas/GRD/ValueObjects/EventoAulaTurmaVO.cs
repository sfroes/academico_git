using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using SMC.Academico.Common.Areas.GRD.Enums;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class EventoAulaTurmaVO : ISMCMappable
    {
        public EventoAulaTurmaCabecalhoVO EventoAulaTurmaCabecalho { get; set; }

        public List<EventoAulaDivisaoTurmaVO> EventoAulaDivisoesTurma { get; set; }

        //Tokens
        public bool PermiteAlterarDataAgendamentoAula { get; set; }
    }
}
