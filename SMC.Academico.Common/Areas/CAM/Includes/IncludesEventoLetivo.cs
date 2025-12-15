using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesEventoLetivo : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        CicloLetivoTipoEvento = 1,

        [EnumMember]
        CicloLetivoTipoEvento_CicloLetivo = 2,

        [EnumMember]
        NiveisEnsino = 4
    }
}