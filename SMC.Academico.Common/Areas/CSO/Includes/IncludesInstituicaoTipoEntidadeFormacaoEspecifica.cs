using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoTipoEntidadeFormacaoEspecifica : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoTipoEntidade = 1,

        [EnumMember]
        TipoFormacaoEspecifica = 2,

        [EnumMember]
        TipoFormacaoEspecificaPai = 4,

        [EnumMember]
        TipoFormacaoEspecificaPai_TipoFormacaoEspecifica = 8,

        [EnumMember]
        TiposFormacaoEspecificasFilhas = 16,
    }
}