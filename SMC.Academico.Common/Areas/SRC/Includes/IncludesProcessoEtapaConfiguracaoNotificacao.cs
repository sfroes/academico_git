using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesProcessoEtapaConfiguracaoNotificacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        ParametrosEnvioNotificacao = 1 << 1,

        [EnumMember]
        ProcessoEtapa = 1 << 2,

        [EnumMember]
        TipoNotificacao = 1 << 3,

        [EnumMember]
        ProcessoUnidadeResponsavel = 1 << 4,

        [EnumMember]
        ProcessoUnidadeResponsavel_EntidadeResponsavel = 1 << 5,

        [EnumMember]
        ProcessoEtapa_Processo = 1 << 6
    }
}