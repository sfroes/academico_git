using SMC.Academico.ServiceContract.Areas.PES.Data;
using System;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{

    public class AlunoData : PessoaAtuacaoData
    {

        public long NumeroRegistroAcademico { get; set; }


        public long SeqTipoVinculoAluno { get; set; }


        public DateTime? DataExclusao { get; set; }


        public int? CodigoAlunoMigracao { get; set; }
    }
}