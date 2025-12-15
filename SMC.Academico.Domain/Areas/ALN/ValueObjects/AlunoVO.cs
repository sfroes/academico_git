using SMC.Academico.Domain.Areas.PES.ValueObjects;
using System;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AlunoVO : InformacoesPessoaVO
    {
        public long NumeroRegistroAcademico { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public DateTime? DataExclusao { get; set; }

        public int? CodigoAlunoMigracao { get; set; }
    }
}