using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CUR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum TipoGestaoDivisaoComponente : short
    {
        [SMCIgnoreValue]
        [EnumMember]
        [Description("")]
        Nenhum = 0,

        [EnumMember]
        [Description("Entrega de comprovante")]
        EntregaComprovante = 1,

        [EnumMember]
        Trabalho = 2,

        [EnumMember]
        Turma = 3,

        [EnumMember]
        [Description("Atividade acadêmica")]
        AtividadeAcademica = 4,

        [EnumMember]
        [Description("Assunto de componente")]
        AssuntoComponente = 5,

        [EnumMember]
        Exame = 6
    }
}