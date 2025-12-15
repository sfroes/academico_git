using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Service.Areas.ORG.Services;
using SMC.Academico.Service.Areas.PES.Services;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using SMC.SGA.Administrativo.Areas.PES.Views.ConfiguracaoAvaliacaoPpa.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConfiguracaoAvaliacaoPpaDynamicModel : SMCDynamicViewModel
    {
        #region DataSources
        [SMCHidden]
        public bool AmostrasAtivas { get; set; } = true;

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(EntidadeService.BuscarUnidadesResponsaveisGPILocalSelect))]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IConfiguracaoAvaliacaoPpaService), nameof(ConfiguracaoAvaliacaoPpaService.BuscarAvaliacoesIntitucionaisSelect))]
        public List<SMCDatasourceItem> AvaliacoesPpaSelect { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IConfiguracaoAvaliacaoPpaService), nameof(ConfiguracaoAvaliacaoPpaService.BuscarOrigemAmostraPpaSelect), values: new string[] { nameof(AmostrasAtivas) })]
        public List<SMCDatasourceItem> OrigensAmostraPpaSelect { get; set; }

        //[SMCDataSource]
        //[SMCServiceReference(typeof(IConfiguracaoAvaliacaoPpaService), nameof(ConfiguracaoAvaliacaoPpaService.BuscarInstrumentosSelect), values: new string[] { nameof(CodigoAvaliacaoPpa) })]
        //public List<SMCDatasourceItem> CodigosInstrumentosPpaSelect { get; set; }


        [SMCDataSource]
        [SMCServiceReference(typeof(IConfiguracaoAvaliacaoPpaService), nameof(ConfiguracaoAvaliacaoPpaService.BuscarTiposInstrumentosSelect))]
        public List<SMCDatasourceItem> TiposInstrumentosSelect { get; set; }


        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        #endregion

        [SMCKey]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCReadOnly(SMCViewMode.Insert)]
        public override long Seq { get; set; }

        [SMCFilter(true,true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        [Required]
        public TipoAvaliacaoPpa TipoAvaliacaoPpa { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(EntidadesResponsaveis), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid8_24)]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        [Required]
        [SMCSelect(nameof(AvaliacoesPpaSelect), autoSelectSingleItem: true)]
        public int? CodigoAvaliacaoPpa { get; set; }

        [SMCFilter(true)]
        [SMCSize(SMCSize.Grid6_24)]
        [CicloLetivoLookup]
        [SMCIgnoreProp(SMCViewMode.List)]
        [SMCConditionalRequired(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.Equals, new object[] { TipoAvaliacaoPpa.SemestralDisciplina, TipoAvaliacaoPpa.AutoavaliacaoProfessor, TipoAvaliacaoPpa.AutoavaliacaoAluno })]
        [SMCConditionalReadonly(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.NotEqual, new object[] { TipoAvaliacaoPpa.SemestralDisciplina, TipoAvaliacaoPpa.AutoavaliacaoProfessor, TipoAvaliacaoPpa.AutoavaliacaoAluno })]
        public CicloLetivoLookupViewModel SeqCicloLetivo { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(NiveisEnsino))]

        public long? SeqNivelEnsino { get; set; }

        [SMCRequired]
        [SMCMaxDate(nameof(DataFimVigencia))]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCDependency(nameof(CodigoAvaliacaoPpa), nameof(ConfiguracaoAvaliacaoPpaController.BuscarDataAvaliacaoIntitucionalSelecionada), "ConfiguracaoAvaliacaoPpa", "PES", false)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCMinDate(nameof(DataInicioVigencia))]
        [SMCDependency(nameof(CodigoAvaliacaoPpa), nameof(ConfiguracaoAvaliacaoPpaController.BuscarDataAvaliacaoIntitucionalSelecionada), "ConfiguracaoAvaliacaoPpa", "PES", false)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCConditionalRequired(nameof(DataFimVigencia), SMCConditionalOperation.NotEqual, "")]
        [SMCConditionalReadonly(nameof(DataFimVigencia), SMCConditionalOperation.Equals, "")]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCMinDate(nameof(DataFimVigencia))]
        public DateTime? DataLimiteRespostas { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(OrigensAmostraPpaSelect), autoSelectSingleItem: true)]
        [SMCRequired]
        public int? CodigoOrigemPpa { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(TiposInstrumentosSelect))]
        [SMCConditionalRequired(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.Equals, new object[] { TipoAvaliacaoPpa.SemestralDisciplina, TipoAvaliacaoPpa.AutoavaliacaoProfessor, TipoAvaliacaoPpa.AutoavaliacaoAluno })]
        [SMCConditionalReadonly(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.NotEqual, new object[] { TipoAvaliacaoPpa.SemestralDisciplina, TipoAvaliacaoPpa.AutoavaliacaoProfessor, TipoAvaliacaoPpa.AutoavaliacaoAluno })]
        public int? SeqTipoInstrumentoPpa { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCConditionalRequired(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.Equals, new object[] { TipoAvaliacaoPpa.SemestralDisciplina, TipoAvaliacaoPpa.AutoavaliacaoProfessor, TipoAvaliacaoPpa.AutoavaliacaoAluno })]
        [SMCConditionalReadonly(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.NotEqual, new object[] { TipoAvaliacaoPpa.SemestralDisciplina, TipoAvaliacaoPpa.AutoavaliacaoProfessor, TipoAvaliacaoPpa.AutoavaliacaoAluno })]
        public int? CodigoAplicacaoQuestionarioSgq { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.Equals, new object[] { TipoAvaliacaoPpa.SemestralDisciplina, TipoAvaliacaoPpa.AutoavaliacaoProfessor, TipoAvaliacaoPpa.AutoavaliacaoAluno })]
        [SMCConditionalReadonly(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.NotEqual, new object[] { TipoAvaliacaoPpa.SemestralDisciplina, TipoAvaliacaoPpa.AutoavaliacaoProfessor, TipoAvaliacaoPpa.AutoavaliacaoAluno })]
        [SMCDependency(nameof(CodigoAvaliacaoPpa), nameof(ConfiguracaoAvaliacaoPpaController.BuscarEspecieAvaliadorSelecionado), "ConfiguracaoAvaliacaoPpa", "PES", false)]
        public int? SeqEspecieAvaliadorPpa { get; set; }

        [SMCSize(SMCSize.Grid18_24)]
        [SMCSelect]
        [SMCConditionalRequired(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.Equals, new object[] { TipoAvaliacaoPpa.Concluinte })]
        [SMCConditionalReadonly(nameof(TipoAvaliacaoPpa), SMCConditionalOperation.NotEqual, new object[] { TipoAvaliacaoPpa.Concluinte })]
        [SMCDependency(nameof(CodigoAvaliacaoPpa), nameof(ConfiguracaoAvaliacaoPpaController.BuscarInstrumentos), "ConfiguracaoAvaliacaoPpa", "PES", false)]
        public int? CodigoInstrumentoPpa { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.List)]

        public string ParteFixaNomeAvaliacao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenList: UC_PES_007_01_01.PESQUISAR_CONFIGURACAO_AVALIACAO,
                           tokenInsert: UC_PES_007_01_02.MANTER_CONFIGURACAO_AVALIACAO,
                           tokenEdit: UC_PES_007_01_02.MANTER_CONFIGURACAO_AVALIACAO,
                           tokenRemove : UC_PES_007_01_02.MANTER_CONFIGURACAO_AVALIACAO
                           )
                    .DisableInitialListing()
                    .EditInModal()
                    .ModalSize(SMCModalWindowSize.Largest)
                    .Detail<ConfiguracaoAvaliacaoPpaListarDynamicModel>("_DetailList")
                    .Service<IConfiguracaoAvaliacaoPpaService>(index: nameof(IConfiguracaoAvaliacaoPpaService.BuscarAvaliacoes),
                                                               save: nameof(IConfiguracaoAvaliacaoPpaService.SalvarConfiguracaoAvaliacao),
                                                               delete: nameof(IConfiguracaoAvaliacaoPpaService.ExcluirConfiguracaoAvaliacao))
                    .Assert(nameof(UIResource.Mensagem_Inclusao_Configuracao), m => true); 
        }

    }
}