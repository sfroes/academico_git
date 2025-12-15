using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelTipoOrientacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoOrientacao = 1 << 0,

        [EnumMember]
        TiposParticipacao = 1 << 1,

        [EnumMember]
        InstituicaoNivelTipoTermoIntercambio = 1 << 2
    }
}