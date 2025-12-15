using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesParceriaIntercambio
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        InstituicaoEnsino = 1,

        [EnumMember]
        Arquivos = 2,

        [EnumMember]
        InstituicoesExternas = 4,

        [EnumMember]
        TiposTermo = 8,

        [EnumMember]
        Vigencias = 16,

        [EnumMember]
        Arquivos_ArquivoAnexado = 32,

        [EnumMember]
        InstituicoesExternas_InstituicaoExterna = 64,

        [EnumMember]
        TiposTermo_TipoTermoIntercambio = 128

    }
}
