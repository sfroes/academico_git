using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Includes
{
    /// <summary>
    /// Includes para entidade PessoaAtuacao
    /// </summary>
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesPessoaAtuacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Pessoa = 2,

        [EnumMember]
        DadosPessoais = 4,

        [EnumMember]
        Documentos = 8,

        [EnumMember]
        Formularios = 16,

        [EnumMember]
        ReferenciasFamiliar = 32,

        [EnumMember]
        Enderecos = 64,

        [EnumMember]
        EnderecosEletronicos = 128,

        [EnumMember]
        Telefones = 256,

        [EnumMember]
        EnderecosEletronicos_EnderecoEletronico = 512,

        [EnumMember]
        Telefones_Telefone = 1024,

        [EnumMember]
        Bloqueios = 2048
    }
}