using SMC.Academico.Common.Constants;
using SMC.Framework;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.CAM.Enums
{ 
    public enum ImpedimentoLiberacaoMatricula : short
    {
        [SMCIgnoreValue]
        [EnumMember] 
        Nenhum = 0,

        [EnumMember]
        [Description("Formação específica não associada ")]
        FormacaoEspecificaNaoAssociada = 1,

        [EnumMember]
        [Description("Grupo de escalonamento com item expirado ")]
        GrupoEscalonamentoComItemExpirado = 2,

        [EnumMember]
        [Description("Condição de obrigatoriedade não cadastrada")]
        CondicaoObrigatoriedadeNaoCadastrada = 3            
    }
} 