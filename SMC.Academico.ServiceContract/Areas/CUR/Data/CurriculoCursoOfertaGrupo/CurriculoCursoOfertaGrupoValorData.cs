using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    /// <summary>
    /// Representa as quantidades de carga horária e créditos de um CurriculoCursoOfertaGrupo
    /// </summary>
    public class CurriculoCursoOfertaGrupoValorData : ISMCMappable
    {
        public int? QuantidadeCreditoObrigatorio { get; set; }

        public int? QuantidadeCreditoOptativo { get; set; }

        public int? QuantidadeHoraAulaObrigatoria { get; set; }

        public int? QuantidadeHoraAulaOptativa { get; set; }

        public int? QuantidadeHoraRelogioObrigatoria { get; set; }

        public int? QuantidadeHoraRelogioOptativa { get; set; }
    }
}