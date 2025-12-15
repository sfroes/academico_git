using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoTipoEvento
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoAgenda = 1,

        [EnumMember]
        InstituicaoEnsino = 2,

        [EnumMember]
        Parametros = 4,

        [EnumMember]
        CiclosLetivosTipoEvento = 8,

        [EnumMember]
        InstituicaoTipoEvento = 16
    }
}