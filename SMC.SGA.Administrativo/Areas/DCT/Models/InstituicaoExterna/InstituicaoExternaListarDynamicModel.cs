using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class InstituicaoExternaListarDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]

        [SMCServiceReference(typeof(IInstituicaoEnsinoService), nameof(IInstituicaoEnsinoService.BuscarInstituicoesEnsinoSelect), values: new string[] { nameof(IgnorarFiltroDadosInstituicao) })]
        public List<SMCDatasourceItem> InstituicoesEnsino { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ILocalidadeService), "BuscarPaisesValidosCorreios")]
        [SMCDataSource("Paises", "Codigo", "Nome", storageType: SMCStorageType.Cache)]
        [SMCInclude(ignore: true)]
        public List<SMCDatasourceItem> Paises { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IDCTDynamicService))]
        [SMCDataSource(dataSource: "CategoriaInstituicaoEnsino")]
        public List<SMCDatasourceItem> CategoriasInstituicaoEnsino { get; set; }

        #endregion DataSources

        [SMCIgnoreProp]
        public bool IgnorarFiltroDadosInstituicao { get; set; } = true;
         
        [SMCOrder(0)]
        [SMCKey]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCSortable(true, true)]
        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        public string Nome { get; set; }

        [SMCOrder(2)]
        [SMCSortable(true)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string Sigla { get; set; }

        [SMCOrder(3)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string DescricaoPais { get; set; }

        [SMCOrder(8)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]        
        [SMCSortable(true, false, "CategoriaInstituicaoEnsino.Descricao")]
        [SMCCssClass("smc-size-md-7 smc-size-xs-7 smc-size-sm-7 smc-size-lg-7")]
        public string DescricaoCategoria { get; set; }

        [SMCOrder(9)]
        [SMCSelect()]
        [SMCSortable(true)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }
    }
}