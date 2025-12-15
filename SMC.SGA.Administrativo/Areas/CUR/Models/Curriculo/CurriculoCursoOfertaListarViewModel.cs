using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class CurriculoCursoOfertaListarViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
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
        /// Somatório da QuantidadeHoraAulaOptativa dos GruposCurriculares desta oferta
        /// </summary>
        public int QuantidadeHoraAulaOptativaTotal { get; set; }

        /// <summary>
        /// Somatório da QuantidadeCreditoOptativa dos GruposCurriculares desta oferta
        /// </summary>
        public int QuantidadeCreditoOptativoTotal { get; set; }

        public List<CurriculoCursoOfertaGrupoListarViewModel> GruposCurriculares { get; set; }
    }
}