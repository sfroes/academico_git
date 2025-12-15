using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCicloLetivoTipoEvento
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoTipoEvento = 1,

        [EnumMember]
        NiveisEnsino = 2,

        [EnumMember]
        Parametros = 4,

        [EnumMember]
        Parametros_InstituicaoTipoEventoParametro = 8
    }
}