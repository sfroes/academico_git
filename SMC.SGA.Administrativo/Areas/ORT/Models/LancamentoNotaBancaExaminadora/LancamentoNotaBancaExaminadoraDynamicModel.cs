using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class LancamentoNotaBancaExaminadoraDynamicModel : SMCDynamicViewModel, ISMCSeq
    {
        #region [ Hidden ]

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqAplicacaoAvaliacao { get => Seq; }

        [SMCHidden]
        [SMCParameter]
        public long SeqTrabalhoAcademico { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqComponenteCurricular { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqOrigemAvaliacao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqNivelEnsino { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqEscalaApuracao { get; set; }

        [SMCHidden]
        public bool? ApuracaoNota { get; set; }

        [SMCHidden]
        public long SeqTipoTrabalho { get; set; }

        [SMCHidden]
        public bool PublicacaoBibliotecaObrigatoria { get; set; }

        #endregion [ Hidden ]

        #region [ Data Source ]

        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> EscalaApuracaoItens { get; set; }

        #endregion [ Data Source ]

        [SMCOrder(0)]
        [SMCMask("99999")]
        [SMCMinValue(1)]
        [SMCConditionalDisplay(nameof(PublicacaoBibliotecaObrigatoria), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(PublicacaoBibliotecaObrigatoria), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public int? NumeroDefesa { get; set; }

        [SMCOrder(1)]
        [SMCConditionalDisplay(nameof(ApuracaoNota), SMCConditionalOperation.Equals, true)]
        [SMCConditionalRequired(nameof(ApuracaoNota), SMCConditionalOperation.Equals, true)]
        [SMCDecimalDigits(0)]
        [SMCMask("999")]
        [SMCMaxValue(nameof(NotaMaxima))]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid3_24)]
        public decimal? Nota { get; set; }

        [SMCOrder(2)]
        [SMCConditionalDisplay(nameof(ApuracaoNota), SMCConditionalOperation.Equals, false)]
        [SMCConditionalRequired(nameof(ApuracaoNota), SMCConditionalOperation.Equals, false)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCSelect(nameof(EscalaApuracaoItens), autoSelectSingleItem: true)]
        public long? SeqEscalaApuracaoItem { get; set; }

        [SMCOrder(3)]
        [SMCFile(ActionDownload = "DownloadFileGuid", AreaDownload = "", ControllerDownload = "Home", DisplayFilesInContextWindow = false, MaxFiles = 1, HideDescription = true)]
        [SMCConditionalDisplay(nameof(PublicacaoBibliotecaObrigatoria), SMCConditionalOperation.Equals, true)]
        // Task 37785: O anexo da ata de defesa não deverá mais ser obrigatório quando o tipo de trabalho estiver configurado por instituição e nível de ensino para ter publicação na biblioteca.
        // Nesta situação, o upload da ata deverá estar disponível, mas não obrigatório.
        //[SMCConditionalRequired(nameof(PublicacaoBibliotecaObrigatoria), SMCConditionalOperation.Equals, true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid16_24, SMCSize.Grid20_24)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        public SMCUploadFile ArquivoAnexadoAtaDefesa { get; set; }

        [SMCDetail(SMCDetailType.Block)]
        [SMCOrder(4)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public SMCMasterDetailList<MembroLancamentoNotaBancaExaminadoraViewModel> MembrosBancaExaminadora { get; set; }

        [SMCHidden]
        public decimal? NotaMaxima { get; set; }

        [SMCIgnoreProp]
        public string MensagemConfirmacaoAssert { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Ajax()
                   .EditInModal()
                   .RequiredIncomingParameters(nameof(SeqTrabalhoAcademico))
                   .RedirectIndexTo("Index", "AvaliacaoTrabalhoAcademico", x => new { seqTrabalhoAcademico = new SMCEncryptedLong((x as LancamentoNotaBancaExaminadoraDynamicModel).SeqTrabalhoAcademico) })
                .Tokens(tokenInsert: UC_ORT_002_02_05.LANCAR_NOTA_BANCA_EXAMINADORA,
                        tokenEdit: UC_ORT_002_02_05.LANCAR_NOTA_BANCA_EXAMINADORA,
                        tokenRemove: UC_ORT_002_02_05.LANCAR_NOTA_BANCA_EXAMINADORA)

                .Assert("MSG_Ata_Nao_Anexada_Publicacao_Biblioteca", (controller, model) =>
                {
                    var modelParsed = model as LancamentoNotaBancaExaminadoraDynamicModel;
                    return (modelParsed.PublicacaoBibliotecaObrigatoria && modelParsed.ArquivoAnexadoAtaDefesa == null);
                })
                .Assert("MSG_Confirmacao_LancamentoAprovacaoNota", (controller, model) =>
                {
                    IAplicacaoAvaliacaoService AplicacaoAvaliacaoService = controller.Create<IAplicacaoAvaliacaoService>();
                    var data = model.Transform<LancamentoNotaBancaExaminadoraData>();
                    bool exibir = AplicacaoAvaliacaoService.ExibirMensagemAprovacaoPublicacaoBibliotecaObrigatoria(data);
                    return exibir;
                })
                .Service<IApuracaoAvaliacaoService>(
                        insert: nameof(IApuracaoAvaliacaoService.BuscarLancamentoNotaBancaExaminadoraInsert),
                        save: nameof(IApuracaoAvaliacaoService.SalvarLancamentoNotaBancaExaminadora),
                        index: nameof(IAplicacaoAvaliacaoService.BuscarListaComponenteAvaliacoesTrabalhoAcademico),
                        delete: nameof(IApuracaoAvaliacaoService.ExcluirNotaLancadaApuracaoAvaliacao));
        }

        #endregion [ Configurações ]
    }
}