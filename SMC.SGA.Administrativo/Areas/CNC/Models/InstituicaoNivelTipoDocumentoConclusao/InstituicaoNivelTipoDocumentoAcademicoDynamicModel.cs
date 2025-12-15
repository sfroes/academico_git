using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.DadosMestres.UI.Mvc.Area.GED.Lookups;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CNC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class InstituicaoNivelTipoDocumentoAcademicoDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelSistemaOrigemService), nameof(IInstituicaoNivelSistemaOrigemService.BuscarTiposUsoNiveisEnsino))]
        public List<SMCDatasourceItem> TipoUsoSistemaOrigem { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoComSequencialInstituicaoNivelSelect))]
        public List<SMCDatasourceItem> NiveisEnsinoDataSource { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ITipoDocumentoAcademicoService), nameof(ITipoDocumentoAcademicoService.BuscarTiposDocumentoAcademicoSelect))]
        public List<SMCDatasourceItem> TiposDocumentoAcademicoDataSource { get; set; }

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TiposFormacaoEspecifica { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        public List<SMCDatasourceItem> ConfiguracaoDiplomaGAD { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IGrupoRegistroService), nameof(IGrupoRegistroService.BuscarGruposRegistroSelect))]
        public List<SMCDatasourceItem> GruposRegistro { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(IOrgaoRegistroService), nameof(IOrgaoRegistroService.BuscarOrgaosRegistroSelect))]
        public List<SMCDatasourceItem> OrgaosRegistro { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        public override long Seq { get; set; }

        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCSelect(nameof(NiveisEnsinoDataSource))]
        public long SeqInstituicaoNivel { get; set; }

        [SMCSortable(true, true, "TipoDocumentoAcademico.Descricao")]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCSelect(nameof(TiposDocumentoAcademicoDataSource))]
        public long SeqTipoDocumentoAcademico { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TipoUsoSistemaOrigem))]
        [SMCConditionalReadonly(nameof(SeqInstituicaoNivel), SMCConditionalOperation.LessThen, 1)]
        [SMCDependency(nameof(SeqInstituicaoNivel), nameof(InstituicaoNivelTipoDocumentoAcademicoController.BuscarTipoUsoSistemaOrigem), "InstituicaoNivelTipoDocumentoAcademico", false)]
        [SMCSize(SMCSize.Grid4_24)]
        public UsoSistemaOrigem UsoSistemaOrigem { get; set; }

        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(ConfiguracaoDiplomaGAD))]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCDependency(nameof(UsoSistemaOrigem), nameof(InstituicaoNivelTipoDocumentoAcademicoController.BuscarConfiguracaoGADPorNivelEnsino), "InstituicaoNivelTipoDocumentoAcademico", true, includedProperties: new string[] { nameof(SeqInstituicaoNivel) })]
        public long? SeqConfiguracaoGad { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid14_24)]
        public string TokenPermissaoEmissaoDocumento { get; set; }

        [SMCHidden]
        public string SiglaContexto { get; } = "ACD";

        [ConfiguracaoProcessoGedLookup]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid14_24)]
        [SMCDependency(nameof(SiglaContexto))]
        [SMCInclude(true)] // O Dynamic gera include automático dos lookups, ignorado por ser uma entidade externa
        public ConfiguracaoProcessoGedLookupViewModel SeqConfiguracaoProcessoGed { get; set; }

        [SMCRadioButtonList]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public bool? HabilitaRegistroDocumento { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(GruposRegistro))]
        [SMCConditionalDisplay(nameof(HabilitaRegistroDocumento), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(HabilitaRegistroDocumento), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24)]
        public long? SeqGrupoRegistro { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(OrgaosRegistro))]
        [SMCConditionalDisplay(nameof(HabilitaRegistroDocumento), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(HabilitaRegistroDocumento), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24)]
        public long? SeqOrgaoRegistro { get; set; }

        [SMCDetail(min: 1)]
        [SMCConditionalDisplay(nameof(UsoSistemaOrigem), SMCConditionalOperation.Equals, UsoSistemaOrigem.ArquivoPDF)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<InstituicaoNivelTipoDocumentoModelosRelatorioViewModel> ModelosRelatorio { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoDocumentoAcademico), nameof(InstituicaoNivelTipoDocumentoAcademicoController.ExibeTiposFormacao), "InstituicaoNivelTipoDocumentoAcademico", true)]
        public bool? ExibeTiposFormacao { get; set; }

        [SMCConditionalDisplay(nameof(ExibeTiposFormacao), SMCConditionalOperation.Equals, true)]
        [SMCDetail]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<InstituicaoNivelTipoDocumentoFormacaoEspecificaViewModel> FormacoesEspecificas { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Service<IInstituicaoNivelTipoDocumentoAcademicoService>(edit: nameof(IInstituicaoNivelTipoDocumentoAcademicoService.BuscarInstituicaoNivelTipoDocumentoAcademico),
                                                                            save: nameof(IInstituicaoNivelTipoDocumentoAcademicoService.Salvar))
                   .Tokens(tokenInsert: UC_CNC_004_04_01.MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL,
                           tokenEdit: UC_CNC_004_04_01.MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL,
                           tokenRemove: UC_CNC_004_04_01.MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL,
                           tokenList: UC_CNC_004_04_01.MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL)
                   .Ajax();
        }
    }
}