using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class AtoRegulatorioBaseVO : ISMCMappable
    {
        public string Tipo { get; set; } //enum Parecer, Resolucao, Decreto, Portaria, Despacho, Deliberação, Lei Federal, Lei Estadual, Lei Municipal
        public string Numero { get; set; }
        public DateTimeOffset? Data { get; set; }
        public string VeiculoPublicacao { get; set; }
        public DateTimeOffset? DataPublicacao { get; set; }
        public int? SecaoPublicacao { get; set; }
        public int? PaginaPublicacao { get; set; }
        public int? NumeroDOU { get; set; }
    }
}
