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
    public class InstituicaoModeloRelatorioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        #region Data Source

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoEnsinoService), nameof(IInstituicaoEnsinoService.BuscarInstituicoesEnsinoSelect))]
        public List<SMCDatasourceItem> InstituicoesEnsino { get; set; }

        #endregion

        [SMCFilter(true, true)]
        [SMCSelect(nameof(InstituicoesEnsino))]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSortable(false, true)]
        public ModeloRelatorio? ModeloRelatorio { get; set; }

        #region Propriedades para ordenação default

        [SMCHidden]
        [SMCSortable(true, true/*, "InstituicaoEnsino.Nome"*/)]
        public string DescricaoInstituicaoEnsino { get; set; }

        #endregion
    }
}