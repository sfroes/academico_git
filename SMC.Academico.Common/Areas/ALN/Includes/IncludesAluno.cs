using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesAluno : int
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 1 << 00,

        [EnumMember]
        DadosPessoais = 1 << 01,

        [EnumMember]
        DadosPessoais_ArquivoFoto = 1 << 02,

        [EnumMember]
        Enderecos_PessoaEndereco_Endereco = 1 << 03,

        [EnumMember]
        Enderecos = 1 << 04,

        [EnumMember]
        EnderecosEletronicos = 1 << 05,

        [EnumMember]
        EnderecosEletronicos_EnderecoEletronico = 1 << 06,

        [EnumMember]
        Pessoa_Filiacao = 1 << 07,

        [EnumMember]
        Telefones_Telefone = 1 << 08,

        [EnumMember]
        Telefones = 1 << 09,

        [EnumMember]
        Pessoa = 1 << 10
    }
}