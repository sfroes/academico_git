using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using SMC.Localidades.ServiceContract.Areas.LOC.Data;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
	public class SolicitacaoAtividadeComplementarDomainService : AcademicoContextDomain<SolicitacaoAtividadeComplementar>
	{
		#region [Service]

		private ILocalidadeService LocalidadeService => Create<ILocalidadeService>();

		#endregion [Service]

		#region [DomainService]

		private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

		#endregion [DomainService]

		/// <summary>
		/// Atualiza a descrição de uma solicitação de atividade complementar
		/// </summary>
		/// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser atualizada</param>
		/// <param name="indOriginal">Flag para atualizar a descrição original ou atualizada</param>
		public string AtualizarDescricao(long seqSolicitacaoServico, bool indOriginal)
		{
			// Busca os dados da solicitação de atividade complementar
			var spec = new SMCSeqSpecification<SolicitacaoAtividadeComplementar>(seqSolicitacaoServico);
			var dadosSolicitacao = this.SearchProjectionByKey(spec, x => new
			{
				Tipo = x.DivisaoComponente.TipoDivisaoComponente.Descricao,
				AtividadeComplementar = x.DivisaoComponente.ConfiguracaoComponente.Descricao,
				Descricao = x.Descricao,
				DescricaoCicloLetivo = x.CicloLetivo.Descricao,
				CargaHoraria = x.CargaHoraria,
				DataInicio = x.DataInicio,
				DataFim = x.DataFim,
				Nota = x.Nota,
				Faltas = x.Faltas,
				Situacao = x.SituacaoHistoricoEscolar,
				SeqEscalaApuracaoItem = x.SeqEscalaApuracaoItem,
				DescricaoEscalaApuracaoItem = x.EscalaApuracaoItem.Descricao,

				Artigo = x.SolicitacaoArtigo.Select(a => new
				{
					TipoPublicacao = (TipoPublicacao?)a.TipoPublicacao,

					SeqQualisPeriodico = (long?)a.QualisPeriodico.Seq,
					CodigoIssn = a.QualisPeriodico.CodigoISSN,
					DescricaoPeriodico = a.QualisPeriodico.Periodico.Descricao,
					AreaPeriodico = a.QualisPeriodico.DescricaoAreaAvaliacao,
					QualisCapesPeriodico = (QualisCapes?)a.QualisPeriodico.QualisCapes,
					Volume = a.NumeroVolumePeriodico,
					Fasciculo = a.NumeroFasciculoPeriodico,
					PaginaInicial = a.NumeroPaginaInicialPeriodico,
					PaginaFinal = a.NumeroPaginaFinalPeriodico,

					DescricaoConferencia = a.DescricaoEvento,
					AnoRealizacao = a.AnoRealizacaoEvento,
					NaturezaConferencia = a.NaturezaArtigo,
					TipoEvento = a.TipoEvento,
					UFEvento = a.UfEvento,
					CidadeEvento = a.CodCidadeEvento
				}).FirstOrDefault()
			});

			if (dadosSolicitacao != null)
			{
				// Formata a Descrição
				string templateItem = "<div class=\"{2}\"><div class=\"smc-display\"><label>{0}</label><p>{1}</p></div></div>";
				string templateFieldset = "<fieldset><legend>{0}</legend>{1}</fieldset>";

				string itemTipo = string.Format(templateItem, "Tipo", dadosSolicitacao.Tipo, SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
				string itemAtiv = string.Format(templateItem, "Atividade complementar", dadosSolicitacao.AtividadeComplementar, SMCSizeHelper.GetSizeClasses(SMCSize.Grid18_24, SMCSize.Grid12_24, SMCSize.Grid18_24, SMCSize.Grid18_24));
				string itemDesc = string.Format(templateItem, "Descrição", dadosSolicitacao.Descricao, SMCSizeHelper.GetSizeClasses(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24));
				string itemCiclo = string.Format(templateItem, "Ciclo letivo", dadosSolicitacao.DescricaoCicloLetivo, SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
				string itemCH = string.Format(templateItem, "Carga horária", dadosSolicitacao.CargaHoraria?.ToString() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
				string itemIni = string.Format(templateItem, "Data início", dadosSolicitacao.DataInicio?.ToString("dd/MM/yyyy") ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
				string itemFim = string.Format(templateItem, "Data fim", dadosSolicitacao.DataFim?.ToString("dd/MM/yyyy") ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid12_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
				string fdDados = string.Format(templateFieldset, "Dados gerais", itemTipo + itemAtiv + itemDesc + itemCiclo + itemCH + itemIni + itemFim);

				string fdInfoArtigo = string.Empty;
				if (dadosSolicitacao.Artigo != null && dadosSolicitacao.Artigo.TipoPublicacao.HasValue)
				{
					string itemTipoPub = string.Format(templateItem, "Tipo de publicação", dadosSolicitacao.Artigo.TipoPublicacao.SMCGetDescription(), SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
					switch (dadosSolicitacao.Artigo.TipoPublicacao.Value)
					{
						case TipoPublicacao.Conferencia:
							// Se uf e cidade preenchidos, busca o nome da cidade
							CidadeData cidade = null;
							if (!string.IsNullOrEmpty(dadosSolicitacao.Artigo.UFEvento) && dadosSolicitacao.Artigo.CidadeEvento.HasValue)
							{
								cidade = LocalidadeService.BuscarCidade(dadosSolicitacao.Artigo.CidadeEvento.Value, dadosSolicitacao.Artigo.UFEvento);
							}
							string itemDscConf = string.Format(templateItem, "Conferência", dadosSolicitacao.Artigo.DescricaoConferencia ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid18_24));
							string itemAnoEv = string.Format(templateItem, "Ano realização", dadosSolicitacao.Artigo.AnoRealizacao?.ToString() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							string itemNatureza = string.Format(templateItem, "Natureza", dadosSolicitacao.Artigo.NaturezaConferencia?.SMCGetDescription() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							string itemTipoEv = string.Format(templateItem, "Tipo de evento", dadosSolicitacao.Artigo.TipoEvento?.SMCGetDescription() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							string itemUF = string.Format(templateItem, "UF", dadosSolicitacao.Artigo.UFEvento ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							string itemCidade = string.Format(templateItem, "Cidade", cidade?.Nome ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							fdInfoArtigo = string.Format(templateFieldset, "Informações do artigo", itemDscConf + itemAnoEv + itemNatureza + itemTipoEv + itemUF + itemCidade);
							break;

						case TipoPublicacao.Periodico:
							string descPeriodico = string.Format("{0} - {1} - {2} - {3}", dadosSolicitacao.Artigo.CodigoIssn, dadosSolicitacao.Artigo.DescricaoPeriodico, dadosSolicitacao.Artigo.AreaPeriodico, dadosSolicitacao.Artigo.QualisCapesPeriodico?.SMCGetDescription());
							string itemPeriodico = string.Format(templateItem, "Periódico", dadosSolicitacao.Artigo.SeqQualisPeriodico.HasValue ? descPeriodico : "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid24_24));
							string itemVolume = string.Format(templateItem, "Volume", dadosSolicitacao.Artigo.Volume?.ToString() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							string itemFasciculo = string.Format(templateItem, "Fascículo", dadosSolicitacao.Artigo.Fasciculo?.ToString() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							string itemPgInicio = string.Format(templateItem, "Página inicial", dadosSolicitacao.Artigo.PaginaInicial?.ToString() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							string itemPgFim = string.Format(templateItem, "Página final", dadosSolicitacao.Artigo.PaginaFinal?.ToString() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24));
							fdInfoArtigo = string.Format(templateFieldset, "Informações do artigo", itemPeriodico + itemVolume + itemFasciculo + itemPgInicio + itemPgFim);
							break;
					}
				}

				string fdNotas = string.Empty;
				if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
				{
					string itemNota = string.Format(templateItem, "Nota", dadosSolicitacao.Nota?.ToString() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid3_24));
					string itemApuracao = string.Format(templateItem, "Apuração", dadosSolicitacao.DescricaoEscalaApuracaoItem ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid3_24));
					string itemFaltas = string.Format(templateItem, "Faltas", dadosSolicitacao.Faltas?.ToString() ?? "-", SMCSizeHelper.GetSizeClasses(SMCSize.Grid3_24));
					string itemSituacaoFinal = string.Format(templateItem, "Situação final", SMCEnumHelper.GetDescription(dadosSolicitacao.Situacao.GetValueOrDefault()), SMCSizeHelper.GetSizeClasses(SMCSize.Grid3_24));
					fdNotas = string.Format(templateFieldset, "Lançamento de nota", itemNota + itemApuracao + itemFaltas + itemSituacaoFinal);
				}
				string novaDescricao = string.Format("<div class=\"smc-sga-confirmacao-dados\">{0}{1}{2}</div>", fdDados, fdInfoArtigo, fdNotas);

				// Salva a Descrição
				if (indOriginal)
				{
					this.UpdateFields<SolicitacaoAtividadeComplementar>(new SolicitacaoAtividadeComplementar { Seq = seqSolicitacaoServico, DescricaoOriginal = novaDescricao }, x => x.DescricaoOriginal);
				}
				else
				{
					this.UpdateFields<SolicitacaoAtividadeComplementar>(new SolicitacaoAtividadeComplementar { Seq = seqSolicitacaoServico, DescricaoAtualizada = novaDescricao }, x => x.DescricaoAtualizada);
				}

				return novaDescricao;
			}
			return null;
		}

		/// <summary>
		/// Realiza o deferimento de uma solicitação de ativicade complementar (RN_SRC_055)
		/// </summary>
		/// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser deferida</param>
		public void DeferirSolicitacaoAtividadeComplementar(long seqSolicitacaoServico)
		{
			// Atualiza a descrição atualizada da solicitação
			AtualizarDescricao(seqSolicitacaoServico, false);

			// Salva o histórico escolar da atividade complementar deferida
			HistoricoEscolarDomainService.SalvarHistoricoAtividadeComplementar(seqSolicitacaoServico);
		}

		/// <summary>
		/// Realiza a reabertura de uma solicitação de atividade complementar (RN_SRC_058)
		/// </summary>
		/// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço a ser reaberta</param>
		public void ReabrirSolicitacao(long seqSolicitacaoServico)
		{
			// Exclui o histórico escolar criado pela solicitação
			HistoricoEscolarDomainService.ExcluirHistoricoEscolarPorSolicitacao(seqSolicitacaoServico);

			// Limpa a descrição atualizada da solicitação de dispensa
			var solicitacaoAtividade = new SolicitacaoAtividadeComplementar()
			{
				Seq = seqSolicitacaoServico,
				DescricaoAtualizada = null
			};
			this.UpdateFields<SolicitacaoAtividadeComplementar>(solicitacaoAtividade, x => x.DescricaoAtualizada);
		}
	}
}