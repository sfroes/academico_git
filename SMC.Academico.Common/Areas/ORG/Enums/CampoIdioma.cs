using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ORG.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum CampoIdioma : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Nome = 1,

        [EnumMember]
        Sigla = 2,

        [EnumMember]
        [Description("Nome resumido")]
        NomeResumido = 3,

        [EnumMember]
        [Description("Nome complementar")]
        NomeComplementar = 4
    }
}