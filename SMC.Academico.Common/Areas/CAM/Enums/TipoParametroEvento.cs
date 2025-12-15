using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoParametroEvento : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Localidade = 1,

        [EnumMember]
        [Description("Curso oferta localidade")]
        CursoOfertaLocalidade = 2,
        
        [EnumMember]
        [Description("Entidade responsável")]
        EntidadeResponsavel = 3,

        [EnumMember]
        [Description("Tipo aluno")]
        TipoAluno = 4
    }
}