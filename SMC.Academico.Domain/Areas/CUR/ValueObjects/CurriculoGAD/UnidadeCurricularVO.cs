using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class UnidadeCurricularVO : ISMCMappable
    {
        public string Tipo { get; set; } 
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int CargaHorariaEmHoraAula { get; set; }
        public double CargaHorariaEmHoraRelogio { get; set; }
        public EmentaVO Ementa { get; set; }
        public string Fase { get; set; }
        public List<UnidadeCurricularEquivalenteVO> Equivalencias { get; set; }
        public List<string> PreRequisitos { get; set; }
        public List<EtiquetaVO> Etiquetas { get; set; }
        public List<string> Areas { get; set; }
    }
}
