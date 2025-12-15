using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoTipoEntidade
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoEnsino = 1,

        [EnumMember]
        TipoEntidade = 2,

        [EnumMember]
        TiposEndereco = 4,

        [EnumMember]
        TiposEnderecoEletronico = 8,

        [EnumMember]
        Situacoes = 16,

        [EnumMember]
        Situacoes_SituacaoEntidade = 32,

        [EnumMember]
        TiposTelefone = 64,
    }
}
