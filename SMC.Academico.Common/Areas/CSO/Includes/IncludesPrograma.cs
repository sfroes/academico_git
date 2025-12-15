using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesPrograma 
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ArquivoLogotipo = 1 << 1,

        [EnumMember]
        Classificacoes = 1 << 2,

        [EnumMember]
        Classificacoes_Classificacao = 1 << 3,

        [EnumMember]
        DadosOutrosIdiomas = 1 << 4,

        [EnumMember]
        Enderecos = 1 << 5,

        [EnumMember]
        EnderecosEletronicos = 1 << 6,

        [EnumMember]
        FormacoesEspecificasEntidade = 1 << 7,

        [EnumMember]
        FormacoesEspecificasEntidade_TipoFormacaoEspecifica = 1 << 8,

        [EnumMember]
        HierarquiasEntidades = 1 << 9,

        [EnumMember]
        HistoricoNotas = 1 << 10,

        [EnumMember]
        HistoricoSituacoes = 1 << 11,

        [EnumMember]
        HistoricoSituacoes_SituacaoEntidade = 1 << 12,

        [EnumMember]
        Telefones = 1 << 13,

        [EnumMember]
        RegimeLetivo = 1 << 14,

        [EnumMember]
        HierarquiasEntidades_ItemSuperior_Entidade = 1 << 15,

        [EnumMember]
        TiposAutorizacaoBdp = 1 << 16
    }
}