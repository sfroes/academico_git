using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesServico
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        InstituicaoEnsino = 1 << 1,

        [EnumMember]
        TipoServico = 1 << 2,

        [EnumMember]
        Justificativas = 1 << 3,

        [EnumMember]
        SituacoesAluno = 1 << 4,

        [EnumMember]
        SituacoesAluno_SituacaoMatricula = 1 << 5,

        [EnumMember]
        SituacoesIngressante = 1 << 6,

        [EnumMember]
        Processos = 1 << 7,

        [EnumMember]
        TermosAdesao = 1 << 8,

        [EnumMember]
        InstituicaoNivelServicos = 1 << 9,

        [EnumMember]
        InstituicaoNivelServicos_InstituicaoNivelTipoVinculoAluno = 1 << 10,

        [EnumMember]
        InstituicaoNivelServicos_InstituicaoNivelTipoVinculoAluno_TipoVinculoAluno = 1 << 11,

        [EnumMember]
        InstituicaoNivelServicos_InstituicaoNivelTipoVinculoAluno_InstituicaoNivel = 1 << 12,

        [EnumMember]
        InstituicaoNivelServicos_InstituicaoNivelTipoVinculoAluno_InstituicaoNivel_NivelEnsino = 1 << 13,

        [EnumMember]
        RestricoesSolicitacaoSimultanea = 1 << 14,

        [EnumMember]
        RestricoesSolicitacaoSimultanea_ServicoRestricao = 1 << 15,

        [EnumMember]
        MotivosBloqueioParcela_MotivoBloqueio = 1 << 16,

        [EnumMember]
        TiposNotificacao = 1 << 17,

        [EnumMember]
        Taxas = 1 << 18,

        [EnumMember]
        ParametrosEmissaoTaxa = 1 << 19,

        [EnumMember]
        TiposDocumento = 1 << 20
    }
}
