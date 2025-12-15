using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesProcessoEtapa
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0 << 0,

        [EnumMember]
        Escalonamentos = 1 << 1,

        [EnumMember]
        Processo_Servico_TipoServico = 1 << 2,

        [EnumMember]
        ConfiguracoesEtapa = 1 << 3,

        [EnumMember]
        ConfiguracoesEtapa_DocumentosRequeridos = 1 << 4,

        [EnumMember]
        ConfiguracoesPagina = 1 << 5,

        [EnumMember]
        ConfiguracoesEtapa_ConfiguracoesPagina = 1 << 6,

        [EnumMember]
        SituacoesItemMatricula = 1 << 7,

        [EnumMember]
        Processo = 1 << 8,

        [EnumMember]
        Processo_CicloLetivo = 1 << 9,

        [EnumMember]
        Processo_CicloLetivo_TiposEvento = 1 << 10,

        [EnumMember]
        Processo_CicloLetivo_TiposEvento_InstituicaoTipoEvento = 1 << 11,

        [EnumMember]
        Processo_CicloLetivo_TiposEvento_Parametros = 1 << 12,

        [EnumMember]
        Processo_CicloLetivo_TiposEvento_Parametros_InstituicaoTipoEventoParametro = 1 << 13,

        [EnumMember]
        Processo_CicloLetivo_TiposEvento_EventosLetivos = 1 << 14,

        [EnumMember]
        Processo_CicloLetivo_TiposEvento_EventosLetivos_NiveisEnsino = 1 << 15,

        [EnumMember]
        Processo_Configuracoes = 1 << 16,

        [EnumMember]
        Processo_Configuracoes_NiveisEnsino = 1 << 17,

        [EnumMember]
        Processo_CicloLetivo_TiposEvento_EventosLetivos_ParametrosEntidade = 1 << 18,

        [EnumMember]
        Processo_UnidadesResponsaveis = 1 << 19,

        [EnumMember]
        Processo_Configuracoes_Cursos = 1 << 20,

        [EnumMember]
        Processo_Configuracoes_Cursos_CursoOfertaLocalidadeTurno = 1 << 21,

        [EnumMember]
        Processo_Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade = 1 << 22,

        [EnumMember]
        Processo_Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades = 1 << 23,

        [EnumMember]
        Processo_Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades_ItemSuperior = 1 << 24,

        [EnumMember]
        Escalonamentos_GruposEscalonamento = 1 << 25,

        [EnumMember]
        FiltrosDados = 1 << 26
    }
}