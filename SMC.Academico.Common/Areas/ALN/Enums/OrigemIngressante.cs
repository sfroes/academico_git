using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum OrigemIngressante : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Cadastrado manualmente")]
        Manual = 1,

        [EnumMember]
        [Description("Convocado")]
        Convocacao = 2,

        [EnumMember]
        [Description("Importação via planilha")]
        ImportacaoPlanilha = 3,

        [EnumMember]
        [Description("Selecionado no GPI")]
        SelecionadoGPI = 4
    }
}