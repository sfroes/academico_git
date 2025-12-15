using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesParametroEnvioNotificacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ProcessoEtapaConfiguracaoNotificacao_ProcessoEtapa = 1
    }
}
