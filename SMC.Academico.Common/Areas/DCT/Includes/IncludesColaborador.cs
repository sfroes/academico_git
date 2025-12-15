using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.DCT.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesColaborador
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Pessoa = 1,

        [EnumMember]
        DadosPessoais = 2,

        [EnumMember]
        Enderecos_PessoaEndereco_Endereco = 4,

        [EnumMember]
        DadosPessoais_ArquivoFoto = 8,

        [EnumMember]
        Telefones = 16,

        [EnumMember]
        Telefones_Telefone = 32,

        [EnumMember]
        EnderecosEletronicos_EnderecoEletronico = 64,

        [EnumMember]
        Pessoa_Filiacao = 128,

        [EnumMember]
        InstituicoesExternas_InstituicaoExterna = 256,

        [EnumMember]
        Vinculos_EntidadeVinculo = 512,

        [EnumMember]
        Vinculos_TipoVinculoColaborador = 1024,
    }
}