using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CSO.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum CursoTipoFormacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Cadastro_Simples = 1,

        [EnumMember]
        Selecao_Formacao = 2,

        [EnumMember]
        Cadastro_Oferta = 4
    }
}