using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoNivelModeloRelatorioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region Data Source

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), "BuscarNiveisEnsinoDaInstituicaoSelect")]
        public List<SMCDatasourceItem> InstituicaoNiveisEnsino { get; set; }

        #endregion

        [SMCFilter(true, true)]
        [SMCSelect(nameof(InstituicaoNiveisEnsino))]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqInstituicaoNivel { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public ModeloRelatorio? ModeloRelatorio { get; set; }

        #region Propriedades para ordenação default

        [SMCHidden]
        [SMCInclude("InstituicaoNivel.NivelEnsino")]
        [SMCMapProperty("InstituicaoNivel.NivelEnsino.Descricao")]
        [SMCSortable(true, true, "InstituicaoNivel.NivelEnsino.Descricao")]
        public string DescricaoInstituicaoNivel { get; set; }

        #endregion
    }
}