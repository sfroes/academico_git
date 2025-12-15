using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesConvocadoOferta
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Convocado_Chamada_Convocacao_ProcessoSeletivo = 1 << 0,

        [EnumMember]
        Convocado_Chamada_Convocacao_CampanhaCicloLetivo = 1 << 1,

        [EnumMember]
        Chamada_Convocacao_ProcessoSeletivo = 1 << 2
    }
}