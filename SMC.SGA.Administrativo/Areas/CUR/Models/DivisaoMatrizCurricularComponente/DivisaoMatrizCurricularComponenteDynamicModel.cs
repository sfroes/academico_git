using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoMatrizCurricularComponenteDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IConfiguracaoComponenteService),
                             nameof(IConfiguracaoComponenteService.BuscarConfiguracaoComponentePorGrupoCurricularComponenteSelect),
                             values: new[] { nameof(SeqGrupoCurricularComponente) })]
        public List<SMCDatasourceItem> ConfiguracoesComponente { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IDivisaoMatrizCurricularService), nameof(IDivisaoMatrizCurricularService.BuscarDivisoesMatrizCurricularDescricaoSelect),
            values: new string[] { nameof(SeqMatrizCurricular) })]
        public List<SMCDatasourceItem> DivisoesMatrizCurricular { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IEscalaApuracaoService), nameof(IEscalaApuracaoService.BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect),
            values: new string[] { nameof(SeqCurriculoCursoOferta) })]
        public List<SMCDatasourceItem> EscalasApuracao { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ITurnoService), nameof(ITurnoService.BuscarTurnosNivelEnsinoPorCurriculoCursoOfertaSelect),
            values: new string[] { nameof(SeqCurriculoCursoOferta) })]
        public List<SMCDatasourceItem> Turnos { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ICriterioAprovacaoService), nameof(ICriterioAprovacaoService.BuscarCriteriosAprovacaoNivelEnsinoPorCurriculoCursoOfertaSelect),
           values: new string[] { nameof(SeqCurriculoCursoOferta) })]
        public List<SMCDatasourceItem> CriteriosAprovacao { get; set; }

        #endregion [ DataSources ]

        #region [ Campos ]

        [SMCKey]
        [SMCHidden]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqMatrizCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqComponenteCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqGrupoCurricularComponente { get; set; }

        /// <summary>
        /// Define se o componente do grupo curricular compontente desta divisão exige subistituto
        /// </summary>
        //[SMCHidden]
        //public bool GrupoCurricularComponenteExigeAssunto { get; set; }

        /// <summary>
        /// Se o grupo curricular já tiver uma divisão definida não pode permitir que o componente cadastre uma divisão
        /// </summary>
        [SMCHidden]
        public bool GrupoCurricularDivisaoCadastrada { get; set; }

        [SMCHidden]
        public bool QuantidadeVagasObrigatoria { get; set; }

        [SMCHidden]
        public bool CriterioAprovacaoObrigatorio { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ConfiguracoesComponente))]
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid15_24, SMCSize.Grid15_24)]
        public long? SeqConfiguracaoComponente { get; set; }

        [SMCRequired]
        [SMCConditionalReadonly(nameof(GrupoCurricularDivisaoCadastrada), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCSelect(nameof(DivisoesMatrizCurricular))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqDivisaoMatrizCurricular { get; set; }

        [SMCConditionalRequired(nameof(QuantidadeVagasObrigatoria), true)]
        [SMCMask("999")]
        [SMCMinValue(0)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public short? QuantidadeVagas { get; set; }

        [SMCConditionalRequired(nameof(CriterioAprovacaoObrigatorio), true)]
        [SMCSelect(nameof(CriteriosAprovacao))]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        public long? SeqCriterioAprovacao { get; set; }

        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(DivisaoMatrizCurricularComponenteController.BuscarCriterioNota), "DivisaoMatrizCurricularComponente", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        public string CriterioNotaMaxima { get; set; }

        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(DivisaoMatrizCurricularComponenteController.BuscarAprovacaoPercentual), "DivisaoMatrizCurricularComponente", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public string CriterioPercentualNotaAprovado { get; set; }

        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(DivisaoMatrizCurricularComponenteController.BuscarPresencaPercentual), "DivisaoMatrizCurricularComponente", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public string CriterioPercentualFrequenciaAprovado { get; set; }

        [SMCDependency(nameof(SeqCriterioAprovacao), nameof(DivisaoMatrizCurricularComponenteController.BuscarEscalaApuracao), "DivisaoMatrizCurricularComponente", true)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid5_24)]
        public string CriterioDescricaoEscalaApuracao { get; set; }

        [SMCMapForceFromTo]
        [SMCSize(SMCSize.Grid24_24)]
        public List<DivisaoMatrizCurricularComponenteDetailViewModel> DivisoesComponente { get; set; }

        [SMCCheckBoxList(nameof(Turnos))]
        [SMCSize(SMCSize.Grid24_24)]
        public List<long> TurnosExcecao { get; set; }

        //[SMCHidden]
        //public bool AssuntoComponente { get; set; } = true;

        //[ComponenteCurricularLookup]
        //[SMCConditionalDisplay(nameof(GrupoCurricularComponenteExigeAssunto), true)]
        //[SMCDependency(nameof(SeqGrupoCurricularComponente))]
        //[SMCDependency(nameof(AssuntoComponente))]
        //[SMCSize(SMCSize.Grid24_24)]
        //public List<ComponenteCurricularLookupViewModel> ComponentesCurricularSubstitutos { get; set; }

        #endregion [ Campos ]

        #region [ Configuracoes ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Detail<DivisaoMatrizCurricularComponenteListarDynamicModel>("_DetailList", hideNavigator: true, hideRowTotalizer: true, denyChangeNumberOfRowsPerPage: true)
                   .HeaderIndex(nameof(DivisaoMatrizCurricularComponenteController.DivisaoMatrizCurricularComponenteCabecalho))
                   .ViewPartialEdit("_EditarConfiguracaoComponenteView")
                   .ViewPartialInsert("_EditarConfiguracaoComponenteView")
                   .ButtonBackEdit((controller, m) =>
                   {
                       var model = (DivisaoMatrizCurricularComponenteDynamicModel)m;
                       return controller.Url.Action("Index", "ConfiguracaoComponenteMatriz", new { SeqMatrizCurricular = SMCEncryptedLong.GetStringValue(model.SeqMatrizCurricular), SeqCurriculoCursoOferta = SMCEncryptedLong.GetStringValue(model.SeqCurriculoCursoOferta), voltar = true });
                   })
                   .ConfigureButton((button, model, action) =>
                   {
                       if (action == SMCDynamicButtonAction.Insert)
                           button.Hide(true);
                   })
                   .ModalSize(SMCModalWindowSize.Medium, SMCModalWindowSize.Medium, SMCModalWindowSize.Largest)
                   .Service<IDivisaoMatrizCurricularComponenteService>(
                        edit: nameof(IDivisaoMatrizCurricularComponenteService.BuscarDivisaoMatrizCurricularComponente),
                        insert: nameof(IDivisaoMatrizCurricularComponenteService.BuscarConfiguracaoNovaDivisaoMatrizCurricularComponente),
                        //index: nameof(IDivisaoMatrizCurricularComponenteService.BuscarDivisaoMatrizCurricularGruposComponentes),
                        save: nameof(IDivisaoMatrizCurricularComponenteService.SalvarDivisaoMatrizCurricularComponente))
                   .Tokens(tokenInsert: UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ,
                           tokenEdit: UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ,
                           //tokenRemove: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                           tokenRemove: UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ,
                           tokenList: UC_CUR_001_06_01.CONSULTA_DIVISOES_MATRIZ_CURRICULAR)
                           //tokenList: SMCSecurityConsts.SMC_DENY_AUTHORIZATION)
                    .Assert("MSG_Assert_Salvar_Configuracao", (service, model) =>
                    {
                        var modelDivisaoMatrizCurricularComponente = (model as DivisaoMatrizCurricularComponenteDynamicModel);

                        var divisaoMatrizCurricularComponenteService = service.Create<IDivisaoMatrizCurricularComponenteService>();

                        var modeloAssert = divisaoMatrizCurricularComponenteService.ValidarAssertSalvar(modelDivisaoMatrizCurricularComponente.Transform<DivisaoMatrizCurricularComponenteData>());

                        return modeloAssert.ExibirAssert;
                    });
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new MatrizCurricularNavigationGroup(this);
        }

        #endregion [ Configuracoes ]
    }
}