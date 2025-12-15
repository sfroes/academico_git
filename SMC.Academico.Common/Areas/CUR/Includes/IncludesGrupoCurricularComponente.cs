using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesGrupoCurricularComponente : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        GrupoCurricular = 1,

        [EnumMember]
        ComponenteCurricular = 2,

        [EnumMember]
        ComponenteCurricular_NiveisEnsino = 4,

        [EnumMember]
        ComponenteCurricular_EntidadesResponsaveis = 8,
    }
}