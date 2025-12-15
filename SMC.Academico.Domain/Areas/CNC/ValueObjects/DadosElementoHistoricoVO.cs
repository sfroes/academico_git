using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class DadosElementoHistoricoVO : ISMCMappable
    {
        public List<ElementoHistoricoVO> ElementosHistorico { get; set; }

        public double CargaHorariaCurso { get; set; }

        public double CargaHorariaCursoIntegralizada { get; set; }

        public string CodigoCurriculo { get; set; }
    }
}
