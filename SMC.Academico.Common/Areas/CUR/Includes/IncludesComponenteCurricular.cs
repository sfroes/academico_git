using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesComponenteCurricular
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoEnsino = 1,

        [EnumMember]
        TipoComponente = 2,

        [EnumMember]
        OrgaosReguladores = 4,

        [EnumMember]
        Ementas = 8,

        [EnumMember]
        EntidadesResponsaveis = 16,

        [EnumMember]
        NiveisEnsino = 32,

        [EnumMember]
        Organizacoes = 64,

        [EnumMember]
        Configuracoes = 128,

        [EnumMember]
        EntidadesResponsaveis_Entidade = 256,

        [EnumMember]
        NiveisEnsino_NivelEnsino = 512,

        [EnumMember]
        Configuracoes_DivisoesComponente = 1024,

        [EnumMember]
        Configuracoes_DivisoesMatrizCurricularComponente = 2048,

        [EnumMember]
        TipoComponente_TiposDivisao = 8192,

        [EnumMember]
        TipoComponente_TiposDivisao_Modalidade = 16384,

        [EnumMember]
        Configuracoes_DivisoesMatrizCurricularComponente_ComponentesCurricularSubstitutos = 32768,
    }
}