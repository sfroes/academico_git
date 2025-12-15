using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoFormacaoEspecificaDynamicModel : SMCDynamicViewModel
    {
        #region DataSources

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoFormacaoEspecificaService), nameof(ITipoFormacaoEspecificaService.BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect), values: new string[] { nameof(SeqNivelEnsino), nameof(SeqInstituicaoEnsino), nameof(TipoCurso) })]
        public List<SMCDatasourceItem> TiposFormacaoEspecifica { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IGrauAcademicoService), nameof(IGrauAcademicoService.BuscarGrauAcademicoSelect), values: new string[] { nameof(SeqCurso), nameof(Ativo) })]
        public List<SMCDatasourceItem> GrauAcademicoSelect { get; set; }

        [SMCDataSource]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITitulacaoService), nameof(ITitulacaoService.BuscarTitulacoesSelect), values: new string[] { nameof(SeqCurso), nameof(SeqGrauAcademico), nameof(Ativo), nameof(CursoTipoFormacao), nameof(SeqCursoOferta), nameof(SeqCursoOuGrauAcademicoCurso), nameof(SeqCursoFormacaoEspecifica) })]
        public List<SMCSelectListItem> TitulacoesDatasource { get; set; }

        #endregion DataSources

        [SMCKey]
        [SMCHidden]
        [SMCOrder(0)]
        public override long Seq { get; set; }

        [SMCIgnoreProp]
        public long? SeqCursoFormacaoEspecifica => Seq;

        [SMCIgnoreProp]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        [SMCIgnoreProp]
        public string DescricaoGrauAcademico { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCRequired]
        [SMCOrder(1)]
        public long SeqCurso { get; set; }

        [SMCReadOnly]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid4_24)]
        public long SeqFormacaoEspecificaID { get; set; }

        [SMCHidden]
        [SMCOrder(2)]
        public CursoTipoFormacao CursoTipoFormacao { get; set; }

        [SMCOrder(3)]
        [SMCConditionalDisplay(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Cadastro_Simples), nameof(CursoTipoFormacao.Cadastro_Oferta))]
        [SMCConditionalRequired(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Cadastro_Simples), nameof(CursoTipoFormacao.Cadastro_Oferta))]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.Equals, 0, PersistentValue = true, RuleName = "SituacaoR01")]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true, RuleName = "SituacaoR02")]
        [SMCConditionalReadonly(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Cadastro_Oferta), RuleName = "SituacaoR03")]
        [SMCConditionalReadonly(nameof(CursoTipoFormacao), SMCConditionalOperation.NotEqual, nameof(CursoTipoFormacao.Cadastro_Oferta), RuleName = "SituacaoR04")]
        [SMCConditionalRule("(SituacaoR02 && SituacaoR04) || (SituacaoR01 && SituacaoR03) || (SituacaoR02 && SituacaoR03)")]
        [SMCSelect(nameof(TiposFormacaoEspecifica), autoSelectSingleItem: true, NameDescriptionField = nameof(DescricaoTipoFormacaoEspecifica))]
        [SMCSize(SMCSize.Grid10_24)]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoFormacaoEspecifica), nameof(CursoFormacaoEspecificaController.TipoFormacaoEspecificaRequired), "CursoFormacaoEspecifica", false, new string[] { nameof(SeqFormacaoEspecifica) })]
        [SMCDependency(nameof(SeqFormacaoEspecifica), nameof(CursoFormacaoEspecificaController.TipoFormacaoEspecificaRequired), "CursoFormacaoEspecifica", false, new string[] { nameof(SeqTipoFormacaoEspecifica) })]
        public bool GrauAcademicoRequerido { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoFormacaoEspecifica), nameof(CursoFormacaoEspecificaController.TipoFormacaoEspecificaExigeTitulacao), "CursoFormacaoEspecifica", false, new string[] { nameof(SeqFormacaoEspecifica) })]
        [SMCDependency(nameof(SeqFormacaoEspecifica), nameof(CursoFormacaoEspecificaController.TipoFormacaoEspecificaExigeTitulacao), "CursoFormacaoEspecifica", false, new string[] { nameof(SeqTipoFormacaoEspecifica) })]
        public bool TitulacaoRequeridoPorFormacao { get; set; }

        [SMCOrder(6)]
        [SMCConditionalDisplay(nameof(CursoTipoFormacao), SMCConditionalOperation.NotEqual, nameof(CursoTipoFormacao.Cadastro_Oferta))]
        [SMCConditionalReadonly(nameof(GrauAcademicoRequerido), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(GrauAcademicoRequerido), SMCConditionalOperation.Equals, true)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCSelect(nameof(GrauAcademicoSelect), autoSelectSingleItem: true, NameDescriptionField = nameof(DescricaoGrauAcademico))]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCMapForceFromTo]
        public long? SeqGrauAcademico { get; set; }

        [SMCOrder(7)]
        [SMCConditionalDisplay(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Cadastro_Simples))]
        [SMCConditionalRequired(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Cadastro_Simples))]
        [SMCDependency(nameof(SeqGrauAcademico), nameof(CursoFormacaoEspecificaController.BuscarDescricaoFormacaoEspecificaCadastroSimples), "CursoFormacaoEspecifica", false, nameof(SeqTipoFormacaoEspecifica))]
        [SMCDependency(nameof(SeqTipoFormacaoEspecifica), nameof(CursoFormacaoEspecificaController.BuscarDescricaoFormacaoEspecificaCadastroSimples), "CursoFormacaoEspecifica", false, nameof(SeqGrauAcademico))]
        [SMCDescription]
        [SMCMaxLength(250)]
        [SMCSize(SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        /// <summary>
        /// Campo para ser enviado como parâmetro do lookup
        /// </summary>
        [SMCHidden]
        public bool SelecaoNivelFolha { get; set; } = true;

        [SMCOrder(4)]
        [SMCConditionalDisplay(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Selecao_Formacao))]
        [SMCConditionalRequired(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Selecao_Formacao))]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid20_24)]
        [SMCDependency(nameof(SeqsEntidadesResponsaveis))]
        [SMCDependency(nameof(SelecaoNivelFolha))]
        [SMCDependency(nameof(SeqCurso))]
        [FormacaoEspecificaLookup]
        public FormacaoEspecificaLookupViewModel SeqFormacaoEspecifica { get; set; }

        [SMCConditionalDisplay(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Cadastro_Oferta))]
        [SMCConditionalRequired(nameof(CursoTipoFormacao), SMCConditionalOperation.Equals, nameof(CursoTipoFormacao.Cadastro_Oferta))]
        [SMCConditionalReadonly(nameof(Seq), SMCConditionalOperation.NotEqual, 0, PersistentValue = true)]
        [CursoOfertaLookup]
        [SMCDependency(nameof(SeqNivelEnsino))]
        [SMCDependency(nameof(SeqCurso))]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCOrder(5)]
        public CursoOfertaLookupViewModel SeqCursoOferta { get; set; }

        [SMCOrder(8)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCOrder(9)]
        [SMCMinDate(nameof(DataInicioVigencia))]
        public DateTime? DataFimVigencia { get; set; }

        [SMCHidden]
        [SMCOrder(10)]
        public List<long> SeqNivelEnsino { get; set; }

        // Fixo Curso?
        [SMCIgnoreProp]
        public ClasseTipoFormacao ClasseTipoFormacao { get; set; } = ClasseTipoFormacao.Curso;

        [SMCHidden]
        public TipoCurso TipoCurso { get; set; }

        [SMCHidden]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCHidden]
        public bool Ativo => true;

        [SMCIgnoreProp]
        public bool SeqCursoOuGrauAcademicoCurso => true;

        [SMCDetail]
        [SMCOrder(11)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalDisplay(nameof(TitulacaoRequeridoPorFormacao), SMCConditionalOperation.Equals, true)]
        public SMCMasterDetailList<CursoFormacaoEspecificaTitulacaoDetailViewModel> Titulacoes { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            Func<SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>, string> retCss = (model) =>
            {
                List<string> ret = new List<string>();

                if (!model.Value.Ativo)
                    ret.Add("smc-sga-item-inativo");

                if (model.Value.TipoCursoFormacaoEspecificaFolha && !model.Value.PossuiOfertaCursoLocalidade)
                    ret.Add("smc-sga-item-nao-associado");

                return string.Join(" ", ret);
            };

            options.IgnoreFilterGeneration()
                   .ButtonBackIndex("Index", "Curso")
                   .Header("CabecalhoCursoFormacaoEspecifica")
                   .HeaderIndex("CabecalhoCursoFormacaoEspecifica")
                   .HeaderIndexList("MensagemCursoFormacaoEspecifica")
                   .Service<ICursoFormacaoEspecificaService>(insert: nameof(ICursoFormacaoEspecificaService.ConfigurarCursoFormacaoEspecifica),
                                                             index: nameof(ICursoFormacaoEspecificaService.BuscarCursoFormacoesEspecificas),
                                                             edit: nameof(ICursoFormacaoEspecificaService.BuscarCursoFormacaoEspecifica),
                                                             delete: nameof(ICursoFormacaoEspecificaService.ExcluirCursoFormacaoEspecifica),
                                                             save: nameof(ICursoFormacaoEspecificaService.SalvarCursoFormacaoEspecifica))
                   .RequiredIncomingParameters(nameof(SeqCurso))
                   .TreeView(configureNode: x => (x as SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>).Options.CssClass = retCss(x as SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>))
                   .Button(idResource: "ReplicarCursoFormacaoEspecifica",
                        action: "ReplicarCursoFormacaoEspecifica",
                        controller: "CursoFormacaoEspecifica",
                        securityToken: UC_CSO_001_01_06.REPLICAR_FORMACAO_ESPECIFICA_CURSO,
                        isModal: true,
                        htmlAttributes: new { data_modal_title = "Replicar Formação Específica Curso", data_modal_submiturl = "CursoFormacaoEspecifica/SalvarReplicarCursoFormacaoEspecifica" },
                        buttonBehavior: SMCButtonBehavior.Alterar,
                        displayButton: x =>
                            (x as SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>).Value.Ativo &&
                            (x as SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>).Value.TipoCursoFormacaoEspecificaFolha,
                        routes: x => new { SeqCurso = new SMCEncryptedLong((x as SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>).Value.SeqCurso), SeqFormacaoEspecifica = new SMCEncryptedLong((x as SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>).Value.Seq) })
                   .EditInModal()
                   .ModalSize(SMCModalWindowSize.Largest)
                   .ConfigureContextMenuButton((button, model) =>
                   {
                       button.Hide(!(model as SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>).Value.SeqCursoFormacaoEspecifica.HasValue);
                       //FIX: Remover ao corrigir a tree
                       button.DisplayMode(SMCButtonDisplayMode.Icon);
                   })
                   .ConfigureContextMenuItem((button, model, action) =>
                   {
                       var cursoFormacao = (model as SMCTreeViewNode<CursoFormacaoEspecificaListarDynamicModel>).Value;
                       switch (action)
                       {
                           case SMCDynamicButtonAction.Insert:
                               button.Hide();
                               break;

                           case SMCDynamicButtonAction.Edit:
                               button.Action("Editar", "CursoFormacaoEspecifica", new { seq = new SMCEncryptedLong(cursoFormacao.SeqCursoFormacaoEspecifica.GetValueOrDefault()) });
                               break;

                           case SMCDynamicButtonAction.Remove:
                               button.Action("Excluir", "CursoFormacaoEspecifica", new
                               {
                                   seq = new SMCEncryptedLong(cursoFormacao.SeqCursoFormacaoEspecifica.GetValueOrDefault()),
                                   seqCurso = new SMCEncryptedLong(cursoFormacao.SeqCurso)
                               });
                               break;
                       }
                   })
                   .Tokens(tokenEdit: UC_CSO_001_01_05.MANTER_FORMACAO_ESPECIFICA_CURSO,
                           tokenInsert: UC_CSO_001_01_05.MANTER_FORMACAO_ESPECIFICA_CURSO,
                           tokenRemove: UC_CSO_001_01_05.MANTER_FORMACAO_ESPECIFICA_CURSO,
                           tokenList: UC_CSO_001_01_04.PESQUISAR_FORMACAO_ESPECIFICA_CURSO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            if (viewMode == SMCViewMode.Insert)
            {
                this.Titulacoes = new SMCMasterDetailList<CursoFormacaoEspecificaTitulacaoDetailViewModel>
                {
                    DefaultModel = new CursoFormacaoEspecificaTitulacaoDetailViewModel() { Ativo = true }
                };
            }

            if (viewMode == SMCViewMode.Edit)
                this.Titulacoes.DefaultModel = new CursoFormacaoEspecificaTitulacaoDetailViewModel() { Ativo = true };
        }
        #endregion [ Configurações ]
    }
}