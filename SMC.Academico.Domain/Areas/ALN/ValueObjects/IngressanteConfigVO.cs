using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    /// <summary>
    /// POCO para ajudar na criação de um ingressante.
    /// </summary>
    public class IngressanteConfigVO
    {
        public short? QuantidadeOfertaCampanhaIngresso { get; set; }

        public string DescricaoVinculo { get; set; }

        public bool ExigeCurso { get; set; }

        public string DescricaoProcesso { get; set; }

        public bool ExigeParceriaIntercambioIngresso { get; set; }

        public List<IngressanteTiposTermoIntercambioConfigVO> TiposTermoIntercambio { get; set; }

        public long? SeqCurso { get; set; }
    }
}