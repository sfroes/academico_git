using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesHierarquiaEntidadeItem
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Entidade = 1,

        [EnumMember]
        Entidade_TipoEntidade = 2,

        [EnumMember]
        HierarquiaEntidade = 4,

        [EnumMember]
        ItemSuperior = 8,

        [EnumMember]
        ItensFilhos = 16,

        [EnumMember]
        ItensFilhos_Entidade = 32,

        [EnumMember]
        TipoHierarquiaEntidadeItem = 64,

        [EnumMember]
        Entidade_HistoricoSituacoes_SituacaoEntidade = 128,

        [EnumMember]
        TipoHierarquiaEntidadeItem_ItensFilhos_TipoEntidade = 256,
    }
}