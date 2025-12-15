using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class EstagioVO : ISMCMappable
    {
        public ConcedenteEstagioVO Concedente { get; set; }
        public string Codigo { get; set; }
        public DateTimeOffset? DataInicio { get; set; }
        public DateTimeOffset? DataFim { get; set; }
        public string Descricao { get; set; }
        public List<HoraRelogioComEtiquetaVO> CargaHorariaEmHoraRelogioComEtiqueta { get; set; } // Pode ser associada a uma etiqueta definida no Currículo Escolar permitindo identificar a natureza da carga horária dentro do Currículo do Curso
        public List<DocenteVO> Docentes { get; set; }

        [Obsolete("OBSOLETO. Utilizar cargaHorariaEmHoraRelogioComEtiqueta")]
        public double? CargaHorariaEmHoraRelogio { get; set; }
    }
}