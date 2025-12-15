using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesInstituicaoNivelTipoVinculoAluno
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 00,

        [EnumMember]
        InstituicaoNivel = 1 << 01,

        [EnumMember]
        InstituicaoNivel_NivelEnsino = 1 << 02,

        [EnumMember]
        TipoVinculoAluno = 1 << 03,

        [EnumMember]
        TiposOrientacao = 1 << 04,

        [EnumMember]
        TiposOrientacao_TipoOrientacao = 1 << 05,

        [EnumMember]
        TiposTermoIntercambio = 1 << 06,

        [EnumMember]
        TiposTermoIntercambio_TipoTermoIntercambio = 1 << 07,

        [EnumMember]
        TiposOrientacao_TiposParticipacao = 1 << 08,

        [EnumMember]
        FormasIngresso = 1 << 09,

        [EnumMember]
        FormasIngresso_FormaIngresso = 1 << 10,

        [EnumMember]
        SituacoesMatricula = 1 << 11,

        [EnumMember]
        SituacoesMatricula_SituacaoMatricula = 1 << 12,

        [EnumMember]
        FormasIngresso_TiposProcessoSeletivo = 1 << 13,

        [EnumMember]
        Servicos = 1 << 14,

        [EnumMember]
        Servicos_Servico = 1 << 15,

        [EnumMember]
        Contratos = 1 << 16,

        [EnumMember]
        Contratos_Servico = 1 << 17,

        [EnumMember]
        TiposOrientacao_InstituicaoNivelTipoTermoIntercambio = 1 << 18,

        [EnumMember]
        TiposOrientacao_InstituicaoNivelTipoTermoIntercambio_TipoTermoIntercambio = 1 << 19,

        [EnumMember]
        FormasIngresso_TiposProcessoSeletivo_TipoProcessoSeletivo = 1 << 20
    }
}