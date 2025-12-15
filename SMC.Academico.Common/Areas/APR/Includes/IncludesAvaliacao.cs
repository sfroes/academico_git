using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesAvaliacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        AplicacoesAvaliacao = 1 << 0,

        [EnumMember]
        AplicacoesAvaliacao_ApuracoesAvaliacao = 1 << 1,

        [EnumMember]
        ArquivoAnexadoInstrucao = 1 << 2,

        [EnumMember]
        AplicacoesAvaliacao_EntregasOnline = 1 << 3        
    }
}
