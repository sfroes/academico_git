using SMC.Framework;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Includes
{
    public enum IncludesClassificacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        ClassificacaoSuperior = 1,

        [EnumMember]
        ClassificacoesFilhas = 2,
    }
}