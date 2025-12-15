using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesProcessoSeletivo
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Ofertas = 1,

        [EnumMember]
        Convocacoes = 2,

        [EnumMember]
        Convocacoes_Ofertas = 3,

        [EnumMember]
        ProcessosMatricula = 4,

        [EnumMember]
        TipoProcessoSeletivo = 5,
    }
}