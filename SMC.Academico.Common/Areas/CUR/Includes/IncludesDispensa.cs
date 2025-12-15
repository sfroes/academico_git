using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesDispensa
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        GrupoOrigem = 1,

        [EnumMember]
        GrupoDispensado = 2,

        [EnumMember]
        HistoricosVigencia = 4,

        [EnumMember]
        MatrizesExcecao = 8,

        [EnumMember]
        GrupoOrigem_Componentes = 16,

        [EnumMember]
        GrupoDispensado_Componentes = 32,

        [EnumMember]
        GrupoOrigem_Componentes_ComponenteCurricular = 64,

        [EnumMember]
        GrupoDispensado_Componentes_ComponenteCurricular = 128,
    }
}