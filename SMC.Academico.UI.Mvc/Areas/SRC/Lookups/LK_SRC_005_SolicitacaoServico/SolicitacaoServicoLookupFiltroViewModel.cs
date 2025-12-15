using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    [SMCGroupedPropertyConfiguration(GroupId = "Solicitante", Size = SMCSize.Grid24_24)]
    [SMCGroupedPropertyConfiguration(GroupId = "Processo", Size = SMCSize.Grid24_24)]
    public class SolicitacaoServicoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        #region DataSource

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Processos { get; set; }

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> GruposEscalonamento { get; set; }

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> ProcessosEtapa { get; set; }

        [SMCIgnoreProp]
        public List<SMCDatasourceItem> SituacoesEtapa { get; set; }

        #endregion DataSource

        #region Propriedades validação

        [SMCHidden]
        public bool SeqProcessoSomenteLeitura { get; set; }

        [SMCHidden]
        public bool SeqGrupoEscalonamentoSomenteLeitura { get; set; }

        [SMCHidden]
        public bool SeqProcessoEtapaSomenteLeitura { get; set; }

        [SMCHidden]
        public bool NomeSomenteLeitura { get; set; }

        [SMCHidden]
        public bool CPFSomenteLeitura { get; set; }

        [SMCHidden]
        public bool PassaporteSomenteLeitura { get; set; }

        #endregion Propriedades validação

        [SMCConditionalReadonly(nameof(NomeSomenteLeitura), true, PersistentValue = true)]
        [SMCSortable(false, true, "PessoaAtuacao.DadosPessoais.Nome")]
        [SMCGroupedProperty("Solicitante")]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid10_24, SMCSize.Grid12_24)]
        public string Solicitante { get; set; }

        [SMCConditionalReadonly(nameof(CPFSomenteLeitura), true, PersistentValue = true)]
        [SMCCpf]
        [SMCGroupedProperty("Solicitante")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string CPF { get; set; }

        [SMCConditionalReadonly(nameof(PassaporteSomenteLeitura), true, PersistentValue = true)]
        [SMCGroupedProperty("Solicitante")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string Passaporte { get; set; }

        [SMCConditionalReadonly(nameof(SeqProcessoSomenteLeitura), true, PersistentValue = true)]
        [SMCGroupedProperty("Processo")]
        [SMCSelect(nameof(Processos))]
        [SMCSize(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24)]
        public long? SeqProcesso { get; set; }

        [SMCGroupedProperty("Processo")]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public string NumeroProtocolo { get; set; }

        [SMCConditionalReadonly(nameof(SeqGrupoEscalonamentoSomenteLeitura), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqProcesso), nameof(SolicitacaoServicoController.BuscarGruposEscalonamentoSelect), "SolicitacaoServicoRoute", "", false)]
        [SMCGroupedProperty("Processo")]
        [SMCSelect(autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqGrupoEscalonamento { get; set; }

        [SMCConditionalReadonly(nameof(SeqProcessoEtapaSomenteLeitura), true, PersistentValue = true)]
        [SMCDependency(nameof(SeqProcesso), nameof(SolicitacaoServicoController.BuscarProcessoEtapaSelect), "SolicitacaoServicoRoute", "", false)]
        [SMCGroupedProperty("Processo")]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqProcessoEtapa { get; set; }

        [SMCDependency(nameof(SeqProcessoEtapa), nameof(SolicitacaoServicoController.BuscarSituacoesEtapasComCategoriaSelect), "SolicitacaoServicoRoute", "", true, new string[] { nameof(SeqProcesso) })]
        [SMCGroupedProperty("Processo")]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public long? SeqSituacaoEtapa { get; set; }

        [SMCHidden]
        public TipoFiltroCentralSolicitacao TipoFiltroCentralSolicitacao { get; set; } = TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao;

        [SMCHidden]
        public long? SeqTipoServico { get; set; }

        [SMCDescription]
        [SMCKey]
        [SMCHidden]
        public string DescricaoLookupSolicitacao { get; set; }
    }
}