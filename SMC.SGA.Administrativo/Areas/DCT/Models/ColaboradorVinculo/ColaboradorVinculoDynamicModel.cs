using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.DCT.Controllers;
using SMC.SGA.Administrativo.Areas.DCT.Views.ColaboradorVinculo.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "Pesquisa", Size = SMCSize.Grid24_24)]
    public class ColaboradorVinculoDynamicModel : SMCDynamicViewModel
    {
        #region [ DataSources ]

        [SMCDataSource]
        [SMCServiceReference(typeof(IEntidadeService), nameof(IEntidadeService.BuscarEntidadesVinculoColaboradorSelect))]
        public List<SMCSelectListItem> EntidadesColaborador { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeVinculoColaboradorService),
                             nameof(IInstituicaoTipoEntidadeVinculoColaboradorService.BuscarTiposVinculoColaboradorPorEntidadeSelect),
                             values: new[] { nameof(SeqEntidadeVinculo), nameof(SeqColaboradorVinculo) })]
        public List<SMCSelectListItem> TiposVinculoColaborador { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(IFormacaoEspecificaService),
                             nameof(IFormacaoEspecificaService.BuscarLinhasDePesquisaGrupoPrograma),
                             values: new[] { nameof(SeqEntidadeVinculo) })]
        public List<SMCSelectListItem> LinhasDePesquisaGrupoPrograma { get; set; }

        [SMCDataSource]
        [SMCServiceReference(typeof(ICursoOfertaLocalidadeService),
                             nameof(ICursoOfertaLocalidadeService.BuscarCursoOfertasLocalidadeAtivasPorEntidadesResponsaveisSelect),
                             values: new[] { nameof(SeqEntidadeVinculo) })]
        public List<SMCSelectListItem> CursoOfertasLocalidades { get; set; }

        #endregion [ DataSources ]

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqColaboradorVinculo { get { return this.Seq; } }

        [SMCHidden]
        [SMCParameter]
        public long SeqColaborador { get; set; }

        [SMCHidden]
        public TipoAtuacao TipoAtuacao { get; set; } = TipoAtuacao.Colaborador;

        [SMCConditionalDisplay(nameof(InseridoPorCarga), true)]
        [SMCCssClass("smc-sga-mensagem-informativa smc-sga-mensagem")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get => UIResource.MensagemInformativa; }

        [SMCHidden]
        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorVinculoController.ValidarTipoEntidadeVinculoGrupoPrograma), "ColaboradorVinculo", false)]
        public bool? EntidadeVinculoGrupoPrograma { get; set; }

        [SMCConditionalReadonly(nameof(InseridoPorCarga), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCRequired]
        [SMCSelect(nameof(EntidadesColaborador), autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid10_24)]
        public long SeqEntidadeVinculo { get; set; }

        [SMCHidden]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCHidden]
        public bool PermiteAlterarDadosColaborador { get; set; }

        [SMCHidden]
        public bool PermitirAlterarDataFimVinculo { get; set; }

        [SMCHidden]
        public bool SeqTipoVinculoColaboradorSomenteLeitura => InseridoPorCarga && !PermiteAlterarDadosColaborador;

        [SMCConditionalReadonly(nameof(SeqTipoVinculoColaboradorSomenteLeitura), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorVinculoController.BuscarTiposVinculoColaboradorPorEntidadeSelect), "ColaboradorVinculo", true, new[] { nameof(SeqColaboradorVinculo) })]
        [SMCRequired]
        [SMCSelect(nameof(TiposVinculoColaborador), autoSelectSingleItem: true, NameDescriptionField = nameof(DescricaoTipoVinculoSelect), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        public long SeqTipoVinculoColaborador { get; set; }

        [SMCHidden]
        public string DescricaoTipoVinculoSelect { get; set; }

        /// <summary>
        /// Estas despendecias são utilizadas no colaborador e colaborador vinculo
        /// </summary>
        [SMCMapProperty("DataInicio")]
        [SMCConditionalReadonly(nameof(InseridoPorCarga), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime DataInicioVinculo { get; set; }

        /// <summary>
        /// Estas despendecias são utilizadas no colaborador e colaborador vinculo
        /// </summary>
        [SMCMapProperty("DataFim")]
        [SMCConditionalRequired("SeqTipoVinculoColaborador", SMCConditionalOperation.Equals, true, DataAttribute = "data-supervisor")]
        [SMCConditionalReadonly(nameof(PermitirAlterarDataFimVinculo), SMCConditionalOperation.Equals, false, PersistentValue = true)]
        [SMCMinDate(nameof(DataInicioVinculo))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public DateTime? DataFimVinculo { get; set; }

        [SMCReadOnly]
        [SMCSelect]
        [SMCSize(SMCSize.Grid4_24)]
        public MotivoFimVinculo? MotivoFimVinculo { get; set; }

        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        public bool InseridoPorCarga { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoVinculoColaborador), nameof(ColaboradorVinculoController.RetornarTipoVinculoNecessitaAcompanhamento), "ColaboradorVinculo", false)]
        public bool ExibirColaboradorResponsavel { get; set; }

        [SMCConditionalDisplay(nameof(ExibirColaboradorResponsavel), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("Pesquisa")]
        [SMCSize(SMCSize.Grid24_24)]
        public string TituloPesquisa { get; set; }

        [SMCConditionalDisplay(nameof(ExibirColaboradorResponsavel), SMCConditionalOperation.Equals, true)]
        [SMCGroupedProperty("Pesquisa")]
        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        public string Observacao { get; set; }

        [SMCConditionalDisplay(nameof(ExibirColaboradorResponsavel), SMCConditionalOperation.Equals, true)]
        [SMCDetail(type: SMCDetailType.Block, min: 1)]
        [SMCGroupedProperty("Pesquisa")]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ColaboradorResponsavelVinculoViewModel> ColaboradoresResponsaveis { get; set; }

        //[SMCDetail]
        //[SMCSize(SMCSize.Grid24_24)]
        //public SMCMasterDetailList<ColaboradorVinculoCursoViewModel> Cursos { get; set; }

        [SMCDependency(nameof(SeqEntidadeVinculo), nameof(ColaboradorVinculoController.BuscarEntidadesFilhas), "ColaboradorVinculo", true)]
        [SMCHidden]
        public long[] SeqsEntidadesResponsaveis { get; set; }

        [SMCConditionalDisplay(nameof(EntidadeVinculoGrupoPrograma), SMCConditionalOperation.Equals, true)]
        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ColaboradorVinculoFormacaoEspecificaViewModel> FormacoesEspecificas { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.HeaderIndex(nameof(ColaboradorVinculoController.CabecalhoColaboradorVinculo))
                   .Header(nameof(ColaboradorVinculoController.CabecalhoColaboradorVinculo))
                   .RequiredIncomingParameters(new[] { nameof(SeqColaborador) })
                   .Detail<ColaboradorVinculoListarDynamicModel>("_DetailList")
                   .ButtonBackIndex("Index", "Colaborador")
                   .Button("AssociarOfertaCursoLocalidade", "Index", "ColaboradorVinculoCurso",
                        UC_DCT_001_06_05.ASSOCIAR_OFERTA_CURSO_TIPO_ATIVIDADE,
                        i => new
                        {
                            SeqColaborador = SMCDESCrypto.EncryptNumberForURL(((ColaboradorVinculoListarDynamicModel)i).SeqColaborador),
                            SeqColaboradorVinculo = SMCDESCrypto.EncryptNumberForURL(((ColaboradorVinculoListarDynamicModel)i).SeqColaboradorVinculo),
                            SeqEntidadeVinculo = SMCDESCrypto.EncryptNumberForURL(((ColaboradorVinculoListarDynamicModel)i).SeqEntidadeVinculo),

                        })
                   .Service<IColaboradorVinculoService>(edit: nameof(IColaboradorVinculoService.BuscarColaboradorVinculo),
                                                        delete: nameof(IColaboradorVinculoService.ExcluirColaboradorVinculo),
                                                        index: nameof(IColaboradorVinculoService.BuscarVinculosColaborador),
                                                        insert: nameof(IColaboradorVinculoService.BuscarConfiguracaoColaboradorVinculo),
                                                        save: nameof(IColaboradorVinculoService.SalvarColaboradorVinculo))
                   .Tokens(tokenInsert: UC_DCT_001_06_04.MANTER_VINCULO_COLABORADOR,
                           tokenEdit: UC_DCT_001_06_04.MANTER_VINCULO_COLABORADOR,
                           tokenRemove: UC_DCT_001_06_04.MANTER_VINCULO_COLABORADOR,
                           tokenList: UC_DCT_001_06_03.PESQUISAR_VINCULO_COLABORADOR);
        }
    }
}