using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Academico.UI.Mvc.Areas.DCT.Lookups;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.PES.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class FormacaoAcademicaDynamicModel : SMCDynamicViewModel
    {

        #region [Data Source]

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ITitulacaoService), nameof(ITitulacaoService.BuscarTitulacoesSelect), values: new[] { nameof(Sexo), nameof(SemCurso) })]
        public List<SMCDatasourceItem> Titulacoes { get; set; }

        [SMCIgnoreProp]
        [SMCDataSource]
        [SMCServiceReference(typeof(ITitulacaoDocumentoComprobatorioService), nameof(ITitulacaoDocumentoComprobatorioService.BuscarTitulacaoDocumentosComprobatorios), values: new[] { nameof(SeqTitulacao) })]
        public List<SMCDatasourceItem> DocumentosComprobatorios { get; set; }

        #endregion

        [SMCHidden]
        public bool RetornarInstituicaoEnsinoLogada { get => true; }

        [SMCIgnoreProp]
        public bool SemCurso => true;

        [SMCHidden]
        [SMCKey]
        [SMCReadOnly]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqPessoaAtuacao { get; set; }

        [SMCSelect("Titulacoes")]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCRequired]
        public long SeqTitulacao { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        [SMCRequired]
        public string Descricao { get; set; }

        [SMCInclude("PessoaAtuacao.DadosPessoais")]
        [SMCMapProperty("PessoaAtuacao.DadosPessoais.Sexo")]
        [SMCHidden]
        public Sexo Sexo { get; set; }

        [SMCMask("9999")]
        [SMCMinValue(1950)]
        [SMCSize(SMCSize.Grid2_24)]
        public int? AnoInicio { get; set; }

        [SMCMask("9999")]
        [SMCMinValue(nameof(AnoInicio))]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public int? AnoObtencaoTitulo { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCRadioButtonList]
        [SMCRequired]
        public bool? TitulacaoMaxima { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string Curso { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string Orientador { get; set; }

        [InstituicaoExternaLookup]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCDependency(nameof(RetornarInstituicaoEnsinoLogada))]
        public InstituicaoExternaLookupViewModel SeqInstituicaoExterna { get; set; }

        [SMCHidden]
        public short? QuantidadeMinima { get; set; }

        [SMCHidden]
        public int QuantidadeMinimaLookup { get { return QuantidadeMinima.HasValue ? (int)QuantidadeMinima.Value : 0; } }

        [SMCHidden]
        public short? QuantidadeMaxima { get; set; }

        [SMCHidden]
        public long? SeqHierarquiaClassificacao { get; set; }

        [SMCHidden]
        public int QuantidadeMaximaLookup { get { return QuantidadeMaxima.HasValue ? (int)QuantidadeMaxima.Value : 0; } }

        [ClassificacaoLookup(MinItemsProperty = nameof(QuantidadeMinimaLookup), MaxItemsProperty = nameof(QuantidadeMaximaLookup))]
        [SMCDependency(nameof(SeqHierarquiaClassificacao))]
        [SMCSize(SMCSize.Grid12_24)]
        public ClassificacaoLookupViewModel SeqClassificacao { get; set; }

        [SMCDependency(nameof(SeqTitulacao), nameof(FormacaoAcademicaController.BuscarDocumentosComprobatoriosTitulacao), "FormacaoAcademica", true)]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect(nameof(DocumentosComprobatorios))]
        public List<long> SeqsDocumentosApresentados { get; set; }

        [SMCIgnoreProp]
        public string MensagemConfirmacaoAssert => Views.FormacaoAcademica.App_LocalResources.UIResource.MensagemConfirmacaoTitulacaoMaxima;

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .HeaderIndex("CabecalhoFormacaoAcademica")
                .Header("CabecalhoFormacaoAcademica")
                .IgnoreFilterGeneration(true)
                .RequiredIncomingParameters(nameof(SeqPessoaAtuacao))
                .ButtonBackIndex("Index", "Colaborador", o => new { Area = "DCT" })
                .Messages(x => string.Format(Views.FormacaoAcademica.App_LocalResources.UIResource.Listar_Excluir_Confirmacao,
                            ((FormacaoAcademicaListarDynamicModel)x).Descricao))
                .Tokens(tokenList: UC_PES_002_01_01.PESQUISAR_FORMACAO_ACADEMICA)
                .Service<IFormacaoAcademicaService>(
                      insert: nameof(IFormacaoAcademicaService.BuscarFormacaoAcademicaInserted),
                      edit: nameof(IFormacaoAcademicaService.BuscarFormacaoAcademica),
                      save: nameof(IFormacaoAcademicaService.SalvarFormacaoAcademica),
                      delete: nameof(IFormacaoAcademicaService.ExcluirFormacaoAcademica))

                .Assert(MensagemConfirmacaoAssert, (service, model) =>
                {
                    var modelFormacaoAcademica = (model as FormacaoAcademicaDynamicModel);
                    var formacaoAcademicaService = service.Create<IFormacaoAcademicaService>();

                    return formacaoAcademicaService.ValidarTitulacaoMaxima(modelFormacaoAcademica.SeqPessoaAtuacao, modelFormacaoAcademica.TitulacaoMaxima, modelFormacaoAcademica.Seq);

                });
        }

    }
}