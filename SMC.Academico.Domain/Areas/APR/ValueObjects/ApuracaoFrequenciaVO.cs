using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class ApuracaoFrequenciaVO : ISMCMappable
    {
        public bool AlunoComHistorico { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string NomeAluno { get; set; }

        public int NumeroFaltas { get; set; }

        public int FaltasAtuais { get; set; }

        public long Seq { get; set; }

        public long SeqAlunoHistoricoCicloLetivo { get; set; }

        public long SeqAula { get; set; }

        public bool AlunoFormado { get; set; }
    }
}
