using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.FIN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoResponsavelFinanceiro : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [Description("Responsável financeiro / Titular")]
        ResponsavelFinanceiroTitular = 1,

        [Description("Convênio / Parceiro")]
        ConvenioParceiro = 2,

        Todos = 3
    }
}