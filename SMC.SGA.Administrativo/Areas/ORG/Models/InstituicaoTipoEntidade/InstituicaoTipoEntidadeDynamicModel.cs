using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html; 
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "VISIBILIDADE", Size = SMCSize.Grid24_24)]
    public class InstituicaoTipoEntidadeDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCServiceReference(service: typeof(IORGDynamicService))]
        [SMCDataSource("TipoEntidade", "Seq", "Descricao", SortBy = SMCSortBy.Description)]
        public List<SMCDatasourceItem> TiposEntidade { get; set; }

        [SMCServiceReference(service: typeof(IInstituicaoTipoEntidadeService), methodName: "BuscarSituacoesEntidadeSelect")]
        [SMCDataSource("SituacaoEntidade", "Seq", "Descricao", storageType: SMCStorageType.TempData)]
        [SMCInclude(true)]
        public List<SMCDatasourceItem> SituacoesEntidade { get; set; }

        #endregion DataSources

        [SMCOrder(0)]
        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        /// <summary>
        /// Encapsula o seq para manter padrão do nome no navigationgroup InstituicaoTipoEntidadeNavigationGroup.
        /// </summary>
        [SMCIgnoreMetadata]
        public long SeqInstituicaoTipoEntidade
        {
            get { return Seq; }
        }

        [SMCOrder(1)]
        [SMCHidden()]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCOrder(2)]
        [SMCSelect("TiposEntidade")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCReadOnly(SMCViewMode.Edit)]
        public long SeqTipoEntidade { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool LogotipoVisivel { get; set; }

        [SMCOrder(4)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool UnidadeSeoVisivel { get; set; }

        [SMCOrder(5)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool SiglaVisivel { get; set; }

        [SMCOrder(6)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool NomeReduzidoVisivel { get; set; }

        [SMCOrder(7)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool NomeComplementarVisivel { get; set; }

        [SMCOrder(8)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool UnidadeAgdVisivel { get; set; }
               
        [SMCOrder(9)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool UnidadeGpiVisivel { get; set; }

        [SMCOrder(10)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool UnidadeNotificacaoVisivel { get; set; }

        [SMCOrder(11)]
        [SMCRequired]
        [SMCGroupedProperty("VISIBILIDADE")]
        [SMCRadioButtonList]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool UnidadeFormularioVisivel { get; set; }

        [SMCOrder(12)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(LogotipoVisivel), false)]
        [SMCConditionalRequired(nameof(LogotipoVisivel), true)]        
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(LogotipoVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? LogotipoObrigatorio { get; set; }

        [SMCOrder(13)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(UnidadeSeoVisivel), false)]
        [SMCConditionalRequired(nameof(UnidadeSeoVisivel), true)]
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(UnidadeSeoVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? UnidadeSeoObrigatorio { get; set; }

        [SMCOrder(14)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(SiglaVisivel), false)]
        [SMCConditionalRequired(nameof(SiglaVisivel), true)]
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(SiglaVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? SiglaObrigatoria { get; set; }

        [SMCOrder(15)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(NomeReduzidoVisivel), false)]
        [SMCConditionalRequired(nameof(NomeReduzidoVisivel), true)]
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(NomeReduzidoVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? NomeReduzidoObrigatorio { get; set; }

        [SMCOrder(16)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(NomeComplementarVisivel), false)]
        [SMCConditionalRequired(nameof(NomeComplementarVisivel), true)]
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(NomeComplementarVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? NomeComplementarObrigatorio { get; set; }

        [SMCOrder(17)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(UnidadeAgdVisivel), false)]
        [SMCConditionalRequired(nameof(UnidadeAgdVisivel), true)]
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(UnidadeAgdVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? UnidadeAgdObrigatorio { get; set; }

        [SMCOrder(18)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(UnidadeGpiVisivel), false)]
        [SMCConditionalRequired(nameof(UnidadeGpiVisivel), true)]
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(UnidadeGpiVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? UnidadeGpiObrigatorio { get; set; }

        [SMCOrder(19)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(UnidadeNotificacaoVisivel), false)]
        [SMCConditionalRequired(nameof(UnidadeNotificacaoVisivel), true)]
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(UnidadeNotificacaoVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? UnidadeNotificacaoObrigatorio { get; set; }

        [SMCOrder(20)]
        [SMCGroupedProperty("OBRIGATORIEDADE")]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(UnidadeFormularioVisivel), false)]
        [SMCConditionalRequired(nameof(UnidadeFormularioVisivel), true)]
        [SMCConditional("smc.sga.instituicaoTipoEntidade.fieldValue", nameof(UnidadeFormularioVisivel), SMCConditionalOperation.Equals, true)]
        [SMCSize(Framework.SMCSize.Grid10_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid8_24)]
        [SMCMapForceFromTo]
        public bool? UnidadeFormularioObrigatorio { get; set; }

        [SMCOrder(21)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<SituacaoInstituicaoTipoEntidadeViewModel> Situacoes { get; set; }

        [SMCOrder(22)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ObrigatoriedadeTelefoneViewModel> TiposTelefone { get; set; }

        [SMCOrder(23)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCCssClass("smc-clear")]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ObrigatoriedadeEnderecoViewModel> TiposEndereco { get; set; }

        [SMCOrder(24)]
        [SMCMapForceFromTo]
        [SMCDetail]
        [SMCSize(SMCSize.Grid12_24)]
        public SMCMasterDetailList<ObrigatoriedadeEnderecoEletronicoViewModel> TiposEnderecoEletronico { get; set; }

        [SMCIgnoreMetadata]
        /// <summary>
        /// Utilizado como parâmetro do servico
        /// </summary>
        public string[] Tokens { get; set; } = new string[] { TOKEN_TIPO_ENTIDADE_EXTERNADA.INSTITUICAO_ENSINO };

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Javascript("Areas/ORG/InstituicaoTipoEntidade")
                .Service<IInstituicaoTipoEntidadeService>(index: nameof(IInstituicaoTipoEntidadeService.BuscarInstituicaoTiposEntidade),
                                                          save: nameof(IInstituicaoTipoEntidadeService.SalvarInstituicaoTipoEntidade))
                .Tokens(tokenList: UC_ORG_002_03_01.PESQUISAR_PARAMETROS_INSTITUICAO_ENSINO,
                        tokenEdit: UC_ORG_002_03_02.MANTER_ASSOCIACAO_TIPO_ENTIDADE,
                        tokenInsert: UC_ORG_002_03_02.MANTER_ASSOCIACAO_TIPO_ENTIDADE,
                        tokenRemove: UC_ORG_002_03_02.MANTER_ASSOCIACAO_TIPO_ENTIDADE);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            switch (viewMode)
            {
                case SMCViewMode.Insert:

                    this.LogotipoVisivel = true;
                    this.UnidadeSeoVisivel = true;
                    this.SiglaVisivel = true;
                    this.NomeReduzidoVisivel = true;
                    this.NomeComplementarVisivel = true;
                    this.UnidadeAgdVisivel = true;
                    this.UnidadeGpiVisivel = true;
                    this.UnidadeNotificacaoVisivel = true;
                    this.UnidadeFormularioVisivel = true;

                    this.LogotipoObrigatorio = false;
                    this.UnidadeSeoObrigatorio = false;
                    this.SiglaObrigatoria = false;
                    this.NomeReduzidoObrigatorio = false;
                    this.NomeComplementarObrigatorio = false;
                    this.UnidadeAgdObrigatorio = false;
                    this.UnidadeGpiObrigatorio = false;
                    this.UnidadeNotificacaoObrigatorio = false;
                    this.UnidadeFormularioObrigatorio = false;

                    this.Situacoes = new SMCMasterDetailList<SituacaoInstituicaoTipoEntidadeViewModel>();
                    this.TiposEndereco = new SMCMasterDetailList<ObrigatoriedadeEnderecoViewModel>();
                    this.TiposTelefone = new SMCMasterDetailList<ObrigatoriedadeTelefoneViewModel>();
                    this.TiposEnderecoEletronico = new SMCMasterDetailList<ObrigatoriedadeEnderecoEletronicoViewModel>();

                    break;
            }
        }
    }
}