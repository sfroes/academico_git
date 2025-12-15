using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesEntidade
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        HistoricoSituacoes = 1,

        [EnumMember]
        HistoricoSituacoes_SituacaoEntidade = 2,

        [EnumMember]
        ArquivoLogotipo = 4,

        [EnumMember]
        TipoEntidade = 8,

        [EnumMember]
        Enderecos = 16,

        [EnumMember]
        Telefones = 32,

        [EnumMember]
        EnderecosEletronicos = 64,

        [EnumMember]
        HierarquiasEntidades = 128,

        [EnumMember]
        HierarquiasEntidades_ItemSuperior_Entidade_TipoEntidade = 256,

        [EnumMember]
        Classificacoes = 512,

        [EnumMember]
        Classificacoes_Classificacao = 1024,

        [EnumMember]
        Classificacoes_Classificacao_HierarquiaClassificacao = 2048,

        [EnumMember]
        FormacoesEspecificasEntidade = 4096,

        [EnumMember]
        FormacoesEspecificasEntidade_TipoFormacaoEspecifica = 8192,

        [EnumMember]
        FormacoesEspecificasEntidade_Cursos_GrauAcademico = 16384
    }
}