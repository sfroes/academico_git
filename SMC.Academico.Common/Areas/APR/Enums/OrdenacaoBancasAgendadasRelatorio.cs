using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SMC.Academico.Common.Areas.APR.Enums
{
    [Flags]
    public enum OrdenacaoBancasAgendadasRelatorio
    {
        [EnumMember]
        [Description("Nome do aluno")]
        NomeAluno = 1 << 0,

        [EnumMember]
        [Description("Data da defesa")]
        DataDefesa = 1 << 1,

        [EnumMember]
        [Description("Nome do orientador")]
        NomeOrientador = 1 << 2,
    }
}