using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class DadosAtoNormativoVO : ISMCMappable
    {
        public string Descricao { get; set; }
        public string Tipo { get; set; } 
        public string Numero { get; set; }
        public DateTime? Data { get; set; }
        public string VeiculoPublicacao { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public int? SecaoPublicacao { get; set; }
        public int? PaginaPublicacao { get; set; }
        public int? NumeroDOU { get; set; }
    }
}
