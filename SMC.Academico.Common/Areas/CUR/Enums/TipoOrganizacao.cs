using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoOrganizacao : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Atividade")]
        Atividade = 1,

        [EnumMember]
        [Description("Tema")]
        Tema = 2,

        [EnumMember]
        [Description("Tópico")]
        Topico = 3
    }
}