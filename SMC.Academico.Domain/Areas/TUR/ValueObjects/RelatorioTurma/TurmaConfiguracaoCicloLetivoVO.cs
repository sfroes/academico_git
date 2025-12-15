using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.TUR.ValueObjects
{
    public class TurmaConfiguracaoCicloLetivoVO : ISMCMappable
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

        public long? SeqComponenteCurricularAssunto { get; set; }

        public string ComponenteSubstituto { get; set; }

        public List<DivisaoTurmaCicloLetivoVO> DivisoesTurma { get; set; }

        public short? AlunosMatriculados { get; set; }

        public List<ColaboradorTurmaConfiguracaoCicloLetivoVO> Colaboradores { get; set; }

        public bool? OcultarColaborador { get; set; }
    }
}