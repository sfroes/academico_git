using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesAtoNormativo : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ArquivoAnexado = 1,

        [EnumMember]
        Entidades = 2,

        [EnumMember]
        Entidades_Entidade = 4,

        [EnumMember]
        AssuntoNormativo = 8,

        [EnumMember]
        TipoAtoNormativo = 16
    }
}
