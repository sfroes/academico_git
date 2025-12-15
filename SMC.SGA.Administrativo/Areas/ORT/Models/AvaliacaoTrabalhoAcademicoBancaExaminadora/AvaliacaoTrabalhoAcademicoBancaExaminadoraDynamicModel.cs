using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "FiltroCancelamentoBanca", Size = SMCSize.Grid24_24)]
    public class AvaliacaoTrabalhoAcademicoBancaExaminadoraDynamicModel : SMCDynamicViewModel, ISMCSeq
    {
        #region [ Hidden ]

        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public bool AlunoFormado { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqTrabalhoAcademico { get; set; }

        [SMCHidden]
        public long? SeqAvaliacao { get; set; }

        [SMCHidden]
        public string Sigla { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqOrigemAvaliacao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqAplicacaoAvaliacao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqNivelEnsino { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long? SeqInstituicaoEnsino { get; set; }

        [SMCHidden]
        public TipoAvaliacao TipoAvaliacaoBanca { get => TipoAvaliacao.Banca; }

        [SMCHidden]
        public long? SeqCalendario { get; set; }

        [SMCHidden]
        public string NotaConceito { get; set; }

        [SMCHidden]
        [SMCParameter]
        public DateTime? DataDepositoSecretaria { get; set; }

        #endregion [ Hidden ]

        #region [ Data Source ]

        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> TiposEvento { get; set; }

        #endregion [ Data Source ]

        [SMCOrder(0)]
        [SMCConditionalReadonly(nameof(AlunoFormado), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid8_24)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSelect(nameof(TiposEvento), AutoSelectSingleItem = true)]
        public long SeqTipoEventoAgd { get; set; }

        [SMCOrder(1)]
        [SMCConditionalReadonly(nameof(AlunoFormado), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid5_24, SMCSize.Grid4_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCMinDate(nameof(DataDepositoSecretaria))]
        public DateTime DataInicioAplicacaoAvaliacao { get; set; }

        [SMCOrder(2)]
        [SMCMaxLength(255)]
        [SMCConditionalReadonly(nameof(AlunoFormado), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCRequired]
        public string Local { get; set; }

        [SMCOrder(3)]
        [SMCConditionalReadonly(nameof(NotaConceito), SMCConditionalOperation.NotEqual, "", PersistentValue = true)]
        [SMCGroupedProperty("FiltroCancelamentoBanca")]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Filter | SMCViewMode.List)]
        public DateTime? DataCancelamento { get; set; }

        [SMCConditionalReadonly(nameof(DataCancelamento), SMCConditionalOperation.Equals, "", RuleName = "R2")]
        [SMCConditionalRequired(nameof(DataCancelamento), SMCConditionalOperation.NotEqual, "")]
        [SMCConditionalDisplay(nameof(Seq), SMCConditionalOperation.NotEqual, 0)]
        [SMCConditionalReadonly(nameof(NotaConceito), SMCConditionalOperation.NotEqual, "", RuleName = "R1", PersistentValue = true)]
        [SMCConditionalRule("(R1 || R2)")]
        [SMCOrder(4)]
        [SMCMultiline]
        [SMCGroupedProperty("FiltroCancelamentoBanca")]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public string MotivoCancelamento { get; set; }

        [SMCDetail(SMCDetailType.Block, ClearOnChangeProperty = nameof(DataInicioAplicacaoAvaliacao))]
        [SMCConditionalReadonly(nameof(AlunoFormado), true, PersistentValue = true, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(DataInicioAplicacaoAvaliacao), SMCConditionalOperation.Equals, "", PersistentValue = false, RuleName = "R2")]
        [SMCConditionalRule("(R1 || R2)")]
        [SMCOrder(5)]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public SMCMasterDetailList<AvaliacaoTrabalhoAcademicoMembroBancaViewModel> MembrosBancaExaminadora { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(DataInicioAplicacaoAvaliacao))]
        public DateTime? DataInicio { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(DataInicioAplicacaoAvaliacao))]
        public DateTime? DataFim { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Ajax()
                   .EditInModal()
                   .Header("HeaderEdit")
                   .RequiredIncomingParameters(nameof(SeqTrabalhoAcademico))
                   .RedirectIndexTo("Index", "AvaliacaoTrabalhoAcademico", x => new { seqTrabalhoAcademico = new SMCEncryptedLong((x as AvaliacaoTrabalhoAcademicoBancaExaminadoraDynamicModel).SeqTrabalhoAcademico) })
                   .Tokens(tokenList: UC_ORT_002_02_03.PESQUISAR_AVALIACAO_TRABALHO_ACADEMICO,
                       tokenInsert: UC_ORT_002_02_04.MANTER_AGENDAMENTO_BANCA_EXAMINADORA,
                        tokenRemove: UC_ORT_002_02_04.MANTER_AGENDAMENTO_BANCA_EXAMINADORA,
                       tokenEdit: UC_ORT_002_02_04.MANTER_AGENDAMENTO_BANCA_EXAMINADORA)
                .Service<IAplicacaoAvaliacaoService>(
                        edit: nameof(IAplicacaoAvaliacaoService.BuscarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora),
                        insert: nameof(IAplicacaoAvaliacaoService.BuscarAvaliacoesTrabalhoAcademicoBancaExaminadoraInsert),
                        save: nameof(IAplicacaoAvaliacaoService.SalvarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora),
                        delete: nameof(IAplicacaoAvaliacaoService.ExcluirAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora));

            if (HttpContext.Current.Request.QueryString.AllKeys.Contains("seq"))
                options.ButtonBackIndex("Index", "TrabalhoAcademico", x => new { area = "ORT" });
        }

        #endregion [ Configurações ]
    }
}