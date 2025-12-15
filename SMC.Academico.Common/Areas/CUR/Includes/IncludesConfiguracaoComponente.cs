using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesConfiguracaoComponente
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ComponenteCurricular = 1,

        [EnumMember]
        DivisoesComponente = 2,

        [EnumMember]
        ComponenteCurricular_InstituicaoEnsino = 4,

        [EnumMember]
        ComponenteCurricular_EntidadesResponsaveis_Entidade = 16,

        [EnumMember]
        ComponenteCurricular_NiveisEnsino = 32,

        [EnumMember]
        DivisoesComponente_TipoDivisaoComponente = 64,

        [EnumMember]
        ComponenteCurricular_NiveisEnsino_NivelEnsino = 128,

        [EnumMember]
        DivisoesComponente_TipoDivisaoComponente_Modalidade = 256,

        [EnumMember]
        DivisoesComponente_ComponenteCurricularOrganizacao = 512,
    }
}