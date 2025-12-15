using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaListarGrupoCursoVO : ISMCMappable
    {
        public string DescricaoCursoLocalidadeTurno { get; set; }

        public List<TurmaListarVO> Turmas { get; set; }
    }
}
