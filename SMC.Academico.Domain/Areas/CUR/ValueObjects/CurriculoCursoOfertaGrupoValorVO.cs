using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    /// <summary>
    /// Representa as quantidades de carga horária e créditos de um CurriculoCursoOfertaGrupo
    /// </summary>
    public class CurriculoCursoOfertaGrupoValorVO : ISMCMappable
    {
        public long SeqGrupoCurricular { get; set; }

        public int? QuantidadeCreditoObrigatorio { get; set; }

        public int? QuantidadeCreditoOptativo { get; set; }

        public int? QuantidadeHoraAulaObrigatoria { get; set; }

        public int? QuantidadeHoraAulaOptativa { get; set; }

        public int? QuantidadeHoraRelogioObrigatoria { get; set; }

        public int? QuantidadeHoraRelogioOptativa { get; set; }

        public List<CurriculoCursoOfertaGrupoValorVO> SubGrupos { get; set; }
    }
}