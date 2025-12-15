using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CNC.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum GrupoDocumentoAcademico : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        Diploma = 1,

        [EnumMember]
        [Description("Histórico Escolar")]
        HistoricoEscolar = 2,

        [EnumMember]
        [Description("Declarações genéricas de aluno")]
        DeclaracoesGenericasAluno = 3,

        [EnumMember]
        [Description("Declarações genéricas de professor")]
        DeclaracoesGenericasProfessor = 4,

        [EnumMember]
        [Description("Currículo Escolar")]
        CurriculoEscolar = 5
    }
}