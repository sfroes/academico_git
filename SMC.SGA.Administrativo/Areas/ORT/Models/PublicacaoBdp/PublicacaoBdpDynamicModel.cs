    using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using SMC.SGA.Administrativo.Areas.ORT.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    [SMCStepConfiguration(Partial = "_aba1")]
    [SMCStepConfiguration(Partial = "_aba2")]
    [SMCStepConfiguration(Partial = "_aba3")]
    [SMCStepConfiguration]
    public class PublicacaoBdpDynamicModel : SMCDynamicViewModel
    {
        #region Informações Gerais

        [SMCStep(0)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public string Titulo { get; set; }

        [SMCReadOnly]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid24_24)]
        public List<PublicacaoBdpAutorViewModel> Alunos { get; set; }

        [SMCReadOnly]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid8_24)]
        public string TipoTrabalho { get; set; }

        [SMCReadOnly]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime? DataDefesa { get; set; }

        [SMCStep(0)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCReadOnly]
        [SMCDisplay]
        [SMCFile(ActionDownload = "DownloadFileGuid", AreaDownload = "", ControllerDownload = "Home", MaxFiles = 1, HideDescription = true, DisplayFilesInContextWindow = true)]
        public SMCUploadFile ArquivoAnexadoAtaDefesa { get; set; }

        [SMCReadOnly]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataEntrega { get; set; }

        [SMCRequired]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public short? QuantidadeVolumes { get; set; }

        [SMCRequired]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public short? QuantidadePaginas { get; set; }
               
        [SMCReadOnly]
        [SMCStep(0)]
        [SMCSize(SMCSize.Grid8_24)]
        [SMCConditionalDisplay(nameof(PublicacaoBdpCabecalhoViewModel.Situacao), nameof(SituacaoTrabalhoAcademico.LiberadaConsulta))]
        public long? CodigoAcervo { get; set; }

        [SMCDetail(max:2)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCStep(0)]
        //[SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        public SMCMasterDetailList<PublicacaoBdpArquivoViewModel> Arquivos { get; set; }

        [SMCMapForceFromTo]
        [SMCStep(0)]
        public List<MembroBancaExaminadoraViewModel> Banca { get; set; } = new List<MembroBancaExaminadoraViewModel>();

        #endregion Informações Gerais

        #region Informações por Idioma

        [SMCStep(1)]
        public SMCMasterDetailList<PublicacaoBdpIdiomaViewModel> Idiomas { get; set; }

        #endregion Informações por Idioma

        #region Informações da autorização

        [SMCStep(2)]
        public SMCMasterDetailList<PublicacaoBdpAutorizacaoViewModel> Autorizacoes { get; set; }

        //[SMCStep(2)]
        //[SMCSize(SMCSize.Grid3_24)]
        //[SMCReadOnly]
        //public DateTime? DataAutorizacao { get; set; }

        //[SMCStep(2)]
        //[SMCSize(SMCSize.Grid10_24)]
        //[SMCSelect]
        //[SMCRequired]
        //[SMCConditionalReadonly(nameof(PublicacaoBdpCabecalhoViewModel.BloqueiaAlteracoes), true, PersistentValue = true)]
        //public TipoAutorizacao? TipoAutorizacao { get; set; }

        //[SMCStep(2)]
        //[SMCSize(SMCSize.Grid11_24)]
        //[SMCReadOnly]
        //public Guid? CodigoAutorizacao { get; set; }


        #endregion Informações da autorização

        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqPublicacaoBdp { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public List<string> DescricoesMembros { get => Banca?.Select(m => m.DescricaoMembro ?? string.Empty).ToList(); }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Tab()
                .Ajax()
                .Detail<PublicacaoBdpListarDynamicModel>("_DetailList", x => x.GroupBy, "_DetailHeader")
                .Header(nameof(PublicacaoBdpController.EditarCabecalho))
                .HeaderIndexList(nameof(PublicacaoBdpController.LegendaLista))
                .Javascript("PublicacaoBdp")
                .DisableInitialListing()
                .Service<ITrabalhoAcademicoService>(index: nameof(ITrabalhoAcademicoService.BuscarTrabalhosAcademicosLiberacaoConsulta),
                                                    edit: nameof(ITrabalhoAcademicoService.AlterarTrabalhoAcademicoPublicacaoBdp),
                                                    save: nameof(ITrabalhoAcademicoService.SalvarAlteracoesLiberacaoConsultaBdp))
                .Tokens(tokenInsert: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                        tokenEdit: UC_ORT_003_02_02.MANTER_TRABALHO_ACADEMICO_BDP,
                        tokenList: UC_ORT_003_02_01.PESQUISAR_TRABALHO_ACADEMICO_BDP,
                        tokenRemove: SMCSecurityConsts.SMC_DENY_AUTHORIZATION);
        }
    }
}