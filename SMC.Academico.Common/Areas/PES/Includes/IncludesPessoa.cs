using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Includes
{
    /// <summary>
    /// Includes para entidade Pessoa
    /// </summary>
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesPessoa
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        DadosPessoais = 1 << 00,

        [EnumMember]
        Enderecos = 1 << 02,

        [EnumMember]
        Enderecos_Endereco = 1 << 03,

        [EnumMember]
        Atuacoes_Enderecos = 1 << 04,

        [EnumMember]
        Atuacoes_Enderecos_PessoaEndereco_Endereco = 1 << 05,

        [EnumMember]
        Atuacoes = 1 << 06,

        [EnumMember]
        Telefones = 1 << 07,

        [EnumMember]
        Telefones_Telefone = 1 << 08,

        [EnumMember]
        Atuacoes_Telefones = 1 << 09,

        [EnumMember]
        EnderecosEletronicos_EnderecoEletronico = 1 << 10,

        [EnumMember]
        DadosPessoais_ArquivoFoto = 1 << 11,

        [EnumMember]
        Filiacao = 1 << 12,
    }
}