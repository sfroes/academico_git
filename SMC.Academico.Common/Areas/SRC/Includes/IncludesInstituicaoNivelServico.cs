using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelServico
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoNivelTipoVinculoAluno = 1,

        [EnumMember]
        Servico = 2,

        [EnumMember]
        InstituicaoNivelTipoVinculoAluno_InstituicaoNivel = 4,

        [EnumMember]
        InstituicaoNivelTipoVinculoAluno_InstituicaoNivel_NivelEnsino = 8,

        [EnumMember]
        Servico_TipoServico = 16,

        [EnumMember]
        InstituicaoNivelTipoVinculoAluno_TipoVinculoAluno = 32
    }
}