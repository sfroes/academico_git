using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoMobilidade : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Ingresso em nossa instituição")]
        IngressoEmNossaInstituicao = 1,

        [EnumMember]
        [Description("Saída para outra instituição")]
        SaidaParaOutraInstituicao = 2
    }
}