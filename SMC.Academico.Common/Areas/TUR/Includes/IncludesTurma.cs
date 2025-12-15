using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.TUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesTurma : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Colaboradores = 1 << 1,

        [EnumMember]
        ConfiguracoesComponente = 1 << 2,

        [EnumMember]
        DivisoesTurma = 1 << 3,

        [EnumMember]
        HistoricosFechamentoDiario = 1 << 4,

        [EnumMember]
        HistoricosSituacao = 1 << 5,

        [EnumMember]
        ConfiguracoesComponente_RestricoesTurmaMatriz = 1 << 6,

        [EnumMember]
        DivisoesTurma_Colaboradores = 1 << 7,

        [EnumMember]
        DivisoesTurma_DivisoesOrganizacao = 1 << 8,

        [EnumMember]
        DivisoesTurma_Colaboradores_DivisoesOrganizacao = 1 << 9,

        [EnumMember]
        DivisoesTurma_DivisoesOrganizacao_Colaboradores = 1 << 10,

    }
}