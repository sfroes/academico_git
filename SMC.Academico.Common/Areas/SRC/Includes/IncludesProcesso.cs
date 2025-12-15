using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesProcesso
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        CicloLetivo = 1 << 1,

        [EnumMember]
        Servico = 1 << 2,

        [EnumMember]
        Servico_TipoServico = 1 << 3,

        [EnumMember]
        UnidadesResponsaveis = 1 << 4,

        [EnumMember]
        UnidadesResponsaveis_EntidadeResponsavel = 1 << 5,

        [EnumMember]
        Etapas = 1 << 6,

        [EnumMember]
        Etapas_Escalonamentos = 1 << 7,

        [EnumMember]
        Configuracoes = 1 << 8,

        [EnumMember]
        GruposEscalonamento = 1 << 9,

        [EnumMember]
        Etapas_Escalonamentos_GruposEscalonamento = 1 << 10,

        [EnumMember]
        Etapas_Escalonamentos_GruposEscalonamento_Parcelas = 1 << 11,

        [EnumMember]
        Etapas_ConfiguracoesNotificacao = 1 << 12,

        [EnumMember]
        Etapas_ConfiguracoesNotificacao_TipoNotificacao = 1 << 13,

        [EnumMember]
        Etapas_ConfiguracoesNotificacao_ProcessoUnidadeResponsavel = 1 << 14,

        [EnumMember]
        Etapas_ConfiguracoesNotificacao_ParametrosEnvioNotificacao = 1 << 15,

        [EnumMember]
        Etapas_Escalonamentos_GruposEscalonamento_GrupoEscalonamento = 1 << 16,

        [EnumMember]
        Etapas_ConfiguracoesNotificacao_ProcessoUnidadeResponsavel_EntidadeResponsavel = 1 << 17,

        [EnumMember]
        Configuracoes_NiveisEnsino = 1 << 18,

        [EnumMember]
        Configuracoes_Cursos = 1 << 19,

        [EnumMember]
        Configuracoes_Cursos_CursoOfertaLocalidadeTurno = 1 << 20,

        [EnumMember]
        Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade = 1 << 21,

        [EnumMember]
        Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades = 1 << 22,

        [EnumMember]
        Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades_ItemSuperior = 1 << 23,      
                     
        [EnumMember]
        Configuracoes_TiposVinculoAluno = 1 << 24,

        [EnumMember]
        Etapas_ConfiguracoesEtapa_ConfiguracaoProcesso = 1 << 25,

        [EnumMember]
        Servico_TiposNotificacao = 1 << 26
    }
}