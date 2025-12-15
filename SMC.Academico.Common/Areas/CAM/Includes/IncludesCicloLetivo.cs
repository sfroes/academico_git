using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCicloLetivo
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        NiveisEnsino = 1 << 1,

        [EnumMember]
        RegimeLetivo = 1 << 2,

        [EnumMember]
        TiposEvento = 1 << 3,

        [EnumMember]
        TiposEvento_InstituicaoTipoEvento = 1 << 4,

        [EnumMember]
        TiposEvento_Parametros = 1 << 5,

        [EnumMember]
        TiposEvento_Parametros_InstituicaoTipoEventoParametro = 1 << 6,

        [EnumMember]
        TiposEvento_EventosLetivos = 1 << 7,

        [EnumMember]
        TiposEvento_EventosLetivos_NiveisEnsino = 1 << 8,

        [EnumMember]
        TiposEvento_EventosLetivos_ParametrosEntidade = 1 << 9
    }
}