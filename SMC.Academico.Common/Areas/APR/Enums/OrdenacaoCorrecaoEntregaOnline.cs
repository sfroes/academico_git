using SMC.Academico.Common.Constants;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [DataContract(Namespace = NAMESPACES.MODEL)]
    public enum OrdenacaoCorrecaoEntregaOnline : short
    {
        [EnumMember]
        [Description("Data de entrega")]
        DataEntrega = 0,

        [EnumMember]
        [Description("Nome do aluno responsável pela entrega")]
        NomeAlunoResponsavel = 1
    }
}