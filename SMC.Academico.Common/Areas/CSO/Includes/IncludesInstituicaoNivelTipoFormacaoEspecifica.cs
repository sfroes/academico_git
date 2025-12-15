using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelTipoFormacaoEspecifica : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        TipoFormacaoEspecifica = 1,

        [EnumMember]
        InstituicaoNivel = 2,

        [EnumMember]
        InstituicaoNivel_NivelEnsino = 4
    }
}