using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class ElementoHistoricoVO : ISMCMappable
    {
        public string Tipo { get; set; } //enum AtividadeComplementar, Disciplina, Estagio, SituacaoDiscente
        public DisciplinaV2VO Disciplina { get; set; }
        public AtividadeComplementarVO AtividadeComplementar { get; set; }
        public EstagioVO Estagio { get; set; }
        public SituacaoDiscenteVO SituacaoDiscente { get; set; }
    }
}
