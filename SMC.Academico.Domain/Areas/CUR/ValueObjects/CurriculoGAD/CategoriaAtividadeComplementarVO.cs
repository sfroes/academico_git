using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CategoriaAtividadeComplementarVO : ISMCMappable
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public double? LimiteCargaHorariaEmHoraRelogio { get; set; }
        public List<AtividadeComplementarCurriculoVO> Atividades { get; set; }
    }
}
