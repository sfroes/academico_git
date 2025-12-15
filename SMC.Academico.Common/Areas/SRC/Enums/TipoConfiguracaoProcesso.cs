using SMC.Academico.Common.Constants;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.SRC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoConfiguracaoProcesso : short
    {
        [EnumMember]
        [Description("Nível de Ensino")]
        NivelEnsino = 1,

        [EnumMember]
        [Description("Oferta de Curso por Localidade")]
        OfertaCursoLocalidade = 2
    }
}
