using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    [SMCGroupedPropertyConfiguration(GroupId = "LookupSolicitacaoDeServicoFiltroSolicitante", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "LookupSolicitacaoDeServicoFiltroSolicitacao", Size = SMCSize.Grid24_24)]
    public class SolicitacaoDeServicoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region DataSource

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Servicos { get; set; }

        #endregion DataSource

        [SMCMaxLength(100)]
        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitante")]
        [SMCSortable(false, true, "PessoaAtuacao.DadosPessoais.Nome")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid12_24)]
        public string Solicitante { get; set; }

        [SMCCpf]
        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitante")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string CPF { get; set; }

        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitante")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string Passaporte { get; set; }

        [SMCKey]
        [SMCDescription]
        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitacao")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string NumeroProtocolo { get; set; }

        [SMCHidden]
        public long? SeqTipoServico { get; set; }

        [SMCSelect(nameof(Servicos))]
        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitacao")]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public long? SeqServico { get; set; }

        [SMCSelect]
        [SMCDependency(nameof(SeqServico), nameof(SolicitacaoServicoController.BuscarProcessosPorServicoSelect), "SolicitacaoServicoRoute", "", true)]
        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitacao")]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid9_24)]
        public long? SeqProcesso { get; set; }

        [SMCSelect]
        [SMCDependency(nameof(SeqProcesso), nameof(SolicitacaoServicoController.BuscarProcessoEtapaSelect), "SolicitacaoServicoRoute", "", true)]
        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitacao")]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public long? SeqProcessoEtapa { get; set; }

        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCDependency(nameof(SeqProcessoEtapa), nameof(SolicitacaoServicoController.BuscarSituacoesEtapasComCategoriaSelect), "SolicitacaoServicoRoute", "", true, new string[] { nameof(SeqProcesso) })]
        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitacao")]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public long? SeqSituacaoEtapa { get; set; }

        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitacao")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public DateTime? DataInclusaoInicio { get; set; }

        [SMCGroupedProperty("LookupSolicitacaoDeServicoFiltroSolicitacao")]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        public DateTime? DataInclusaoFim { get; set; }

        [SMCHidden]
        public TipoFiltroCentralSolicitacao TipoFiltroCentralSolicitacao { get; set; } = TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao;

        #region Propriedades para ordenação default
        
        [SMCHidden]
        [SMCSortable(false, true, "ConfiguracaoProcesso.Processo.Descricao")]
        public string Processo { get; set; }

        #endregion
    }
}
