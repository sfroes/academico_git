using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesMatrizCurricularOferta
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        MatrizCurricular = 1 << 01,

        [EnumMember]
        MatrizCurricular_Requisitos_Requisito_Itens_ComponenteCurricular_Configuracoes = 1 << 02,

        [EnumMember]
        MatrizCurricular_Requisitos_Requisito_Itens_ComponenteCurricular_Configuracoes_DivisoesComponente_TipoDivisaoComponente = 1 << 03,

        [EnumMember]
        MatrizCurricular_Requisitos_Requisito_Itens_ComponenteCurricular_NiveisEnsino = 1 << 04,

        [EnumMember]
        MatrizCurricular_Requisitos_Requisito_ComponenteCurricular_Configuracoes = 1 << 05,

        [EnumMember]
        MatrizCurricular_Requisitos_Requisito_ComponenteCurricular_Configuracoes_DivisoesComponente_TipoDivisaoComponente = 1 << 06,

        [EnumMember]
        MatrizCurricular_Requisitos_Requisito_ComponenteCurricular_NiveisEnsino = 1 << 07,
    }
}