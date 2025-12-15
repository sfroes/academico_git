using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesTermoIntercambio
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Arquivos = 1 << 0,

        [EnumMember]
        TiposMobilidade = 1 << 1,

        [EnumMember]
        Vigencias = 1 << 2,

        [EnumMember]
        ParceriaIntercambioInstituicaoExterna = 1 << 3,

        [EnumMember]
        ParceriaIntercambioTipoTermo = 1 << 4,

        [EnumMember]
        Arquivos_ArquivoAnexado = 1 << 5,

        [EnumMember]
        ParceriaIntercambioInstituicaoExterna_InstituicaoExterna = 1 << 6,

        [EnumMember]
        TiposMobilidade_Pessoas = 1 << 7,

        [EnumMember]
        PessoasAtuacao = 1 << 8,

        [EnumMember]
        PessoasAtuacao_Orientacao_OrientacoesColaborador_Colaborador_DadosPessoais = 1 << 9,

        [EnumMember]
        NivelEnsino = 1 << 10,

        [EnumMember]
        ParceriaIntercambioInstituicaoExterna_ParceriaIntercambio = 1 << 11,

        [EnumMember]
        ParceriaIntercambioTipoTermo_TipoTermoIntercambio = 1 << 12
    }
}
