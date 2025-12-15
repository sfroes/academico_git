using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.MAT.Models;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IProcessoEtapaService), nameof(IProcessoEtapaService.BuscarSituacoesEtapaPorProcessoEtapaSelect), values: new string[] { nameof(SeqProcessoEtapa) })]
        public List<SMCDatasourceItem> SituacoesEtapa { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IProcessoEtapaService), nameof(IProcessoEtapaService.BuscarTiposPrazoAtendimentoEtapa), values: new string[] { nameof(SeqProcessoEtapa) })]
        public List<SMCDatasourceItem> TiposPrazoEtapa { get; set; }

        #endregion DataSources

        #region Propriedades Auxiliares

        [SMCParameter]
        [SMCHidden]
        public long SeqProcessoEtapa { get { return this.Seq; } }

        [SMCHidden]
        [SMCMapProperty("Processo.Servico.TipoServico.ExigeEscalonamento")]
        public bool ExigeEscalonamento { get; set; }

        [SMCHidden]
        [SMCMapProperty("Processo.Servico.TipoServico.Token")]
        public string TokenTipoServico { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }

        [SMCHidden]
        public bool ExibeSecaoTokenMatricula { get; set; }

        #endregion Propriedades Auxiliares

        #region Dados Gerais

        [SMCReadOnly]
        [SMCSelect(NameDescriptionField = nameof(DescricaoEtapaSgf))]
        [SMCSize(SMCSize.Grid8_24)]
        public long SeqEtapaSgf { get; set; }

        public string DescricaoEtapaSgf { get; set; }

        [SMCKey]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCRequired]
        [SMCMaxLength(255)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid13_24)]
        public string DescricaoEtapa { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24)]
        public short Ordem { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24)]
        public string Token { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid9_24)]
        public SituacaoEtapa SituacaoEtapa { get; set; }

        [SMCReadOnly]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataEncerramento { get; set; }

        #endregion

        [SMCRequired]
        [SMCSelect(nameof(TiposPrazoEtapa))]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24)]
        public TipoPrazoEtapa? TipoPrazoEtapa { get; set; }

        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true, RuleName = "CRO3")]
        [SMCConditionalReadonly(nameof(TipoPrazoEtapa), SMCConditionalOperation.NotEqual, (int)Academico.Common.Areas.SRC.Enums.TipoPrazoEtapa.DiasCorridos, PersistentValue = false, RuleName = "CRO4")]
        [SMCConditionalReadonly(nameof(TipoPrazoEtapa), SMCConditionalOperation.NotEqual, (int)Academico.Common.Areas.SRC.Enums.TipoPrazoEtapa.DiasUteis, PersistentValue = false, RuleName = "CRO5")]
        [SMCConditionalRule("CRO3 || (CRO4 && CRO5)")]
        [SMCConditionalRequired(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoPrazoEtapa.DiasCorridos, RuleName = "CR1")]
        [SMCConditionalRequired(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoPrazoEtapa.DiasUteis, RuleName = "CR2")]
        [SMCConditionalRule("CR1 || CR2")]
        [SMCDependency(nameof(TipoPrazoEtapa), nameof(ProcessoEtapaController.PreencherCampoNumeroDiasPrazoEtapa), "ProcessoEtapa", false)]
        [SMCSize(SMCSize.Grid6_24)]
        public short? NumeroDiasPrazoEtapa { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCConditionalReadonly(nameof(TipoPrazoEtapa), SMCConditionalOperation.NotEqual, (int)Academico.Common.Areas.SRC.Enums.TipoPrazoEtapa.PeriodoVigencia, PersistentValue = false, RuleName = "CRO6")]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, RuleName = "CRO7", PersistentValue = true)]
        [SMCConditionalRule("CRO6 || CRO7")]
        [SMCConditionalRequired(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoPrazoEtapa.PeriodoVigencia)]
        [SMCDependency(nameof(TipoPrazoEtapa), nameof(ProcessoEtapaController.PreencherCampoDataInicio), "ProcessoEtapa", false)]
        [SMCSize(SMCSize.Grid6_24)]
        public DateTime? DataInicio { get; set; }

        [SMCMinDateNow]
        [SMCMinDate(nameof(DataInicio))]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCConditionalReadonly(nameof(TipoPrazoEtapa), SMCConditionalOperation.NotEqual, (int)Academico.Common.Areas.SRC.Enums.TipoPrazoEtapa.PeriodoVigencia, PersistentValue = false, RuleName = "CRO8")]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, RuleName = "CRO9", PersistentValue = true)]
        [SMCConditionalRule("CRO8 || CRO9")]
        [SMCConditionalRequired(nameof(TipoPrazoEtapa), SMCConditionalOperation.Equals, (int)Academico.Common.Areas.SRC.Enums.TipoPrazoEtapa.PeriodoVigencia)]
        [SMCDependency(nameof(TipoPrazoEtapa), nameof(ProcessoEtapaController.PreencherCampoDataFim), "ProcessoEtapa", false)]
        [SMCSize(SMCSize.Grid6_24)]
        public DateTime? DataFim { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid9_24)]
        public bool CentralAtendimento { get; set; }

        [SMCMaxLength(150)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(CentralAtendimento), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(CentralAtendimento), SMCConditionalOperation.Equals, false)]
        [SMCSize(SMCSize.Grid24_24)]
        public string OrientacaoAtendimento { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid13_24)]
        public bool FinalizacaoEtapaAnterior { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid13_24)]
        public bool SolicitacaoEtapaAnteriorAtendida { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid8_24)]
        public bool ExibeItemMatriculaSolicitante { get; set; }

        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true, RuleName = "CRO10")]
        [SMCConditionalReadonly(nameof(ExibeItemMatriculaSolicitante), SMCConditionalOperation.NotEqual, true, PersistentValue = false, RuleName = "CRO11")]
        [SMCConditionalRule("CRO10 || CRO11")]
        [SMCConditionalRequired(nameof(ExibeItemMatriculaSolicitante), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid13_24)]
        public bool? ExibeItemAposTerminoEtapa { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24)]
        public bool EtapaCompartilhada { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid5_24)]
        public bool ControleVaga { get; set; }

        [SMCDetail]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<SituacaoItemMatriculaViewModel> SituacoesItemMatricula { get; set; }

        [SMCDetail]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ProcessoEtapaFiltroDadoViewModel> FiltrosDados { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.IgnoreFilterGeneration()
                   .Header("CabecalhoProcessoEtapa")
                   .ViewPartialEdit("_Editar")
                   .RedirectIndexTo("Index", "Processo", null)
                   .ButtonBackIndex("Index", "Processo")
                   .Tokens(tokenInsert: UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO,
                           tokenEdit: UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO,
                           tokenRemove: UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO,
                           tokenList: UC_SRC_002_01_03.MANTER_ETAPA_PROCESSO)
                   .Service<IProcessoEtapaService>(edit: nameof(IProcessoEtapaService.BuscarProcessoEtapa),
                                                   save: nameof(IProcessoEtapaService.SalvarProcessoEtapa))
                    .Assert("MSG_Assert_Etapa_Escalonamentos", (service, model) =>
                    {
                        var modelProcessoEtapa = (model as ProcessoEtapaDynamicModel);

                        var processoEtapaService = service.Create<IProcessoEtapaService>();
                        processoEtapaService.ValidarModeloSalvar(modelProcessoEtapa.Transform<ProcessoEtapaData>());

                        bool exibeAssert = processoEtapaService.ValidarAssertSalvar(modelProcessoEtapa.Transform<ProcessoEtapaData>());

                        return exibeAssert;
                    });
        }
    }
}