using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Inscricoes.UI.Mvc.Areas.INS.Lookups;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoSelect))]
        public List<SMCSelectListItem> NiveisEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICicloLetivoService), nameof(ICicloLetivoService.BuscarCiclosLetivosPorCampanhaSelect), values: new[] { nameof(SeqCampanha) })]
        public List<SMCSelectListItem> CiclosLetivos { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoProcessoSeletivoService), nameof(ITipoProcessoSeletivoService.BuscarTiposProcessoSeletivoPorNivelEnsinoSelect), values: new[] { nameof(SeqsNivelEnsino) })]
        public List<SMCSelectListItem> TiposProcessoSeletivo { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoVinculoAlunoService), nameof(ITipoVinculoAlunoService.BuscarTipoVinculoAlunoPorTipoProcessoSeletivo), values: new[] { nameof(SeqTipoProcessoSeletivo) })]
        public List<SMCSelectListItem> TiposVinculoAluno { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IFormaIngressoService), nameof(IFormaIngressoService.BuscarFormasIngressoInstituicaoNivelVinculoSelect), values: new[] { nameof(SeqsNivelEnsino), nameof(SeqTipoProcessoSeletivo), nameof(SeqTipoVinculoAluno) })]
        public List<SMCSelectListItem> FormasIngresso { get; set; }

        #endregion DataSources

        [SMCHidden]
        public long SeqCampanha { get; set; }

        [SMCKey]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(NiveisEnsino))]
        [SMCSize(SMCSize.Grid8_24)]
        public List<long> SeqsNivelEnsino { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(TiposProcessoSeletivo))]
        [SMCDependency(nameof(SeqsNivelEnsino), nameof(TipoProcessoSeletivoController.BuscarTipoProcessoPorNiveislEnsino), "TipoProcessoSeletivo", true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTipoProcessoSeletivo { get; set; }

        [SMCSelect(nameof(TiposVinculoAluno), AutoSelectSingleItem = true)]
        [SMCConditionalReadonly(nameof(SeqTipoProcessoSeletivo), "true", DataAttribute = "ingresso-direto")]
        [SMCConditionalRequired(nameof(SeqTipoProcessoSeletivo), "false", DataAttribute = "ingresso-direto")]
        [SMCDependency(nameof(SeqTipoProcessoSeletivo), nameof(ProcessoSeletivoController.BuscarTipoVinculo), "ProcessoSeletivo", true)]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqTipoVinculoAluno { get; set; }

        [SMCSelect(nameof(FormasIngresso), AutoSelectSingleItem = true)]
        [SMCConditionalReadonly(nameof(SeqTipoProcessoSeletivo), "true", DataAttribute = "ingresso-direto")]
        [SMCConditionalRequired(nameof(SeqTipoProcessoSeletivo), "false", DataAttribute = "ingresso-direto")]
        // Comportamento Sobrepostos dos eventos
        //[SMCDependency(nameof(SeqNivelEnsino), nameof(ProcessoSeletivoController.BuscarFormaIngresso), "ProcessoSeletivo", true, new string[] { nameof(SeqTipoProcessoSeletivo), nameof(SeqTipoVinculoAluno) })]
        //[SMCDependency(nameof(SeqTipoProcessoSeletivo), nameof(ProcessoSeletivoController.BuscarFormaIngresso), "ProcessoSeletivo", true, new string[] { nameof(SeqNivelEnsino), nameof(SeqTipoVinculoAluno) })]
        [SMCDependency(nameof(SeqTipoVinculoAluno), nameof(ProcessoSeletivoController.BuscarFormaIngresso), "ProcessoSeletivo", true, new string[] { nameof(SeqsNivelEnsino), nameof(SeqTipoProcessoSeletivo) })]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqFormaIngresso { get; set; }

        [LookupProcesso]
        [SMCConditionalReadonly(nameof(SeqTipoProcessoSeletivo), SMCConditionalOperation.NotEqual, "false", DataAttribute = "ingresso-direto")]
        [SMCDependency(nameof(AnoReferencia))]
        [SMCDependency(nameof(UnidadeResponsavel))]
        [SMCDependency(nameof(SemestreReferencia))]
        [SMCSize(SMCSize.Grid16_24)]
        public GPILookupViewModel SeqProcessoGpi { get; set; }

        [SMCConditionalReadonly(nameof(SeqTipoProcessoSeletivo), SMCConditionalOperation.NotEqual, TOKEN_TIPO_PROCESSO_SELETIVO.DISCIPLINA_ISOLADA, DataAttribute = "tipo")]
        [SMCConditionalRequired(nameof(SeqTipoProcessoSeletivo), TOKEN_TIPO_PROCESSO_SELETIVO.DISCIPLINA_ISOLADA, DataAttribute = "tipo")]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24)]
        public bool? ReservaVaga { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ProcessoSeletivoProcessoMatriculaViewModel> ProcessosMatricula { get; set; }

        [SMCHidden]
        public long? UnidadeResponsavel { get; set; }

        [SMCHidden]
        public int? AnoReferencia { get; set; }

        [SMCHidden]
        public int? SemestreReferencia { get; set; }

        #region [ Configuração ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenEdit: UC_CAM_001_02_02.MANTER_PROCESSO_SELETIVO,
                           tokenInsert: UC_CAM_001_02_02.MANTER_PROCESSO_SELETIVO,
                           tokenRemove: UC_CAM_001_02_02.MANTER_PROCESSO_SELETIVO,
                           tokenList: UC_CAM_001_02_01.PESQUISAR_PROCESSO_SELETIVO)
                   .HeaderIndex(nameof(ProcessoSeletivoController.CabecalhoProcessoSeletivo))
                   .Ajax()
                   .Service<IProcessoSeletivoService>(index: nameof(IProcessoSeletivoService.BuscarProcessosSeletivos),
                                                      edit: nameof(IProcessoSeletivoService.BuscarProcessosSeletivo),
                                                      save: nameof(IProcessoSeletivoService.SalvarProcessoSeletivo),
                                                      insert: nameof(IProcessoSeletivoService.NovoProcessosSeletivo))
                   .Button("CadastrarOfertasProcessoSeletivo", nameof(ProcessoSeletivoOfertaController.Index), "ProcessoSeletivoOferta",
                                (model) => new
                                {
                                    seqProcessoSeletivo = (SMCEncryptedLong)(model as ProcessoSeletivoListarDynamicModel).Seq,
                                    seqCampanha = (SMCEncryptedLong)(model as ProcessoSeletivoListarDynamicModel).SeqCampanha
                                }, htmlAttributes: new { @ref = "btn_cadastro" })
                   //.Button("Convocacao", "", "",
                   //             (model) => new {
                   //                 seqProcessoSeletivo = (SMCEncryptedLong)(model as ProcessoSeletivoListarDynamicModel).Seq,
                   //                 seqCampanha = (SMCEncryptedLong)(model as ProcessoSeletivoListarDynamicModel).SeqCampanha
                   //             }, htmlAttributes: new { @ref = "btn_convocacao" })
                   //.Button("ConsultarCandidatos", "", "",
                   //             (model) => new {
                   //                seqProcessoSeletivo = (SMCEncryptedLong)(model as ProcessoSeletivoListarDynamicModel).Seq,
                   //                seqCampanha = (SMCEncryptedLong)(model as ProcessoSeletivoListarDynamicModel).SeqCampanha
                   //             }, htmlAttributes: new { @ref = "btn_consulta" })
                   .ConfigureButton((fluent, model, action) =>
                   {
                       if (fluent.Options.HtmlAttributes.ContainsKey("ref"))
                       {
                           var btn = fluent.Options.HtmlAttributes["ref"];
                           switch (btn)
                           {
                               case "btn_convocacao":
                                   var item = model as ProcessoSeletivoListarDynamicModel;
                                   if (item.IngressoDireto)
                                   {
                                       fluent.Enabled(false);
                                       fluent.ButtonInstructions(Views.ProcessoSeletivo.App_LocalResources.UIResource.MSG_TipoProcessoIngressoDireto);
                                   }
                                   break;
                           }
                       }
                   })
                   .ButtonBackIndex("Index", "Campanha");
        }

        #endregion [ Configuração ]
    }
}