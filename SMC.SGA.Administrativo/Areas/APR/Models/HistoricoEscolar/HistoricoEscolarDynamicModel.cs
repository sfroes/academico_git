using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.APR.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class HistoricoEscolarDynamicModel : SMCDynamicViewModel
    {
        #region [ Datasources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(ICicloLetivoService), nameof(ICicloLetivoService.BuscarCiclosLetivosPorHistoricoAluno), values: new[] { nameof(SeqAluno) })]
        public List<SMCDatasourceItem> CiclosLetivos { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ICriterioAprovacaoService), nameof(ICriterioAprovacaoService.BuscarCriteriosAprovacaoSelect), values: new[] { nameof(SeqComponenteCurricular), nameof(SeqAluno), nameof(SeqCicloLetivo) })]
        public List<SMCDatasourceItem> CriteriosAprovacao { get; set; }

        [SMCDataSource(storageType: SMCStorageType.None)]
        [SMCServiceReference(typeof(IEscalaApuracaoItemService), nameof(IEscalaApuracaoItemService.BuscarEscalaApuracaoItensSelect), values: new[] { nameof(SeqEscalaApuracao) })]
        public List<SMCDatasourceItem> EscalaApuracaoItens { get; set; }

        #endregion [ Datasources ]

        #region [ Parâmetros ]

        [SMCFilterKey]
        [SMCHidden]
        [SMCParameter]
        public long SeqAluno { get; set; }

        [SMCHidden]
        public TipoGestaoDivisaoComponente[] TiposGestaoDivisaoComponente
        {
            get
            {
                return SMCAuthorizationHelper.Authorize(UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES) ?
                    new[]
                    {
                            TipoGestaoDivisaoComponente.AtividadeAcademica,
                            TipoGestaoDivisaoComponente.EntregaComprovante,
                            TipoGestaoDivisaoComponente.Exame,
                            TipoGestaoDivisaoComponente.Trabalho,
                            TipoGestaoDivisaoComponente.Turma
                    } :
                    new[] { TipoGestaoDivisaoComponente.Exame };
            }
        }

        [SMCHidden]
        public bool AssuntoComponente => true;

        [SMCHidden]
        public long? SeqOrigemAvaliacao { get; set; }

        [SMCHidden]
        public long? SeqSolicitacaoDispensa { get; set; }

        [SMCHidden]
        public bool SomenteLeitura { get; set; }

        [SMCHidden]
        public decimal? PercentualFrequencia { get; set; }

        #endregion [ Parâmetros ]

        #region [ Campos ]

        [SMCHidden]
        public override long Seq { get; set; }

        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCConditionalRequired(nameof(SomenteLeitura), false)]
        [SMCSelect(nameof(CiclosLetivos), autoSelectSingleItem: true, NameDescriptionField = nameof(DescricaoCicloLetivo))]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        public long? SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDependency(nameof(SeqComponenteCurricular), nameof(HistoricoEscolarController.VerificarComponenteCurricularOptativoMatriz), "HistoricoEscolar", true, includedProperties: new[] { nameof(SeqAluno), nameof(SeqCicloLetivo) })]
        [SMCHidden]
        public bool? ComponenteOptativoMatriz { get; set; }

        [SMCConditionalReadonly(nameof(ComponenteOptativoMatriz), SMCConditionalOperation.NotEqual, "", PersistentValue = true)]
        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCConditionalRequired(nameof(SomenteLeitura), false)]
        [SMCDependency(nameof(SeqComponenteCurricular), nameof(HistoricoEscolarController.VerificarComponenteCurricularOptativoAluno), "HistoricoEscolar", true, includedProperties: new[] { nameof(SeqAluno), nameof(SeqCicloLetivo) })]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid6_24)]
        public bool Optativa { get; set; }

        [SMCHidden]
        public bool VinculoAlunoExigeOfertaMatrizCurricular { get; set; }

        /// <summary>
        /// Define se serão listados apenas os componentes e assuntos da matriz do aluno.
        /// Este campo será desabilitado na view caso o vínculo do aluno não exija oferta de matriz.
        /// </summary>
        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        public bool ConsiderarMatriz { get; set; }

        [SMCDependency(nameof(ConsiderarMatriz), nameof(HistoricoEscolarController.BuscarMatrizCurricular), "HistoricoEscolar", true, includedProperties: new[] { nameof(SeqAluno), nameof(SeqCicloLetivo) })]
        [SMCDependency(nameof(SeqCicloLetivo), nameof(HistoricoEscolarController.BuscarMatrizCurricular), "HistoricoEscolar", true, includedProperties: new[] { nameof(SeqAluno), nameof(ConsiderarMatriz) })]
        [SMCHidden]
        public long? SeqMatrizCurricular { get; set; }

        [ComponenteCurricularLookup]
        [SMCConditionalReadonly(nameof(SeqCicloLetivo), "", PersistentValue = true, RuleName = "CRC2")]
        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "CRC1")]
        [SMCConditionalRequired(nameof(SomenteLeitura), false)]
        [SMCConditionalRule("CRC1 || CRC2")]
        [SMCDependency(nameof(SeqMatrizCurricular))]
        [SMCDependency(nameof(TiposGestaoDivisaoComponente))]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCLookupViewModel SeqComponenteCurricular { get; set; }

        //FIX: Remover e referenciar campos hidden do componente curricular nos conditionals do assunto ao corrigir campos hidden do lookup
        [SMCHidden]
        [SMCDependency(nameof(SeqComponenteCurricular), nameof(HistoricoEscolarController.BuscarComponenteCurricularExigeAssunto), "HistoricoEscolar", false)]
        public bool? ComponenteCurricularExigeAssunto { get; set; }

        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [ComponenteCurricularLookup]
        [SMCConditionalDisplay(nameof(ComponenteCurricularExigeAssunto), true)]
        [SMCConditionalRequired(nameof(ComponenteCurricularExigeAssunto), true)]
        [SMCDependency(nameof(AssuntoComponente))]
        [SMCDependency(nameof(SeqComponenteCurricular))]
        [SMCDependency(nameof(SeqMatrizCurricular))]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCLookupViewModel SeqComponenteCurricularAssunto { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqComponenteCurricular), nameof(HistoricoEscolarController.BuscarComponenteCurricularGestaoExame), "HistoricoEscolar", true)]
        public bool ComponenteCurricularGestaoExame { get; set; }

        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCConditionalDisplay(nameof(ComponenteCurricularGestaoExame), true)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataExame { get; set; }

        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCConditionalRequired(nameof(SomenteLeitura), false)]
        [SMCDependency(nameof(SeqComponenteCurricular), nameof(HistoricoEscolarController.BuscarCriteriosAprovacao), "HistoricoEscolar", true, new[] { nameof(SeqAluno), nameof(SeqCicloLetivo), nameof(ConsiderarMatriz) })]
        [SMCSelect(nameof(CriteriosAprovacao), autoSelectSingleItem: true, NameDescriptionField = nameof(DescricaoCriterioAprovacao))]
        [SMCSize(SMCSize.Grid24_24)]
        public long? SeqCriterioAprovacao { get; set; }

        [SMCHidden]
        public string DescricaoCriterioAprovacao { get; set; }

        [SMCHidden]
        public bool IndicadorApuracaoNota { get; set; }

        [SMCHidden]
        public TipoArredondamento? TipoArredondamento { get; set; }

        [SMCHidden]
        public short NotaMaxima { get; set; }

        [SMCConditionalReadonly(nameof(IndicadorApuracaoNota), false, RuleName = "CRN2", PersistentValue = true)]
        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, RuleName = "CRN1", PersistentValue = true)]
        [SMCConditionalRequired(nameof(IndicadorApuracaoNota), true, RuleName ="CRQ1")]
        [SMCConditionalRequired(nameof(SomenteLeitura), false, RuleName = "CRQ2")]
        [SMCConditionalRule("CRN1 || CRN2")]
        [SMCConditionalRule("CRQ1 && CRQ2")]
        [SMCMaxValue(nameof(NotaMaxima))]
        [SMCMinValue(0)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public short? Nota { get; set; }

        [SMCHidden]
        public bool IndicadorApuracaoEscala { get; set; }

        [SMCHidden]
        public long? SeqEscalaApuracao { get; set; }

        [SMCConditionalReadonly(nameof(IndicadorApuracaoEscala), false, PersistentValue = true, RuleName = "CRE2")]
        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "CRE1")]
        [SMCConditionalRequired(nameof(IndicadorApuracaoEscala), true, RuleName = "CRQ1")]
        [SMCConditionalRequired(nameof(SomenteLeitura), false, RuleName = "CRQ2")]
        [SMCConditionalRule("CRE1 || CRE2")]
        [SMCConditionalRule("CRQ1 && CRQ2")]
        [SMCDependency(nameof(Nota), nameof(HistoricoEscolarController.BuscarEscalaApuracaoItemNota), "HistoricoEscolar", false, nameof(SeqEscalaApuracao))]
        [SMCDependency(nameof(SeqEscalaApuracao), nameof(HistoricoEscolarController.BuscarEscalaApuracao), "HistoricoEscolar", false)]
        [SMCSelect(nameof(EscalaApuracaoItens), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqEscalaApuracaoItem { get; set; }

        [SMCHidden]
        public bool IndicadorApuracaoFrequencia { get; set; }

        [SMCConditionalReadonly(nameof(IndicadorApuracaoFrequencia), false, PersistentValue = true, RuleName = "CRF2")]
        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "CRF1")]
        [SMCConditionalRequired(nameof(IndicadorApuracaoFrequencia), true, RuleName = "CRQ1")]
        [SMCConditionalRequired(nameof(SomenteLeitura), false, RuleName = "CRQ2")]
        [SMCConditionalRule("CRF1 || CRF2")]
        [SMCConditionalRule("CRQ1 && CRQ2")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public short? Faltas { get; set; }

        [SMCHidden]
        public short? PercentualMinimoFrequencia { get; set; }

        [SMCHidden]
        public short? PercentualMinimoNota { get; set; }

        [SMCHidden]
        public short? CargaHoraria { get; set; }

        [SMCHidden]
        public short? Credito { get; set; }

        [SMCDependency(nameof(Faltas), nameof(HistoricoEscolarController.CalcularSituacaoFinal), "HistoricoEscolar", false, includedProperties: new[] { nameof(Nota), nameof(SeqEscalaApuracaoItem), nameof(CargaHoraria), nameof(IndicadorApuracaoEscala), nameof(IndicadorApuracaoFrequencia), nameof(IndicadorApuracaoNota), nameof(NotaMaxima), nameof(PercentualMinimoFrequencia), nameof(PercentualMinimoNota), nameof(SeqComponenteCurricular), nameof(SeqCriterioAprovacao), nameof(SeqEscalaApuracao), nameof(TipoArredondamento) })]
        [SMCDependency(nameof(Nota), nameof(HistoricoEscolarController.CalcularSituacaoFinal), "HistoricoEscolar", false, includedProperties: new[] { nameof(SeqEscalaApuracaoItem), nameof(Faltas), nameof(CargaHoraria), nameof(IndicadorApuracaoEscala), nameof(IndicadorApuracaoFrequencia), nameof(IndicadorApuracaoNota), nameof(NotaMaxima), nameof(PercentualMinimoFrequencia), nameof(PercentualMinimoNota), nameof(SeqComponenteCurricular), nameof(SeqCriterioAprovacao), nameof(SeqEscalaApuracao), nameof(TipoArredondamento) })]
        [SMCDependency(nameof(SeqEscalaApuracaoItem), nameof(HistoricoEscolarController.CalcularSituacaoFinal), "HistoricoEscolar", false, includedProperties: new[] { nameof(Nota), nameof(Faltas), nameof(CargaHoraria), nameof(IndicadorApuracaoEscala), nameof(IndicadorApuracaoFrequencia), nameof(IndicadorApuracaoNota), nameof(NotaMaxima), nameof(PercentualMinimoFrequencia), nameof(PercentualMinimoNota), nameof(SeqComponenteCurricular), nameof(SeqCriterioAprovacao), nameof(SeqEscalaApuracao), nameof(TipoArredondamento) })]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string SituacaoFinal { get; set; }

        [SMCConditionalReadonly(nameof(SomenteLeitura), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<HistoricoEscolarColaboradorDetailViewModel> Colaboradores { get; set; }

        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        public string Observacao { get; set; }

        #endregion [ Campos ]

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.RequiredIncomingParameters(nameof(SeqAluno))
                   .HeaderIndex(nameof(HistoricoEscolarController.BuscarHistoricoEscolarCabecalho))
                   .IgnoreFilterGeneration()
                   .Detail<HistoricoEscolarListarDynamicModel>("_DetailList", g => g.SeqAlunoHistorico, "_DetailListHeader")
                   .EditInModal(refreshIndexPageOnSubmit: true)
                   .ViewPartialEdit("_Edit")
                   .ViewPartialInsert("_Edit")
                   .ModalSize(SMCModalWindowSize.Large)
                   .ButtonBackIndex("Index", "Aluno", model => new { Area = "ALN" })
                   .Service<IHistoricoEscolarService>(index: nameof(IHistoricoEscolarService.BuscarHistoricosEscolares),
                                                      insert: nameof(IHistoricoEscolarService.BuscarConfiguracaoHistoricoEscolarComponente),
                                                      save: nameof(IHistoricoEscolarService.SalvarHistoricoEscolarComponente),
                                                      edit: nameof(IHistoricoEscolarService.BuscarHistoricoEscolarComponente),
                                                      delete: nameof(IHistoricoEscolarService.ExcluirHistoricoEscolarComponente))
                   .Tokens(tokenInsert: new[] { UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES },
                           tokenEdit: new[] { UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES },
                           tokenList: new[] { UC_APR_002_08_01.PESQUISAR_HISTORICO_ESCOLAR },
                           tokenRemove: new[] { UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES });
        }

        #endregion [ Configuração ]
    }
}