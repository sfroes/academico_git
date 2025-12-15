using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCursoOfertaLocalidade : int
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ArquivoLogotipo = 1,

        [EnumMember]
        Classificacoes = 2,

        [EnumMember]
        Classificacoes_Classificacao = 4,

        [EnumMember]
        CursoUnidade = 8,

        [EnumMember]
        Enderecos = 16,

        [EnumMember]
        EnderecosEletronicos = 32,

        [EnumMember]
        FormacoesEspecificas = 64,

        [EnumMember]
        FormacoesEspecificasEntidade = 128,

        [EnumMember]
        FormacoesEspecificasEntidade_TipoFormacaoEspecifica = 256,

        [EnumMember]
        HierarquiasEntidades = 512,

        [EnumMember]
        Telefones = 1024,

        [EnumMember]
        Turnos = 2048,

        [EnumMember]
        HierarquiasEntidades_ItemSuperior = 4096,

        [EnumMember]
        CursoOferta_Curso_NivelEnsino = 8192,

        [EnumMember]
        HistoricoSituacoes_SituacaoEntidade = 16384,

        [EnumMember]
        Modalidade = 32768,

        [EnumMember]
        HierarquiasEntidades_ItemSuperior_Entidade = 65536,

        [EnumMember]
        CursoOferta = 131072,

        [EnumMember]
        Turnos_Turno = 262144,

        [EnumMember]
        TipoEntidade = 524288,

        [EnumMember]
        FormacoesEspecificas_FormacaoEspecifica_TipoFormacaoEspecifica = 1048576,
    }
}