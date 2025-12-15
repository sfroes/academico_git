using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class CurriculoCursoOfertaListaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        /// <summary>
        /// Somatório da QuantidadeHoraAulaObrigatoria dos GruposCurriculares desta oferta
        /// </summary>
        public int QuantidadeHoraAulaObrigatoriaTotal { get; set; }

        /// <summary>
        /// Somatório da QuantidadeCreditoObrigatorio dos GruposCurriculares desta oferta
        /// </summary>
        public int QuantidadeCreditoObrigatorioTotal { get; set; }

        /// <summary>
        /// Somatório da QuantidadeCreditoOptativo dos GruposCurriculares desta oferta
        /// </summary>
        public int QuantidadeCreditoOptativoTotal { get; set; }

        /// <summary>
        /// Somatório da QuantidadeHoraAulaOptativa dos GruposCurriculares desta oferta
        /// </summary>
        public int QuantidadeHoraAulaOptativaTotal { get; set; }                        

        public IEnumerable<CurriculoCursoOfertaGrupoListaVO> GruposCurriculares { get; set; }
    }
}