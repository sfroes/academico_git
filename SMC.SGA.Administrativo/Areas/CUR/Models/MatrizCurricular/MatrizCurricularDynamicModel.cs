using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CUR.Views.MatrizCurricular.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    [SMCStepConfiguration]
    [SMCStepConfiguration]
    [SMCStepConfiguration(ActionStep = "OfertasMatriz", Partial = "_DadosConfirmacao", UseOnTabs = false)]
    public class MatrizCurricularDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        #region [ DataSource ]

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IDivisaoCurricularService), nameof(IDivisaoCurricularService.BuscarDivisoesCurricularesPorCurriculoCursoOferta), values: new string[] { nameof(SeqCurriculoCursoOferta) })]
        public List<SMCDatasourceItem> DivisoesCurricular { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IModalidadeService), nameof(IModalidadeService.BuscarModalidadesPorCurriculoCursoOfertaSelect), values: new string[] { nameof(SeqCurriculoCursoOferta) })]
        public List<SMCDatasourceItem> Modalidades { get; set; }

        [SMCDataSource("Localidades", storageType: SMCStorageType.Session)]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService), nameof(ICursoOfertaLocalidadeService.BuscarUnidadesLocalidadesPorCurriculoCursoOfertaSelect), values: new string[] { nameof(SeqCurriculoCursoOferta), nameof(SeqModalidade) })]
        public List<SMCDatasourceItem> Localidades { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IGrupoCurricularComponenteService), nameof(IGrupoCurricularComponenteService.BuscarComponentesCurricularesPadrao), values: new string[] { nameof(SeqCurriculoCursoOferta) })]
        public List<SMCDatasourceItem> ComponenteCurricularPadrao { get; set; }

        #endregion [ DataSource ]

        [SMCIgnoreProp]
        public int Step { get; set; }

        [SMCHidden]
        [SMCKey]
        [SMCStep(0)]
        public override long Seq { get; set; }

        // Matriz Ativa
        [SMCHidden]
        [SMCStep(0)]
        public bool Ativo { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCStep(0)]
        public long SeqCurriculoCursoOferta { get; set; }

      
        [SMCHidden]
        [SMCMapProperty(nameof(Seq))]
        [SMCParameter]
        [SMCStep(0)]
        public long SeqMatrizCurricular { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(0)]
        [SMCReadOnly]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(0)]
        public string Codigo { get; set; }

        [SMCHidden]
        [SMCStep(1)]
        public string CodigoMatrizCurricular { get { return Codigo; } }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCConditionalReadonly(nameof(Ativo), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("DivisoesCurricular")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid5_24)]
        [SMCStep(0)]
        public long SeqDivisaoCurricular { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCConditionalReadonly(nameof(Ativo), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCMask("999")]
        [SMCMinValue(1)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCStep(0)]
        public short? QuantidadeMesesPrevistoConclusao { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCConditionalReadonly(nameof(Ativo), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCMask("999")]
        [SMCMinValue(nameof(QuantidadeMesesPrevistoConclusao))]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCStep(0)]
        public short? QuantidadeMesesLimiteConclusao { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCMask("999")]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCStep(0)]
        public short? QuantidadeMesesSolicitacaoProrrogacao { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCConditionalReadonly(nameof(Ativo), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCOrder(5)]
        [SMCRequired]
        [SMCSelect("Modalidades")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCStep(0)]
        public long? SeqModalidade { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(6)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid10_24)]
        [SMCStep(0)]
        public string Descricao { get; set; }

        [SMCDependency(nameof(SeqModalidade), "PreencherDescricaoMatrizCurricular", "MatrizCurricular", "CUR", false, new string[] { nameof(SeqDivisaoCurricular) })]
        [SMCDependency(nameof(SeqDivisaoCurricular), "PreencherDescricaoMatrizCurricular", "MatrizCurricular", "CUR", false, new string[] { nameof(SeqModalidade) })]
        [SMCConditionalReadonly(nameof(Ativo), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(7)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        [SMCStep(0)]
        public string DescricaoComplementar { get; set; }

        [SMCDetail(SMCDetailType.Modal, min: 1, windowSize: SMCModalWindowSize.Largest)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(8)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(1)]
        public SMCMasterDetailList<MatrizCurricularOfertaViewModel> Ofertas { get; set; }

        [SMCHidden]
        public int NumeroSequencial { get; set; }

        [SMCHidden]
        public bool BloqueioAlteracao { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCOrder(9)]
        [SMCSelect("ComponenteCurricularPadrao")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid9_24)]
        [SMCStep(0)]
        [SMCRequired]
        public long SeqComponenteCurricularPadrao { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Detail<MatrizCurricularListarDynamicModel>("_DetailList", allowSort: false)
                   .HeaderIndex("CabecalhoCurriculoCursoOferta")
                   .Header("CabecalhoCurriculoCursoOferta")
                   .ButtonBackIndex("Index", "Curriculo")
                   .Assert("Confirmacao_ExclusaoOfertaVigente", model => (model as MatrizCurricularDynamicModel)
                                                                            .Ofertas
                                                                            .RemovedItems
                                                                            .OfType<MatrizCurricularOfertaViewModel>()
                                                                            .Any(a => a.DataInicioVigencia <= DateTime.Today && a.SeqDivisao != 0))
                   .Messages(model => (model as MatrizCurricularListarDynamicModel).ContemOfertaAtiva ? UIResource.Confirmacao_ExclusaoMatrizVigente : null)
                   .Button("MatrizCurricularConfiguracaoComponente",
                           "Index",
                           "MatrizCurricularConfiguracaoComponente",
                           UC_CUR_001_05_04.PESQUISAR_CONFIGURACAO_GRUPO_MATRIZ,
                           model => new
                           {
                               SeqMatrizCurricular = SMCDESCrypto.EncryptNumberForURL((model as ISMCSeq).Seq),
                               SeqCurriculoCursoOferta = SMCDESCrypto.EncryptNumberForURL((model as MatrizCurricularListarDynamicModel).SeqCurriculoCursoOferta)
                           })
                   .Button("DivisaoMatrizCurricularComponente",
                           "Index",
                           "ConfiguracaoComponenteMatriz",
                           UC_CUR_001_05_06.PESQUISAR_CONFIGURACAO_COMPONENTE_MATRIZ,
                           model => new
                           {
                               SeqMatrizCurricular = SMCDESCrypto.EncryptNumberForURL((model as ISMCSeq).Seq),
                               SeqCurriculoCursoOferta = SMCDESCrypto.EncryptNumberForURL((model as MatrizCurricularListarDynamicModel).SeqCurriculoCursoOferta)
                           })
                   .Button("ConsultaDivisoesMatrizCurricular",
                           "Index",
                           "ConsultaDivisoesMatrizCurricular",
                           UC_CUR_001_06_01.CONSULTA_DIVISOES_MATRIZ_CURRICULAR,
                           model => new
                           {
                               SeqMatrizCurricular = SMCDESCrypto.EncryptNumberForURL((model as ISMCSeq).Seq),
                               SeqCurriculoCursoOferta = SMCDESCrypto.EncryptNumberForURL((model as MatrizCurricularListarDynamicModel).SeqCurriculoCursoOferta)
                           })
                    .Button("RequisitosMatrizCurricular",
                           "Index",
                           "Requisito",
                           UC_CUR_003_01_01.PESQUISAR_REQUISITO,
                           model => new
                           {
                               SeqMatrizCurricular = SMCDESCrypto.EncryptNumberForURL((model as ISMCSeq).Seq),
                               SeqCurriculoCursoOferta = SMCDESCrypto.EncryptNumberForURL((model as MatrizCurricularListarDynamicModel).SeqCurriculoCursoOferta)
                           })
                   .Service<IMatrizCurricularService>(
                                                          edit: nameof(IMatrizCurricularService.BuscarMatrizCurricular),
                                                         index: nameof(IMatrizCurricularService.BuscarMatrizesCurricular),
                                                        insert: nameof(IMatrizCurricularService.BuscarConfiguracaoMatrizCurricular),
                                                          save: nameof(IMatrizCurricularService.SalvarMatrizCurricular))
                   .IgnoreFilterGeneration()
                   .Wizard(editMode: SMCDynamicWizardEditMode.Tab)
                   .Tokens(tokenList: UC_CUR_001_05_01.PESQUISAR_MATRIZ_CURRICULAR,
                           tokenInsert: UC_CUR_001_05_02.MANTER_MATRIZ_CURRICULAR,
                           tokenEdit: UC_CUR_001_05_02.MANTER_MATRIZ_CURRICULAR,
                           tokenRemove: UC_CUR_001_05_02.MANTER_MATRIZ_CURRICULAR);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new MatrizCurricularNavigationGroup(this);
        }

        #endregion [ Configurações ]
    }
}