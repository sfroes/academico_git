using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class EventoAulaTurmaData : ISMCMappable
    {
        public EventoAulaTurmaCabecalhoData EventoAulaTurmaCabecalho { get; set; }

        public List<EventoAulaDivisaoTurmaData> EventoAulaDivisoesTurma { get; set; }

        //Tokens
        public bool PermiteAlterarDataAgendamentoAula { get; set; }
    }
}
