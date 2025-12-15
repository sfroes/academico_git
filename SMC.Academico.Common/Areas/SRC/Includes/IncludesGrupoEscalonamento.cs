using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesGrupoEscalonamento
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        Itens = 1 << 1,

        [EnumMember]
        Processo = 1 << 2,

        [EnumMember]
        Itens_Escalonamento = 1 << 3,

        [EnumMember]
        Processo_Etapas = 1 << 4,

        [EnumMember]
        Processo_Servico = 1 << 5,

        [EnumMember]
        Itens_Escalonamento_ProcessoEtapa = 1 << 6,

        [EnumMember]
        Itens_Parcelas = 1 << 7
    }
}