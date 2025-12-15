using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoRelatorioServico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Posição consolidada serviço por ciclo letivo")]
        PosicaoConsolidadaServicoCicloLetivo = 1,

        [EnumMember]
        [Description("Solicitações com bloqueios")]
        SolicitacoesBloqueio = 2
    }
}
