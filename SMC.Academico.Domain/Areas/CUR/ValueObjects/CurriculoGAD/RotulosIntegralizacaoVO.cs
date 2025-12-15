using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class RotulosIntegralizacaoVO : ISMCMappable
    {
        public string TipoUnidadeCurricular { get; set; } 
        public List<string> Etiquetas { get; set; }
        public string Codigo { get; set; }
        public LimitesCargaHorariaVO LimitesCargaHoraria { get; set; }
    }
}
