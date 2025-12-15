using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class CurriculoCursoOfertaGrupoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        [SMCOrder(1)]
        [SMCRequired]
        public override long Seq { get; set; }

        [SMCHidden]
        public bool DesconsiderarItensVinculadosAoCurriculoCursoOferta => true;

        [GrupoCurricularLookup]
        [SMCDependency(nameof(DesconsiderarItensVinculadosAoCurriculoCursoOferta))]
        [SMCDependency(nameof(SeqCurriculoCursoOferta))]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public GrupoCurricularLookupViewModel SeqGrupoCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOferta { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqCurriculoCursoOfertaGrupo { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid12_24)]
        public CurriculoCursoOfertaGrupoComponenteObrigatorio Obrigatorio { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(4)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public bool ExibidoHistoricoEscolar { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(5)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public bool DesconsiderarIntegralizacao { get; set; }

        #region [ Quantidades disponíveis ]

        /// <summary>
        /// Quantidades de carga horária e créditos disponíveis na oferta de curso.
        /// </summary>
        public CurriculoCursoOfertaGrupoValorViewModel QuantidadesDisponiveis { get; set; }

        /// <summary>
        /// Quantidades de carga horária e créditos do grupo currícular selecionado
        /// </summary>
        public CurriculoCursoOfertaGrupoValorViewModel QuantidadesGrupoCurricularSelecionado { get; set; }

        #endregion [ Quantidades disponíveis ]

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal(allowSaveNew: false, refreshIndexPageOnSubmit: true)
                   .ModalSize(SMCModalWindowSize.Large)
                   .HeaderIndex(nameof(CurriculoCursoOfertaGrupoController.CabecalhoCurriculoCursoOferta))
                   .IgnoreFilterGeneration()
                   .ButtonBackIndex("Index", "Curriculo")
                   .ViewPartialEdit("_Edit")
                   .ViewPartialInsert("_Edit")
                   .Assert("MSG_QuantidadeDisponivelUltrapassada", Model => { return ValidarQuantidadesUltrapassadas(Model as CurriculoCursoOfertaGrupoDynamicModel); })
                   .ConfigureContextMenuButton((button, model) =>
                   {
                       button.Hide((model as SMCTreeViewNode<CurriculoCursoOfertaGrupoListarDynamicModel>).Value.Folha).DisplayMode(SMCButtonDisplayMode.Icon);
                   })
                   .ConfigureContextMenuItem((button, model, action) => button.Hide(action == SMCDynamicButtonAction.Insert || action == SMCDynamicButtonAction.View))
                   .Service<ICurriculoCursoOfertaGrupoService>(index: nameof(ICurriculoCursoOfertaGrupoService.BuscarGruposCurricularesTreeCurriculoCursoOferta),
                                                              insert: nameof(ICurriculoCursoOfertaGrupoService.BuscarQuantidadesDisponiveis),
                                                                save: nameof(ICurriculoCursoOfertaGrupoService.SalvarCurriculoCursoOfertaGrupo),
                                                                edit: nameof(ICurriculoCursoOfertaGrupoService.BuscarCurriculoCursoOfertaGrupo),
                                                              delete: nameof(ICurriculoCursoOfertaGrupoService.ExcluirCurriculoCursoOfertaGrupo))
                   .TreeView(treeOpen: false, configureNode: x => !(x as SMCTreeViewNode<CurriculoCursoOfertaGrupoListarDynamicModel>).Value.Folha ? (x as SMCTreeViewNode<CurriculoCursoOfertaGrupoListarDynamicModel>).Options.CssClass = "smc-sga-treeview-item-destaque" : null)
                   .Tokens(tokenInsert: UC_CUR_001_01_06.ASSOCIAR_GRUPO_CURRICULAR_OFERTA_CURSO,
                           tokenEdit: UC_CUR_001_01_06.ASSOCIAR_GRUPO_CURRICULAR_OFERTA_CURSO,
                           tokenRemove: UC_CUR_001_01_06.ASSOCIAR_GRUPO_CURRICULAR_OFERTA_CURSO,
                           tokenList: UC_CUR_001_01_05.PESQUISAR_ASSOCIACAO_GRUPO_CURRICULAR_OFERTA_CURSO);
        }

        public override void InitializeModel(SMCViewMode viewMode)
        {
            base.InitializeModel(viewMode);

            if (this.QuantidadesDisponiveis == null)
                this.QuantidadesDisponiveis = new CurriculoCursoOfertaGrupoValorViewModel();
            if (this.QuantidadesGrupoCurricularSelecionado == null)
                this.QuantidadesGrupoCurricularSelecionado = new CurriculoCursoOfertaGrupoValorViewModel();
            this.QuantidadesDisponiveis.Prefixo = nameof(this.QuantidadesDisponiveis);
            this.QuantidadesGrupoCurricularSelecionado.Prefixo = nameof(this.QuantidadesGrupoCurricularSelecionado);

            if (viewMode == SMCViewMode.Insert)
                this.ExibidoHistoricoEscolar = true;
        }

        #endregion [ Configurações ]

        /// <summary>
        /// Valida se o grupo selecionado ultrapassa algum dos valores disponíveis
        /// </summary>
        /// <param name="model">Modelo submetido para o assert</param>
        /// <returns>Verdadeiro caso algum dos valores esteja além do disponível</returns>
        private bool ValidarQuantidadesUltrapassadas(CurriculoCursoOfertaGrupoDynamicModel model)
        {
            int quantidadeCreditos = (model.QuantidadesGrupoCurricularSelecionado?.QuantidadeCreditoObrigatorio ?? 0) + (model.QuantidadesGrupoCurricularSelecionado?.QuantidadeCreditoOptativo ?? 0);
            int quantidadeHorasAula = (model.QuantidadesGrupoCurricularSelecionado?.QuantidadeHoraAulaObrigatoria ?? 0) + (model.QuantidadesGrupoCurricularSelecionado?.QuantidadeHoraAulaOptativa ?? 0);
            int quantidadeHorasAulaRelogio = (model.QuantidadesGrupoCurricularSelecionado?.QuantidadeHoraRelogioObrigatoria ?? 0) + (model.QuantidadesGrupoCurricularSelecionado?.QuantidadeHoraRelogioOptativa ?? 0);

            return model.Obrigatorio == CurriculoCursoOfertaGrupoComponenteObrigatorio.Obrigatorios ?
                quantidadeCreditos > (model.QuantidadesDisponiveis?.QuantidadeCreditoObrigatorio ?? int.MaxValue) ||
                quantidadeHorasAula > (model.QuantidadesDisponiveis?.QuantidadeHoraAulaObrigatoria ?? int.MaxValue) ||
                quantidadeHorasAulaRelogio > (model.QuantidadesDisponiveis?.QuantidadeHoraRelogioObrigatoria ?? int.MaxValue)
                :
                quantidadeCreditos > (model.QuantidadesDisponiveis?.QuantidadeCreditoOptativo ?? int.MaxValue) ||
                quantidadeHorasAula > (model.QuantidadesDisponiveis?.QuantidadeHoraAulaObrigatoria ?? int.MaxValue) ||
                quantidadeHorasAulaRelogio > (model.QuantidadesDisponiveis?.QuantidadeHoraRelogioOptativa ?? int.MaxValue);
        }
    }
}