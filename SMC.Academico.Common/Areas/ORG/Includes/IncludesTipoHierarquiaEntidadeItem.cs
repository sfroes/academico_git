using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesTipoHierarquiaEntidadeItem
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoEntidade = 1,

        [EnumMember]
        TipoHierarquiaEntidade = 2,

        [EnumMember]
        ItensFilhos = 4,

        [EnumMember]
        ItemSuperior = 8,
    }
}
