using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaConfiguracaoNotificacaoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region Datasource    

        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        public List<SMCDatasourceItem> TiposNotificacao { get; set; }

        public List<SMCDatasourceItem> GruposEscalonamento { get; set; }

        #endregion

        [SMCParameter]
        [SMCHidden]
        [SMCFilterKey]
        public long SeqProcesso { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        [SMCSize(SMCSize.Grid8_24)]
        public long? SeqProcessoUnidadeResponsavel { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(TiposNotificacao))]
        [SMCSize(SMCSize.Grid6_24)]
        public long? SeqTipoNotificacao { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCConditionalReadonly(nameof(SeqTipoNotificacao), SMCConditionalOperation.NotEqual, "")]
        [SMCDependency(nameof(SeqTipoNotificacao), nameof(ProcessoEtapaConfiguracaoNotificacaoController.PreencherCampoPermiteAgendamento), "ProcessoEtapaConfiguracaoNotificacao", false)]
        [SMCSize(SMCSize.Grid4_24)]
        public bool? PermiteAgendamento { get; set; }

        [SMCHidden]
        public bool ExigeEscalonamento { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqTipoNotificacao), nameof(ProcessoEtapaConfiguracaoNotificacaoController.ValidaExibeGrupoEscalonamento), "ProcessoEtapaConfiguracaoNotificacao", true)]
        public bool AuxiliarTipoNotificacaoPermiteAgendamento { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(PermiteAgendamento), nameof(ProcessoEtapaConfiguracaoNotificacaoController.ValidaExibeGrupoEscalonamento), "ProcessoEtapaConfiguracaoNotificacao", true)]
        public bool AuxiliarPermiteAgendamento { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect(nameof(GruposEscalonamento))]
        [SMCConditionalDisplay(nameof(ExigeEscalonamento), true, RuleName = "CD1")]
        [SMCConditionalDisplay(nameof(AuxiliarTipoNotificacaoPermiteAgendamento), true, RuleName = "CD2")]
        [SMCConditionalDisplay(nameof(AuxiliarPermiteAgendamento), true, RuleName = "CD3")]
        [SMCConditionalRule("(CD1 && CD2) || (CD1 && CD3)")]
        [SMCSize(SMCSize.Grid6_24)]
        public List<long> SeqsGrupoEscalonamento { get; set; }

        #region Propriedades para ordenação default

        //[SMCHidden]
        //[SMCSortable(true, true)]
        //public DateTime DataAutenticacao { get; set; }

        #endregion
    }
}