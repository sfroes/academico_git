using SMC.Academico.Common.Constants;
using SMC.Framework;
using System;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.PES.Includes
{
    [Flags]
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum IncludesMensagemPessoaAtuacao
    {
        [SMCIgnoreValue]
        [EnumMember]
        Nenhum = 0,

        [EnumMember]
        Mensagem_ArquivoAnexado = 1,

        [EnumMember]
        Mensagem_TipoMensagem = 2,

        [EnumMember]
        Mensagem = 4
    }
} 