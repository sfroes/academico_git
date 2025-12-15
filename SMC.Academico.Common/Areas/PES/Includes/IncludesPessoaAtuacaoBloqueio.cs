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
    public enum IncludesPessoaAtuacaoBloqueio
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        PessoaAtuacao = 2,

        [EnumMember]
        PessoaAtuacao_Pessoa = 4,

        [EnumMember]
        PessoaAtuacao_DadosPessoais = 8,

        [EnumMember]
        MotivoBloqueio_TipoBloqueio = 16,

        [EnumMember]
        Comprovantes_ArquivoAnexado = 32,

        [EnumMember]
        Comprovantes = 64,

        [EnumMember]
        Itens = 128,

        [EnumMember]
        MotivoBloqueio = 256,

        [EnumMember]
        SolicitacaoServico = 512,

        [EnumMember]
        SolicitacaoServico_ConfiguracaoProcesso_Processo_CicloLetivo = 1024
    }
}