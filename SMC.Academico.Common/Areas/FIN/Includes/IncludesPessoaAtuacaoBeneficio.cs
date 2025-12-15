using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesPessoaAtuacaoBeneficio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        PessoaAtuacao = 1 << 0,

        [EnumMember]
        PessoaAtuacao_DadosPessoais = 1 << 1,

        [EnumMember]
        PessoaAtuacao_Pessoa = 1 << 2,

        [EnumMember]
        Beneficio = 1 << 3,

        [EnumMember]
        ResponsaveisFinanceiro = 1 << 4,

        [EnumMember]
        ConfiguracaoBeneficio = 1 << 5,

        [EnumMember]
        ResponsaveisFinanceiro_PessoaJuridica = 1 << 6,

        [EnumMember]
        Beneficio_TipoBeneficio = 1 << 7,

        [EnumMember]
        ControlesFinanceiros = 1 << 8,
        
        [EnumMember]
        HistoricoSituacoes = 1 << 9,

        [EnumMember]
        ArquivosAnexo = 1 << 10,

        [EnumMember]
        ArquivosAnexo_ArquivoAnexado = 1 << 11,

        [EnumMember]
        HistoricoVigencias = 1 << 12
    }
}
