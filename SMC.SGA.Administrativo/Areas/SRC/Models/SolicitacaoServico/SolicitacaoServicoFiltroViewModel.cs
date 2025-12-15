using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.UI.Mvc.Areas.PES.Lookups;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoServicoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        #region DataSources

        [SMCDataSource]
        public List<SMCDatasourceItem> TiposVinculoAluno { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> UsuariosResponsaveis { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Servicos { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> EntidadesResponsaveis { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Etapas { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> SituacoesEtapa { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Processos { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> GruposEscalonamento { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Bloqueios { get; set; }

        #endregion DataSources

        #region Propriedades Auxiliares

        [SMCHidden]
        [SMCDependency(nameof(SeqsProcessos), nameof(SolicitacaoServicoController.PreencherBloquearGrupoEscalonamento), "SolicitacaoServico", true)]
        public bool BloquearGrupoEscalonamento { get; set; }

        [SMCHidden]
        [SMCDependency(nameof(SeqsProcessos), nameof(SolicitacaoServicoController.PreencherBloquearSituacaoDocumentacao), "SolicitacaoServico", true)]
        public bool BloquearSituacaoDocumentacao { get; set; }

        [SMCHidden]
        public bool PrimeiroAcessoPagina { get; set; }

        [SMCHidden]
        [SMCSortable(true, true, "DataSolicitacao")]
        public DateTime DataSolicitacao { get; set; }

        #endregion Propriedades Auxiliares

        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid3_24, SMCSize.Grid3_24)]
        [SMCFilter(true, true)]
        [SMCSortable(true, true, "NumeroProtocolo")]
        public string NumeroProtocolo { get; set; }

        [PessoaLookup]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCFilter(true, true)]
        public PessoaLookupViewModel SeqPessoa { get; set; }

        [SMCSelect(nameof(Servicos), UseCustomSelect = true)]
        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid7_24, SMCSize.Grid7_24)]
        public long? SeqServico { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(Processos), ForceMultiSelect = true, UseCustomSelect = true)]
        [SMCDependency(nameof(SeqServico), nameof(SolicitacaoServicoController.BuscarProcessosPorServicoSelect), "SolicitacaoServico", true)]
        public List<long> SeqsProcessos { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(EntidadesResponsaveis))]
        public List<long> SeqsEntidadesResponsaveis { get; set; }

        [SMCFilter(true, true)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(Etapas))]
        [SMCDependency(nameof(SeqServico), nameof(SolicitacaoServicoController.BuscarEtapasDoServicoSelect), "SolicitacaoServico", true)]
        public long? SeqProcessoEtapa { get; set; }

        [SMCFilter(true, true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        public CategoriaSituacao? CategoriaSituacao { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCSelect(nameof(SituacoesEtapa))]
        [SMCDependency(nameof(SeqProcessoEtapa), nameof(SolicitacaoServicoController.BuscarSituacoesEtapasSgfSelect), "SolicitacaoServico", true, new string[] { nameof(SeqServico) })]
        [SMCFilter(true, true)]
        public long? SeqSituacaoEtapa { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalRequired(nameof(SeqProcessoEtapa), SMCConditionalOperation.GreaterThen, 0, RuleName = "Rule1")]
        [SMCConditionalRequired(nameof(SeqSituacaoEtapa), SMCConditionalOperation.GreaterThen, 0, RuleName = "Rule2")]
        [SMCConditionalReadonly(nameof(SeqProcessoEtapa), SMCConditionalOperation.Equals, "", RuleName = "Rule3")]
        [SMCConditionalReadonly(nameof(SeqSituacaoEtapa), SMCConditionalOperation.Equals, "", RuleName = "Rule4")]
        [SMCConditionalRule("Rule1 && Rule2")]
        [SMCConditionalRule("Rule3 || Rule4")]
        [SMCDependency(nameof(SeqProcessoEtapa), nameof(SolicitacaoServicoController.PreencherTipoFiltroCentralSolicitacaoSelect), "SolicitacaoServico", false, includedProperties: new string[] { nameof(SeqSituacaoEtapa) })]
        [SMCDependency(nameof(SeqSituacaoEtapa), nameof(SolicitacaoServicoController.PreencherTipoFiltroCentralSolicitacaoSelect), "SolicitacaoServico", false, includedProperties: new string[] { nameof(SeqProcessoEtapa) })]
        [SMCFilter(true, true)]
        public TipoFiltroCentralSolicitacao? TipoFiltroCentralSolicitacao { get; set; }

        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCFilter(true, true)]
        [SMCSelect(nameof(UsuariosResponsaveis), UseCustomSelect = true, NameDescriptionField = "NomeFormatado")]
        public long? SeqUsuarioResponsavel { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid5_24)]
        [SMCFilter(true, true)]
        public bool? DisponivelParaAtendimento { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid4_24)]
        [SMCFilter(true, true)]
        public bool? PossuiBloqueio { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCSelect(nameof(GruposEscalonamento), AutoSelectSingleItem = true)]
        [SMCFilter(true, true, AdvancedFilter = true)]
        [SMCDependency(nameof(SeqsProcessos), nameof(SolicitacaoServicoController.BuscarGruposEscalonamentoDoProcessoSelect), "SolicitacaoServico", true)]
        [SMCConditionalReadonly(nameof(BloquearGrupoEscalonamento), true, RuleName = "Rule1")]
        [SMCConditionalReadonly(nameof(SeqsProcessos), SMCConditionalOperation.Equals, 0, RuleName = "Rule2")]
        [SMCConditionalRule("Rule1 || Rule2")]
        public long? SeqGrupoEscalonamento { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCSelect(AutoSelectSingleItem = true)]
        [SMCFilter(true, true, AdvancedFilter = true)]
        [SMCDependency(nameof(SeqsProcessos), nameof(SolicitacaoServicoController.BuscarSituacoesDocumentacaoDoProcessoSelect), "SolicitacaoServico", true)]
        [SMCConditionalReadonly(nameof(BloquearSituacaoDocumentacao), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public SituacaoDocumentacao SituacaoDocumentacao { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        [SMCSelect(nameof(Bloqueios), UseCustomSelect = true)]
        [SMCFilter(true, true, AdvancedFilter = true)]
        [SMCConditionalReadonly(nameof(PossuiBloqueio), SMCConditionalOperation.NotEqual, true, RuleName = "Rule1")]
        [SMCConditionalReadonly(nameof(SeqsProcessos), SMCConditionalOperation.Equals, 0, RuleName = "Rule2")]
        [SMCDependency(nameof(SeqsProcessos), nameof(SolicitacaoServicoController.BuscarBloqueiosDoProcessoSelect), "SolicitacaoServico", true)]
        [SMCConditionalRule("Rule1 || Rule2")]
        public long? SeqBloqueio { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter(true, true, AdvancedFilter = true)]
        public DateTime? DataInclusaoInicio { get; set; }

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24)]
        [SMCFilter(true, true, AdvancedFilter = true)]
        [SMCMinDate(nameof(DataInclusaoInicio))]
        public DateTime? DataInclusaoFim { get; set; }

    }
}