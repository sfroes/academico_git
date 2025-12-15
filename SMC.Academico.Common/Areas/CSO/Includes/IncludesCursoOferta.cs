using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCursoOferta : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Curso = 1,

        [EnumMember]
        Curso_HierarquiasEntidades = 2,

        [EnumMember]
        Curso_HierarquiasEntidades_ItemSuperior = 4,

        [EnumMember]
        Curso_HierarquiasEntidades_Entidade = 8,

        [EnumMember]
        Curso_NivelEnsino = 16,

        [EnumMember]
        Curso_HistoricoSituacoes_SituacaoEntidade = 32
    }
}