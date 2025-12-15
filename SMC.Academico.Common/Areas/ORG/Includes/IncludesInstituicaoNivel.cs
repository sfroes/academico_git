using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivel
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoEnsino = 1,

        [EnumMember]
        NivelEnsino = 2,

        [EnumMember]
        Localidades = 4,

        [EnumMember]
        ModelosRelatorio = 8,

        [EnumMember]
        HierarquiasClassificacao = 16,

        [EnumMember]
        Modalidades = 32,

        [EnumMember]
        TiposCurso = 64,

        [EnumMember]
        TiposFormacaoEspecifica = 128,

        [EnumMember]
        TiposOfertaCurso = 256,

        [EnumMember]
        Turnos = 512,
    }
}
