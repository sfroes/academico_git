using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using SMC.SGA.Administrativo.Areas.FIN.Views.PessoaAtuacaoBeneficio.App_LocalResources;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaAtaucaoBeneficioAlterarVigenciaViewModel : SMCViewModelBase
    {

        #region [DataSource]

        public List<SMCDatasourceItem> MotivosAlteracoes { get; set; }

        #endregion

        #region [Hiddens]

        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqBeneficio { get; set; }

        [SMCHidden]
        public bool Aluno { get; set; }

        [SMCHidden]
        public long? SeqConfiguracaoBeneficio { get; set; }

        #endregion

        #region Variavéis auxiliares

        //[SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.DesabilitarDataFimBeneficio), "PessoaAtuacaoBeneficio", true)]
        [SMCHidden]
        public bool DesablilitarDataFim { get; set; }

        #endregion

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public string DescricaoBeneficio { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public string DescricaoFormaDeducao { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public string DescricaoTipoDeducao { get; set; }

        [SMCValueEmpty("-")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public decimal? ValorBeneficio { get; set; }

        [SMCDependency(nameof(SeqBeneficio), nameof(PessoaAtuacaoBeneficioController.BuscarIncideParcelaSelect), "PessoaAtuacaoBeneficio", false, new string[] { nameof(SeqPessoaAtuacao), nameof(Aluno) })]
        [SMCConditionalRequired(nameof(Aluno), false)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid5_24)]
        public bool? IncideParcelaMatricula { get; set; }

        [SMCConditionalDisplay(nameof(IncideParcelaMatricula), SMCConditionalOperation.Equals, false)]
        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHidden(SMCViewMode.List)]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get; set; } = UIResource.Texto_MensagemInformativa;

        [SMCDependency(nameof(IncideParcelaMatricula), nameof(PessoaAtuacaoBeneficioController.BuscarDataAdmissaoIngressante), "PessoaAtuacaoBeneficio", false, new string[] { nameof(SeqPessoaAtuacao) })]
        [SMCConditionalReadonly(nameof(IncideParcelaMatricula), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime DataInicioVigencia { get; set; }

        [SMCMinDate(nameof(DataInicioVigencia))]
        [SMCConditionalReadonly(nameof(DesablilitarDataFim), true, PersistentValue = true)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid4_24)]
        public DateTime? DataFimVigencia { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(MotivosAlteracoes), AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid11_24)]
        public long SeqMotivoAlteracaoBeneficio { get; set; }

        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        public string Justificativa { get; set; }

        [SMCDetail]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<PessoaAtuacaoBeneficioAnexoViewModel> ArquivosAnexo { get; set; }
    }
}