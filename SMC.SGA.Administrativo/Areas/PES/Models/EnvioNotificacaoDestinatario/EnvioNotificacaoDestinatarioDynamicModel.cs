using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class EnvioNotificacaoDestinatarioDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqNotificacaoEmail { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string Assunto { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        public string Remetente { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string UsuarioEnvio { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCDateTimeMode(SMCDateTimeMode.Date)]
        public DateTime? DataEnvio { get; set; }

        [SMCHidden]
        public long SeqNotificacaoEmailDestinatario { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .HeaderIndex("BuscarCabecalho")
                .Service<IEnvioNotificacaoDestinatarioService>(index: nameof(IEnvioNotificacaoDestinatarioService.BuscarNotificacoesDestinatario))
                .Tokens(tokenInsert: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                        tokenRemove: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                        tokenEdit: SMCSecurityConsts.SMC_DENY_AUTHORIZATION)
                .Grid(numberOfButtonsToShow: 3)
                .Button(idResource: "Visualizar",
                        action: "ConsultarDetalheNotificacao",
                        controller: "SolicitacaoServico",
                        securityToken: UC_SRC_004_01_05.CONSULTAR_DADOS_NOTIFICACAO,
                        htmlAttributes: new
                        {
                            data_modal_title = "Detalhes da notificação",
                            data_behavior = "data-modal-open",
                            data_idmodal = "modalVisualizarDetalheNotificacao",
                            data_ajax = "true",
                            data_ajax_method = "Post",
                            data_ajax_mode = "replace"
                        },
                        routes: r => new
                        {
                            area = "SRC",
                            seqNotificacaoEmail = SMCDESCrypto.EncryptForURL(((EnvioNotificacaoDestinatarioListarDynamicModel)r).SeqNotificacaoEmailDestinatario.ToString())
                        },
                        cssClass: "smc-btn-visualizar-ico");

            VerificarBackIndex(options, HttpContext.Current.Request.UrlReferrer?.ToString(), true);
        }

        /// <summary>
        /// Busca primeiro na URL de referência se é alguma conhecida. Se sim, seta no session e ativa o botão back.
        /// Se não, verifica no session se já tem definido a origem.
        /// </summary>
        private void VerificarBackIndex(SMCDynamicOptions options, string checkBack, bool checkSession = false)
        {
            string keySession = "__BACK_NOTIFICACAO";

            if (checkBack != null)
            {
                if (checkBack.Contains("Aluno"))
                {
                    HttpContext.Current.Session[keySession] = "Aluno";
                    options.ButtonBackIndex("Index", "Aluno", x => new { area = "ALN" });
                }
                else if (checkBack.Contains("Colaborador"))
                {
                    HttpContext.Current.Session[keySession] = "Colaborador";
                    options.ButtonBackIndex("Index", "Colaborador", x => new { area = "DCT" });
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
    }
}