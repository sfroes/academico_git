using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.ALN.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoRelatorio : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Declaração de disciplinas cursadas")]
        DeclaracaoDisciplinasCursadas = 1,

        [EnumMember]
        [Description("Declaração de matrícula")]
        DeclaracaoMatricula = 2,

        [EnumMember]
        [Description("Identidade estudantil")]
        IdentidadeEstudantil = 3,

        [EnumMember]
        [Description("Histórico escolar")]
        HistoricoEscolar = 4,

        [EnumMember]
        [Description("Histórico escolar interno")]
        HistoricoEscolarInterno = 5,

        [EnumMember]
        [Description("Listagem de alunos")]
        ListagemAssinatura = 6,

        [EnumMember]
        [Description("Declaração genérica")]
        DeclaracaoGenerica = 7,
    }
}
