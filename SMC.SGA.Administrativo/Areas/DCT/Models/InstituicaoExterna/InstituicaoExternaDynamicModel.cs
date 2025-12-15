using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Localidades.Common.Constants;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class InstituicaoExternaDynamicModel : SMCDynamicViewModel
    {
        public InstituicaoExternaDynamicModel()
        {
            this.Ativo = true;
            //this.CodigoPais = (int)LocalidadesDefaultValues.SEQ_PAIS_BRASIL;
        }

        [SMCOrder(0)]
        [SMCKey]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCDescription]
        [SMCSortable(true, true)]
        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        public string Nome { get; set; }

        [SMCOrder(2)]
        [SMCMaxLength(15)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCSortable(true)]
        public string Sigla { get; set; }

        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect("Paises", "Codigo", "Nome", StorageType = SMCStorageType.Cache)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid5_24)]
        [SMCSortable(true)]
        public int CodigoPais { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ILocalidadeService), "BuscarPaisesValidosCorreios")]
        [SMCDataSource("Paises", "Codigo", "Nome", storageType: SMCStorageType.Cache)]
        [SMCInclude(ignore: true)]
        public List<SMCDatasourceItem> Paises { get; set; }

        [SMCOrder(4)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid5_24)]
        public bool EhInstituicaoEnsino { get; set; }

        [SMCOrder(5)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCConditionalReadonly("EhInstituicaoEnsino", SMCConditionalOperation.Equals, "false")]
        public int? CodigoMEC { get; set; }

        [SMCOrder(6)]
        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(15)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCConditionalReadonly("EhInstituicaoEnsino", SMCConditionalOperation.Equals, "false")]
        public string CodigoCAPES { get; set; }

        [SMCOrder(7)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSelect("CategoriasInstituicaoEnsino", SortBy = SMCSortBy.Description)]
        [SMCConditionalReadonly("EhInstituicaoEnsino", SMCConditionalOperation.Equals, "false")]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        [SMCConditionalRequired(nameof(CodigoPais), SMCConditionalOperation.Equals, LocalidadesDefaultValues.SEQ_PAIS_BRASIL, RuleName = "RSeqCategoriaInstituicaoEnsino1")]
        [SMCConditionalRequired(nameof(EhInstituicaoEnsino), SMCConditionalOperation.Equals, true, RuleName = "RSeqCategoriaInstituicaoEnsino2")]
        [SMCConditionalRule("RSeqCategoriaInstituicaoEnsino1 && RSeqCategoriaInstituicaoEnsino2")]
        public long? SeqCategoriaInstituicaoEnsino { get; set; }

        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IDCTDynamicService))]
        [SMCDataSource(dataSource: "CategoriaInstituicaoEnsino")]
        public List<SMCDatasourceItem> CategoriasInstituicaoEnsino { get; set; }

        [SMCOrder(8)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCInclude("CategoriaInstituicaoEnsino")]
        [SMCMapProperty("CategoriaInstituicaoEnsino.Descricao")]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        [SMCSortable(true, false, "CategoriaInstituicaoEnsino.Descricao")]
        public string DescricaoCategoriaInstituicaoEnsino { get; set; }

        [SMCOrder(9)]
        [SMCSelect()]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        //        [SMCSortable(true)]
        [SMCConditionalReadonly("EhInstituicaoEnsino", SMCConditionalOperation.Equals, "false")]
        [SMCConditionalRequired(nameof(CodigoPais), SMCConditionalOperation.Equals, LocalidadesDefaultValues.SEQ_PAIS_BRASIL, RuleName = "RTipoInstituicaoEnsino1")]
        [SMCConditionalRequired(nameof(EhInstituicaoEnsino), SMCConditionalOperation.Equals, true, RuleName = "RTipoInstituicaoEnsino2")]
        [SMCConditionalRule("RTipoInstituicaoEnsino1 && RTipoInstituicaoEnsino2")]
        public TipoInstituicaoEnsino? TipoInstituicaoEnsino { get; set; }

        [SMCOrder(10)]
        [SMCHidden(SMCViewMode.List)]
        [SMCSelect("InstituicoesEnsino")]
        [SMCSize(SMCSize.Grid14_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid10_24)]
        [SMCConditionalReadonly("EhInstituicaoEnsino", SMCConditionalOperation.Equals, "false")]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCOrder(11)]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCMapForceFromTo]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid9_24)]
        public bool Ativo { get; set; }

        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoEnsinoService), nameof(IInstituicaoEnsinoService.BuscarInstituicoesEnsinoSelect), values: new string[] { nameof(IgnorarFiltroDadosInstituicao) })]
        public List<SMCDatasourceItem> InstituicoesEnsino { get; set; }

        [SMCIgnoreProp]
        public bool IgnorarFiltroDadosInstituicao { get; set; } = true;

        #endregion DataSources

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Tokens(tokenEdit: UC_DCT_001_05_01.MANTER_INSTITUICAO_EXTERNA,
                        tokenInsert: UC_DCT_001_05_01.MANTER_INSTITUICAO_EXTERNA,
                        tokenList: UC_DCT_001_05_01.MANTER_INSTITUICAO_EXTERNA,
                        tokenRemove: UC_DCT_001_05_01.MANTER_INSTITUICAO_EXTERNA)
                 .DisableInitialListing(true)
                 .Service<IInstituicaoExternaService>(index: nameof(IInstituicaoExternaService.BuscarInstituicoesExternas));
        }
    }
}