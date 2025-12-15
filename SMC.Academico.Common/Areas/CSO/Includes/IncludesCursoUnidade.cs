using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesCursoUnidade : short
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
        Curso = 8,

        [EnumMember]
        Curso_CursosOferta = 16,

        [EnumMember]
        Curso_NivelEnsino = 32,

        [EnumMember]
        CursosOfertaLocalidade_Turnos_Turno = 64,

        [EnumMember]
        Enderecos = 128,

        [EnumMember]
        EnderecosEletronicos = 256,

        [EnumMember]
        HierarquiasEntidades = 512,

        [EnumMember]
        HierarquiasEntidades_ItemSuperior_Entidade = 1024,

        [EnumMember]
        Telefones = 2048,
    }
}