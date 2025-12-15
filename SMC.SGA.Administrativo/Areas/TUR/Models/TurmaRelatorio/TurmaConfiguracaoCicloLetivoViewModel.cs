using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaConfiguracaoCicloLetivoViewModel : ISMCMappable
    {
        public long? SeqTurmaConfiguracaoComponente { get; set; }

        public long? SeqConfiguracaoComponente { get; set; }

        public long? SeqOfertaCurso { get; set; }

        public long? SeqTurma { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string Turma { get; set; }

        public string TipoTurma { get; set; }

        public short Vagas { get; set; }

        public string ConfiguracaoComponente { get; set; }

        public string ComponenteSubstituto { get; set; }

        public List<DivisaoTurmaCicloLetivoViewModel> DivisoesTurma { get; set; }

        public short? AlunosMatriculados { get; set; }

        public List<ColaboradorTurmaConfiguracaoCicloLetivoViewModel> Colaboradores { get; set; }

        public bool? OcultarColaborador { get; set; }
    }
}