using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesFuncionario
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,
        [EnumMember]
        Pessoa = 1 << 0,
        [EnumMember]
        DadosPessoais = 1 << 1,
        [EnumMember]
        Enderecos_PessoaEndereco_Endereco = 1 << 2,
        [EnumMember]
        DadosPessoais_ArquivoFoto = 1 << 3,
        [EnumMember]
        Telefones = 1 << 4,
        [EnumMember]
        Telefones_Telefone = 1 << 5,
        [EnumMember]
        EnderecosEletronicos_EnderecoEletronico = 1 << 6,
        [EnumMember]
        Pessoa_Filiacao = 1 << 7,
    }
}