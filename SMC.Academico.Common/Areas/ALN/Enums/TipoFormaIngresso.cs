using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoFormaIngresso : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Processo seletivo interno")]
        ProcessoSeletivoInterno = 1,

        [EnumMember]
        [Description("Processo seletivo externo")]
        ProcessoSeletivoExterno = 2,

        [EnumMember]
        [Description("Avaliação do colegiado")]
        AvaliacaoColegiado = 3

    }
}