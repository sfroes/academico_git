using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelTipoComponenteCurricular
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TiposDivisaoComponente = 1,

        [EnumMember]
        TipoComponenteCurricular = 2,

        [EnumMember]
        InstituicaoNivel = 4,

        [EnumMember]
        TiposDivisaoComponente_TipoDivisaoComponente = 8
    }
}