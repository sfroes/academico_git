using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoIntercambioParticipanteViewModel : SMCViewModelBase
    {
        [SMCDataSource]
        [SMCIgnoreProp]
        public List<SMCDatasourceItem> Instituicoes { get; set; }

        [SMCDependency(nameof(AtendimentoIntercambioViewModel.SeqTipoOrientacao), "BuscarDadosTipoOrientacao", "RealizarAtendimento", true, nameof(AtendimentoIntercambioViewModel.SeqTermoIntercambio), nameof(AtendimentoIntercambioViewModel.SeqPessoaAtuacao))]
        [SMCSelect(nameof(AtendimentoIntercambioViewModel.Colaboradores), UseCustomSelect = true, AutoSelectSingleItem = true)]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid6_24)]
        [SMCUnique]
        public long? SeqColaborador { get; set; }

        [SMCDependency(nameof(AtendimentoIntercambioViewModel.SeqTipoOrientacao), "BuscarDadosTipoOrientacao", "RealizarAtendimento", true, nameof(AtendimentoIntercambioViewModel.SeqTermoIntercambio), nameof(AtendimentoIntercambioViewModel.SeqPessoaAtuacao))]

        [SMCSelect(nameof(AtendimentoIntercambioViewModel.TiposParticipacoes), autoSelectSingleItem: true)]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        [SMCRequired]
        public TipoParticipacaoOrientacao? TipoParticipacaoOrientacao { get; set; }

        [SMCSelect(nameof(Instituicoes), autoSelectSingleItem: true)]
        [SMCConditionalReadonly(nameof(SeqColaborador), null, RuleName = "R1")]
        [SMCConditionalReadonly(nameof(TipoParticipacaoOrientacao), null, RuleName = "R2")]
		[SMCConditionalReadonly("CotutelaJaAssociada", true, PersistentValue = true, RuleName = "R3")]
		[SMCConditionalRule("R1 || R2 || R3")]
        [SMCDependency(nameof(SeqColaborador), "BuscarDadosColaborador", "RealizarAtendimento", true, nameof(AtendimentoIntercambioViewModel.SeqTermoIntercambio), nameof(AtendimentoIntercambioViewModel.SeqPessoaAtuacao), nameof(AtendimentoIntercambioViewModel.SeqTipoOrientacao), nameof(TipoParticipacaoOrientacao))]
        [SMCDependency(nameof(TipoParticipacaoOrientacao), "BuscarDadosColaborador", "RealizarAtendimento", true, nameof(AtendimentoIntercambioViewModel.SeqTermoIntercambio), nameof(AtendimentoIntercambioViewModel.SeqPessoaAtuacao), nameof(AtendimentoIntercambioViewModel.SeqTipoOrientacao), nameof(SeqColaborador))]
        [SMCSize(Framework.SMCSize.Grid6_24)]
        [SMCRequired]
        public long? SeqInstituicaoExterna { get; set; }

		[SMCRequired]
		[SMCSize(Framework.SMCSize.Grid3_24)]
		[SMCMaxDate(nameof(DataFim))]
		[SMCConditionalReadonly("ExigePeriodo", SMCConditionalOperation.NotEqual, false, PersistentValue = true)]
		[SMCDependency("SeqTermoIntercambio", "PreencherDatasTermo", "RealizarAtendimento", "SRC", false, "SeqPessoaAtuacao", "DataInicio", "DataFim", "ExigePeriodo")]
		public DateTime? DataInicio { get; set; }

		[SMCSize(Framework.SMCSize.Grid3_24)]
		[SMCMinDate(nameof(DataInicio))]
		[SMCConditionalReadonly("ExigePeriodo", SMCConditionalOperation.NotEqual, false, PersistentValue = true)]
		[SMCDependency("SeqTermoIntercambio", "PreencherDatasTermo", "RealizarAtendimento", "SRC", false, "SeqPessoaAtuacao", "DataInicio", "DataFim", "ExigePeriodo")]
		public DateTime? DataFim { get; set; }

		[SMCHidden]
		public long? SeqOrientacaoColaborador { get; set; }
	}
}