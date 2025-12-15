using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ParametroEnvioNotificacaoItemVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcessoEtapaConfiguracaoNotificacao { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }

        public short QuantidadeDiasInicioEnvio { get; set; }

        public Temporalidade Temporalidade { get; set; }

        public AtributoAgendamento AtributoAgendamento { get; set; }

        public short? QuantidadeDiasRecorrencia { get; set; }

        public bool? ReenviarNotificacao { get; set; }

        public bool Ativo { get; set; }

        public bool PossuiNotificacaoEnviada { get; set; }
    }
}