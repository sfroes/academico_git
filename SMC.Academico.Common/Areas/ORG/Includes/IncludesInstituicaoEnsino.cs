using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoEnsino
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Mantenedora = 1,

        [EnumMember]
        Enderecos = 2,

        [EnumMember]
        Telefones = 4,

        [EnumMember]
        EnderecosEletronicos = 8,

        [EnumMember]
        ArquivoLogotipo = 16
    }
}