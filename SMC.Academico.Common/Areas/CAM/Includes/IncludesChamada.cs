using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesChamada
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Convocacao = 1,

        [EnumMember]
        Convocacao_ProcessoSeletivo = 2,
        

        [EnumMember]
        GrupoEscalonamento = 4,

        [EnumMember]
        Convocados = 8,

        [EnumMember]
        Convocacao_CampanhaCicloLetivo = 16,

        [EnumMember]
        Convocacao_CampanhaCicloLetivo_Campanha = 32,

        [EnumMember]
        GrupoEscalonamento_Itens = 64,

        [EnumMember]
        GrupoEscalonamento_Itens_Escalonamento = 128,

        [EnumMember]
        GrupoEscalonamento_Itens_Escalonamento_Parcelas = 256
    }
} 