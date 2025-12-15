using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesDivisaoMatrizCurricularComponente
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        DivisaoMatrizCurricular = 1 << 01,

        [EnumMember]
        DivisaoMatrizCurricular_DivisaoCurricularItem = 1 << 02,

        [EnumMember]
        GrupoCurricularComponente = 1 << 03,

        [EnumMember]
        GrupoCurricularComponente_ComponenteCurricular = 1 << 04,

        [EnumMember]
        TurnosExcecao = 1 << 05,

        [EnumMember]
        DivisoesComponente = 1 << 06,

        [EnumMember]
        DivisoesComponente_DivisaoComponente = 1 << 07,

        [EnumMember]
        ComponentesCurricularSubstitutos = 1 << 09,

        [EnumMember]
        ConfiguracaoComponente = 1 << 10,

        [EnumMember]
        ConfiguracaoComponente_ComponenteCurricular = 1 << 11,

        [EnumMember]
        ConfiguracaoComponente_ComponenteCurricular_NiveisEnsino = 1 << 12,

        [EnumMember]
        ConfiguracaoComponente_ComponenteCurricular_EntidadesResponsaveis_Entidade = 1 << 13,

        [EnumMember]
        MatrizCurricular = 1 << 14,

        [EnumMember]
        GrupoCurricularComponente_ComponenteCurricular_NiveisEnsino = 1 << 15,

        [EnumMember]
        ComponentesCurricularSubstitutos_TipoComponente = 1 << 16,

        [EnumMember]
        ComponentesCurricularSubstitutos_NiveisEnsino_NivelEnsino = 1 << 17,

        [EnumMember]
        ComponentesCurricularSubstitutos_EntidadesResponsaveis = 1 << 18,

        [EnumMember]
        ComponentesCurricularSubstitutos_EntidadesResponsaveis_Entidade = 1 << 19,
    }
}