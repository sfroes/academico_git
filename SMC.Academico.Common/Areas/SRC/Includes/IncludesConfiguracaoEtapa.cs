using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesConfiguracaoEtapa
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ConfiguracoesPagina = 1,

        [EnumMember]
        ConfiguracoesPagina_TextosSecao = 2,

        [EnumMember]
        ConfiguracoesPagina_Arquivos = 4,

        [EnumMember]
        DocumentosRequeridos = 8,

        [EnumMember]
        GruposDocumentoRequerido = 16,

        [EnumMember]
        GruposDocumentoRequerido_Itens = 32,

        [EnumMember]
        ProcessoEtapa = 64,

        [EnumMember]
        ConfiguracaoProcesso_Processo_Servico_TipoServico = 128,

        [EnumMember]
        ConfiguracoesBloqueio = 256,

        [EnumMember]
        ConfiguracaoProcesso_Processo_Servico = 512
    }
}