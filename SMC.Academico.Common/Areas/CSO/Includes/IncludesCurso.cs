using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCurso : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ArquivoLogotipo = 1,

        [EnumMember]
        Enderecos = 2,

        [EnumMember]
        EnderecosEletronicos = 4,

        [EnumMember]
        HierarquiasEntidades = 8,

        [EnumMember]
        Telefones = 16,

        [EnumMember]
        HistoricoSituacoes_SituacaoEntidade = 32,

        [EnumMember]
        NivelEnsino = 64,

        [EnumMember]
        HierarquiasEntidades_ItemSuperior_Entidade = 128,

        [EnumMember]
        CursosFormacaoEspecifica = 256,

        [EnumMember]
        FormacoesEspecificasEntidade = 512,

        [EnumMember]
        CursosOferta = 1024,

        [EnumMember]
        HierarquiasEntidades_ItemSuperior = 2048,

        [EnumMember]
        Classificacoes_Classificacao = 4096,

        [EnumMember]
        CursosFormacaoEspecifica_FormacaoEspecifica = 8192,
    }
}