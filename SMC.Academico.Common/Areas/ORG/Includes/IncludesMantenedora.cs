using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesMantenedora
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ArquivoLogotipo = 1,

        [EnumMember]
        Enderecos = 2,

        [EnumMember]
        EnderecosEletronicos = 4,

        [EnumMember]
        Telefones = 8
    }
}
