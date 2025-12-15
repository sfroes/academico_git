using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoTipoEntidadeVinculoColaborador
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoTipoEntidade = 1,

        [EnumMember]
        TipoVinculoColaborador = 2,

        [EnumMember]
        InstituicaoTipoEntidade_TipoEntidade = 4,
    }
}