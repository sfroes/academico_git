using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesGrupoCurricular : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoConfiguracaoGrupoCurricular = 1,

        [EnumMember]
        TipoConfiguracaoGrupoCurricular_TiposConfiguracoesGrupoCurricularFilhos = 2,

        [EnumMember]
        ComponentesCurriculares_ComponenteCurricular = 4,

        [EnumMember]
        ComponentesCurriculares_ComponenteCurricular_NiveisEnsino = 8,

        [EnumMember]
        GruposCurricularesFilhos = 16,

        [EnumMember]
        ComponentesCurriculares_ComponenteCurricular_Configuracoes_DivisoesComponente_TipoDivisaoComponente_Modalidade = 32,

        [EnumMember]
        ComponentesCurriculares_ComponenteCurricular_Configuracoes_DivisoesComponente_TipoDivisaoComponente_TipoComponenteCurricular= 64,
    }
}