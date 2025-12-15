using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using SMC.SGA.Administrativo.Areas.ALN.Views.InstituicaoNivelTipoVinculoAluno.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    [SMCStepConfiguration(ActionStep = nameof(InstituicaoNivelTipoVinculoAlunoController.VinculoParametros))]
    [SMCStepConfiguration(ActionStep = nameof(InstituicaoNivelTipoVinculoAlunoController.FormaIngresso))]
    [SMCStepConfiguration(ActionStep = nameof(InstituicaoNivelTipoVinculoAlunoController.TipoTermoIntercambio))]
    [SMCStepConfiguration(ActionStep = nameof(InstituicaoNivelTipoVinculoAlunoController.SituacaoMatricula))]
    [SMCStepConfiguration(ActionStep = nameof(InstituicaoNivelTipoVinculoAlunoController.Confirmacao), Partial = "_DadosConfirmacao", UseOnTabs = false)] 
    public class InstituicaoNivelTipoVinculoAlunoDynamicModel : SMCDynamicViewModel, ISMCWizardViewModel, ISMCStatefulView, ISMCMappable
    {
        [SMCIgnoreProp]
        public int Step { get; set; }

        #region DataSource 

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoDaInstituicaoSelect))]
        public List<SMCDatasourceItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTiposVinculoAlunoSelect))]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTermoIntercambioService), nameof(ITipoTermoIntercambioService.BuscarTiposTermosIntercambiosSelect))]
        public List<SMCDatasourceItem> TiposTermosIntercambios { get; set; }

        [SMCDataSource]
        [SMCHidden]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ISituacaoMatriculaService), nameof(ISituacaoMatriculaService.BuscarSituacoesMatriculasSelect))]
        public List<SMCDatasourceItem> SituacoesMatriculaSelect { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(IFormaIngressoService), nameof(IFormaIngressoService.BuscarFormasIngressoSelect), values: new string[] { nameof(SeqTipoVinculoAluno) })]
        public List<SMCDatasourceItem> FormasIngressoSelect { get; set; }

        [SMCDataSource(SMCStorageType.Session)]
        [SMCServiceReference(typeof(ITipoProcessoSeletivoService), nameof(ITipoProcessoSeletivoService.BuscarTiposProcessoSeletivoSelect), values: new string[] { nameof(SeqTipoVinculoAluno) })]
        public List<SMCDatasourceItem> TiposProcessoSeletivoSelect { get; set; }        

        #endregion

        #region Instituicao Nivel Tipo Vinculo Aluno

        [SMCKey]
        [SMCHidden]
        [SMCStep(0, 0)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCSelect("NiveisEnsino")]
        [SMCStep(0, 0)]
        [SMCReadOnly(SMCViewMode.Edit)]
        public long SeqInstituicaoNivel { get; set; }

        [SMCRequired]
        [SMCOrder(1)]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCSelect("TiposVinculoAluno")]
        [SMCReadOnly(SMCViewMode.Edit)]
        public long SeqTipoVinculoAluno { get; set; }

        //[SMCSize(SMCSize.Grid10_24)]
        //[SMCSelect]
        //[SMCOrder(2)]
        //[SMCStep(0)]
        //[SMCRequired]
        //public long TipoEntidadeIngressante { get; set; } 

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCOrder(4)]
        [SMCStep(0, 0)]
        [SMCRadioButtonList]
        public bool? ExigeParceriaIntercambioIngresso { get; set; }

        [SMCOrder(5)]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid5_24)]
        [SMCRequired]
        [SMCRadioButtonList]
        public bool? ExigeCurso { get; set; }

        [SMCOrder(7)]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid5_24)]
        [SMCRequired]
        [SMCRadioButtonList]
        public bool? ConcedeFormacao { get; set; }

        [SMCOrder(6)]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid6_24)]
        [SMCRequired]
        [SMCRadioButtonList]
        public bool? ExigeOfertaMatrizCurricular { get; set; }

        [SMCOrder(8)]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid8_24)]
        [SMCMinValue(0)]
        [SMCRequired]
        [SMCMask("9999")]
        public short? QuantidadeOfertaCampanhaIngresso { get; set; }

        [SMCOrder(9)]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCSelect]
        public TipoCobranca TipoCobranca { get; set; }

        [SMCOrder(10)]
        [SMCStep(0, 0)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCRadioButtonList]
        public bool? PossuiValorFixoMatricula { get; set; }

        #endregion

        #region Instituicao Nivel Forma Ingresso
         
        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCDetail(SMCDetailType.Modal ,min: 1)]
        [SMCStep(1, 1)]
        [SMCMapForceFromTo]
        [SMCDataSource]
        public SMCMasterDetailList<InstituicaoNivelFormaIngressoViewModel> FormasIngresso { get; set; }

        #endregion

        #region Tipo de Termo de Intercâmbio

        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail]
        [SMCStep(2, 2)]
        public SMCMasterDetailList<InstituicaoNivelTipoTermoIntercambioViewModel> TiposTermoIntercambio { get; set; }

        #endregion

        #region  Situação de matrícula

        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCDetail(min: 1)]
        [SMCStep(3, 3)]
        public SMCMasterDetailList<InstituicaoNivelSituacaoMatriculaViewModel> SituacoesMatricula { get; set; }

        #endregion

        #region  Confirmação

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string NivelEnsinoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string DescricaoVinculoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public string ExigeParceriaConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public string ExigeCursoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public string ConcedeFormacaoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public string ExigeOfertaMatrizCurricularConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public string QuantidadeOfertasConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public string TipoDeCobrancaConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid8_24)]
        public string PossuiValorFixoMatriculaConfirmacao { get; set; }

        [SMCIgnoreProp]
        public List<InstituicaoNivelFormaIngressoConfirmacaoViewModel> FormasIngressoConfirmacao { get; set; }

        [SMCIgnoreProp]
        public List<InstituicaoNivelTipoTermoIntercambioConfirmacaoViewModel> TiposTermoIntercambioConfirmacao { get; set; }

        [SMCIgnoreProp]
        public List<InstituicaoNivelSituacaoMatriculaConfirmacaoViewModel> SituacoesMatriculaConfirmacao { get; set; }
         
        #endregion

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Javascript("Areas/ALN/InstituicaoNivelTipoVinculoAluno")
                .Wizard(editMode: SMCDynamicWizardEditMode.Tab) 
                .Service<IInstituicaoNivelTipoVinculoAlunoService>(
                      index: nameof(IInstituicaoNivelTipoVinculoAlunoService.ListarInstituicaoNivelTipoVinculoAluno),
                      edit: nameof(IInstituicaoNivelTipoVinculoAlunoService.AlterarInstituicaoNivelTipoVinculoAluno),
                      save: nameof(IInstituicaoNivelTipoVinculoAlunoService.SalvarInstituicaoNivelTipoVinculoAluno)
                 )
                .Detail<InstituicaoNivelTipoVinculoAlunoListarDynamicModel>("_DetailList")
                .Messages(x => string.Format(UIResource.Listar_Excluir_Confirmacao,
                                ((InstituicaoNivelTipoVinculoAlunoListarDynamicModel)x).NivelEnsino))
                .Tokens(tokenList: UC_ALN_003_01_01.PESQUISAR_VINCULO_INSTITUICAO_NIVEL,
                        tokenEdit: UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL,
                        tokenRemove: UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL,
                        tokenInsert: UC_ALN_003_01_02.MANTER_VINCULO_INSTITUICAO_NIVEL);
        } 

        #endregion
    }
}