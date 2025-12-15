using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Dynamic;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Util;
using System;
using System.Collections.Generic;
using System.Web;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
	public class MaterialDynamicModel : SMCDynamicViewModel
	{
		#region [ DataSources ]

		[SMCDataSource]
		[SMCIgnoreProp]
		[SMCServiceReference(typeof(IInstituicaoNivelService), nameof(IInstituicaoNivelService.BuscarNiveisEnsinoReconhecidoLDBSelect))]
		public List<SMCDatasourceItem> NiveisEnsino { get; set; }

		#endregion [ DataSources ]

		[SMCKey]
		[SMCHidden]
		public override long Seq { get; set; }

		[SMCHidden]
		[SMCParameter]
		public long SeqOrigemMaterial { get; set; }

		[SMCHidden]
		[SMCParameter]
		public TipoOrigemMaterial TipoOrigemMaterial { get; set; }

		[SMCHidden]
		[SMCParameter]
		public long SeqOrigem { get; set; }

		//Descrição da origem do material.
		[SMCHidden]
		[SMCParameter]
		public string DescricaoOrigem { get; set; }

		[SMCHidden]
		public long? SeqSuperior { get; set; }

		[SMCHidden]
		public long? SeqArquivoAnexado { get; set; }

		[SMCHidden]
		public Guid? UidArquivoAnexado { get; set; }

		[SMCReadOnly]
		[SMCSize(SMCSize.Grid24_24)]
		[SMCConditionalDisplay("SeqSuperior", SMCConditionalOperation.GreaterThen, 0)]
		public string NomePasta { get; set; }

		[SMCSelect]
		[SMCRequired]
		[SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
		public TipoMaterial TipoMaterial { get; set; }

		[SMCRequired]
		[SMCMaxLength(100)]
		[SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid16_24)]
		public string Descricao { get; set; }

		[SMCSize(SMCSize.Grid24_24)]
		[SMCUrl]
		[SMCConditionalDisplay(nameof(TipoMaterial), SMCConditionalOperation.Equals, TipoMaterial.Link)]
		[SMCConditionalRequired(nameof(TipoMaterial), SMCConditionalOperation.Equals, TipoMaterial.Link)]
		public string UrlLink { get; set; }

		[SMCCssClass("smc-sga-upload-linha-unica")]
		[SMCSize(SMCSize.Grid24_24)]
		[SMCConditionalDisplay(nameof(TipoMaterial), SMCConditionalOperation.Equals, TipoMaterial.Arquivo)]
		[SMCConditionalRequired(nameof(TipoMaterial), SMCConditionalOperation.Equals, TipoMaterial.Arquivo)]
		[SMCFile(AreaDownload = "", ActionDownload = "DownloadFileGuid", ControllerDownload = "Home", HideDescription = true)]
		public SMCUploadFile ArquivoAnexado { get; set; }

		[SMCMultiline]
		[SMCSize(SMCSize.Grid24_24)]
		public string Observacao { get; set; }

		[SMCHidden]
		public bool ExibeVisualizacoes { get; set; }

		[SMCDetail]
		[SMCSize(SMCSize.Grid24_24)]
		[SMCConditionalDisplay(nameof(ExibeVisualizacoes), true)]
		//[SMCConditionalReadonly(nameof(TipoMaterial), TipoMaterial.Pasta)]
		public SMCMasterDetailList<MaterialVisualizacaoViewModel> Visualizacoes { get; set; }

		#region Configurações

		public override void ConfigureDynamic(ref SMCDynamicOptions options)
		{
			//NOTE: DynamicUIMVC Caso tenha uma área no sistema lembrar de colocar na route da area o namespace do sistema e do ui.mvc. Procure por DynamicUIMVC
			options
				.EditInModal(allowSaveNew: false)
				.ConfigureContextMenuItem((button, model, action) =>
					{
						bool tipoClassificacaoFolha = ((SMCTreeViewNode<MaterialListarDynamicModel>)model).Value.Folha;
						if (action == SMCDynamicButtonAction.Insert && tipoClassificacaoFolha)
						{
							button.Hide();
						}

						if (action != SMCDynamicButtonAction.Custom)
						{
							button.Hide(true);
						}
					})
				.TreeView(configureNode: x =>
				{
					var item = x as SMCTreeViewNode<MaterialListarDynamicModel>;
					if (item.Value.TipoMaterial == TipoMaterial.Link)
					{
						if (item.Value.DataAlteracao.GetValueOrDefault() != default(DateTime))
						{
							item.Tooltip = (!string.IsNullOrEmpty(item.Value.Observacao) ? item.Value.Observacao + " - " : string.Empty) + item.Value.UrlLink + " - " + item.Value.DataAlteracao.Value.ToString("d") + " - " + item.Value.DataAlteracao.Value.ToString("HH:mm");
						}
						else
						{
							item.Tooltip = (!string.IsNullOrEmpty(item.Value.Observacao) ? item.Value.Observacao + " - " : string.Empty) + item.Value.UrlLink + " - " + item.Value.DataInclusao.ToString("d") + " - " + item.Value.DataInclusao.ToString("HH:mm");
						}
						item.Target = "_blank";
						item.Options.CssClass = "smc-sga-treeview-ico-link";
						return item.Url = item.Value.UrlLink;
					}
					else if (item.Value.TipoMaterial == TipoMaterial.Arquivo)
					{
						if (item.Value.DataAlteracao.GetValueOrDefault() != default(DateTime))
						{
							item.Tooltip = $"{(!string.IsNullOrEmpty(item.Value.Observacao) ? item.Value.Observacao + " - " : string.Empty)} {item.Value.ArquivoAnexado.Nome} ({SMCUploadHelper.FormatFileSize(item.Value.ArquivoAnexado.Tamanho)}) - {item.Value.DataAlteracao.Value.ToString("d")} - {item.Value.DataAlteracao.Value.ToString("HH:mm")}";
						}
						else
						{
							item.Tooltip = $"{(!string.IsNullOrEmpty(item.Value.Observacao) ? item.Value.Observacao + " - " : string.Empty)} {item.Value.ArquivoAnexado.Nome} ({SMCUploadHelper.FormatFileSize(item.Value.ArquivoAnexado.Tamanho)}) - {item.Value.DataInclusao.ToString("d")} - {item.Value.DataInclusao.ToString("HH:mm")}";
						}
						item.Target = "_blank";
						item.Attributes.Add("data-behavior-download", "true");
						item.Options.CssClass = "smc-sga-treeview-ico-arquivo";
						return item.Url = "../Home/DownloadFileGuid?guidFile=" + item.Value.UidArquivoAnexado.Value;
					}
					else if (item.Value.TipoMaterial == TipoMaterial.Pasta)
					{
						if (item.Value.DataAlteracao.GetValueOrDefault() != default(DateTime))
						{
							item.Tooltip = (!string.IsNullOrEmpty(item.Value.Observacao) ? item.Value.Observacao + " - " : string.Empty) + item.Value.DataAlteracao.Value.ToString("d") + " - " + item.Value.DataAlteracao.Value.ToString("HH:mm");
						}
						else
						{
							item.Tooltip = (!string.IsNullOrEmpty(item.Value.Observacao) ? item.Value.Observacao + " - " : string.Empty) + item.Value.DataInclusao.ToString("d") + " - " + item.Value.DataInclusao.ToString("HH:mm");
						}

						return item.Options.CssClass = "smc-sga-treeview-ico-pasta";
					}
					return null;
				})
				.Button(idResource: "NovoMaterial",
						action: "Incluir",
						controller: "Material",
						securityToken: UC_APR_004_01_02.MANTER_MATERIAL,
						routes: model =>
						{
							var node = model as SMCTreeViewNode<MaterialListarDynamicModel>;
							return new
							{
								seqOrigemMaterial = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqOrigemMaterial),
								tipoOrigemMaterial = node.Value.TipoOrigemMaterial,
								seqOrigem = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqOrigem),
								descricaoOrigem = node.Value.DescricaoOrigem,
								seqPai = node.Value.Seq,
								seqSuperior = node.Value.Seq
							};
						},
						isModal: true,
						buttonBehavior: SMCButtonBehavior.Novo,
						htmlAttributes: new { data_modal_title = "Nova pasta/material" },
						displayButton: model =>
						{
							var item = (model as SMCTreeViewNode<MaterialListarDynamicModel>).Value;
							return item.TipoMaterial == TipoMaterial.Pasta;
						})
				.Button(idResource: "AlterarMaterial",
						action: "Editar",
						controller: "Material",
						securityToken: UC_APR_004_01_02.MANTER_MATERIAL,
						routes: model =>
						{
							var node = model as SMCTreeViewNode<MaterialListarDynamicModel>;
							return new
							{
								seq = SMCDESCrypto.EncryptNumberForURL(node.Value.Seq),
								seqOrigemMaterial = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqOrigemMaterial)
							};
						},
						isModal: true,
						htmlAttributes: new { data_modal_title = "Alterar pasta/material" },
						buttonBehavior: SMCButtonBehavior.Alterar)
				.Button(idResource: "ExcluirMaterialPasta",
						action: "Excluir",
						controller: "Material",
						securityToken: UC_APR_004_01_02.MANTER_MATERIAL,
						routes: model =>
						{
							var node = model as SMCTreeViewNode<MaterialListarDynamicModel>;
							return new
							{
								seq = SMCDESCrypto.EncryptNumberForURL(node.Value.Seq),
								seqOrigemMaterial = SMCDESCrypto.EncryptNumberForURL(node.Value.SeqOrigemMaterial)
							};
						},
						buttonBehavior: SMCButtonBehavior.Excluir,
						confirm: model => new SMCDynamicConfirm
						{
							Title = (model as SMCTreeViewNode<MaterialListarDynamicModel>).Value.TipoMaterial == TipoMaterial.Pasta ? "Exclusão de pasta" : "Exclusão de material",
							Message = (model as SMCTreeViewNode<MaterialListarDynamicModel>).Value.TipoMaterial == TipoMaterial.Pasta ?
										string.Format("Confirma a exclusão da pasta '{0}' e de todas suas subpastas e arquivos/links?", (model as SMCTreeViewNode<MaterialListarDynamicModel>).Value.Descricao) :
										string.Format("Confirma a exclusão do material '{0}'?", (model as SMCTreeViewNode<MaterialListarDynamicModel>).Value.Descricao)
						})
				.ModalSize(SMCModalWindowSize.Medium)
				.Service<IMaterialService>(index: nameof(IMaterialService.ListarMateriais),
										   save: nameof(IMaterialService.SalvarMaterial),
										   insert: nameof(IMaterialService.InserirMaterial),
										   delete: nameof(IMaterialService.ExcluirMaterial),
										   edit: nameof(IMaterialService.AlterarMaterial))
				.IgnoreFilterGeneration()
				.Tokens(tokenList: UC_APR_004_01_01.PESQUISAR_MATERIAL,
						tokenEdit: UC_APR_004_01_02.MANTER_MATERIAL,
						tokenRemove: UC_APR_004_01_02.MANTER_MATERIAL,
						tokenInsert: UC_APR_004_01_02.MANTER_MATERIAL)
				.HeaderIndex("BuscarCabecalho");

			//Montando as urls de retorno.
			VerificarBackIndex(options, HttpContext.Current.Request.UrlReferrer?.ToString(), true);
		}

		/// <summary>
		/// Busca primeiro na URL de referência se é alguma conhecida. Se sim, seta no session e ativa o botão back.
		/// Se não, verifica no session se já tem definido a origem.
		/// </summary>
		private void VerificarBackIndex(SMCDynamicOptions options, string checkBack, bool checkSession = false)
		{
			string keySession = "__BACK_MATERIAL";

			if (checkBack != null)
			{
				if (checkBack.Contains("Entidade"))
				{
					HttpContext.Current.Session[keySession] = "Entidade";
					options.ButtonBackIndex("Index", "Entidade", x => new { area = "ORG" });
				}
				else if (checkBack.Contains("InstituicaoEnsino"))
				{
					HttpContext.Current.Session[keySession] = "InstituicaoEnsino";
					options.ButtonBackIndex("Index", "InstituicaoEnsino", x => new { area = "ORG" });
				}
				else if (checkBack.Contains("Curso"))
				{
					HttpContext.Current.Session[keySession] = "Curso";
					options.ButtonBackIndex("Index", "Curso", x => new { area = "CSO" });
				}
				else if (checkBack.Contains("Programa"))
				{
					HttpContext.Current.Session[keySession] = "Programa";
					options.ButtonBackIndex("Index", "Programa", x => new { area = "CSO" });
				}
				else if (checkBack.Contains("SGA.Professor"))
				{
					HttpContext.Current.Session[keySession] = "SGA.Professor";
					options.ButtonBackIndex("Index", "Home", x => new { area = "" });
				}
				else if (checkSession)
				{
					VerificarBackIndex(options, HttpContext.Current.Session[keySession]?.ToString(), false);
				}
			}
			else if (checkSession)
			{
				VerificarBackIndex(options, HttpContext.Current.Session[keySession]?.ToString(), false);
			}
		}

		#endregion Configurações
	}
}