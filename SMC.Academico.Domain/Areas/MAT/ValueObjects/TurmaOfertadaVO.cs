using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class TurmaOfertadaVO : ISMCMappable
    {
        public int AnoTurma { get; set; }
        public int SemestreTurma { get; set; }
        public int SeqTurma { get; set; }
        public int NumeroPeriodo { get; set; }
        public bool PertenceAoCurriculo { get; set; }
        public string NomeDisciplina { get; set; }
        public int? CodGrupoProposicoes { get; set; }
        public string DescricaoConteudo { get; set; }
        public int FormacaoCurriculo { get; set; }
        public string DescricaoTituloFormacaoCurricular { get; set; }
        public List<CargaHorariaVO> CargaHoraria { get; set; }
        public bool DisciplinaOptativa { get; set; }
        public bool PermiteExclusao { get; set; }
        public long SeqSolicitacaomatricula { get; set; }
        public long SeqSolicitacaoMatriculaItem { get; set; }
    }
}
