using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoDeclaracaoDisciplinaCursada : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Todos componentes curriculares")]
        TodosComponentesCurriculares = 1,

        [EnumMember]
        [Description("Somente disciplina")]
        SomenteDisciplina = 2,
    }
}
