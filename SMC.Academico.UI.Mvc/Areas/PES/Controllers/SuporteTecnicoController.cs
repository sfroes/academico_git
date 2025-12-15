using Newtonsoft.Json;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.PES.Models.SuporteTecnico;
using SMC.Academico.UI.Mvc.Areas.PES.Views.SuporteTecnico.App_LocalResources;
using SMC.EstruturaOrganizacional.ServiceContract.Areas.ESO.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Rest;
using SMC.Framework.Security;
using SMC.Framework.Security.Portable;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.PES.Controllers
{
    public class SuporteTecnicoController : SMCControllerBase
    {
        #region [ Serviços ]

        public IColaboradorService ColaboradorService => Create<IColaboradorService>();

        public IAlunoService AlunoService => Create<IAlunoService>();

        public IEstruturaOrganizacionalService EstruturaOrganizacionalService => Create<IEstruturaOrganizacionalService>();

        #endregion [ Serviços ]

        #region APIS

        public SMCApiClient APISuporteTecnicoChamados => SMCApiClient.Create("SuporteTecnicoChamados");

        #endregion APIS

        /// <summary>
        /// Certificado utilizado para gerar o token de login para autenticação no CSC
        /// Mesmo certificado do \\gtiarqprod01\aplicativos$\webdav\chaveRSA
        /// </summary>
        private static readonly string _certificadoAssinaturaToken = "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";

        /// <summary>
        /// Formato do texto que deve ser assinado para validação do token
        /// </summary>
        private const string _dataFormat = "{0}+{1:yyyy-MM-dd HH:mm}";

        public static void Tsl()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        }

        [SMCAuthorize(UC_PES_001_10_01.CONSULTA_CREDENCIAIS_ACESSO)]
        public ActionResult Index()
        {
            Tsl();

            // Recupera o código de pessoa
            var codigoPessoa = User.SMCGetCodigoPessoa();
            if (codigoPessoa == null)
                throw new SMCApplicationException("Código de pessoa não foi vinculado à este usuário.");

            var model = new SuporteTecnicoViewModel();

            model.CodigoPessoa = codigoPessoa.ToString();
            model.Nome = User.SMCGetNome();
            model.Professor = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_PROFESSOR;
            model.Mobile = false;

            if (model.Professor)
            {
                var seqColaborador = HttpContext.GetEntityLogOn(FILTER.PROFESSOR).Value;
                var colaborador = ColaboradorService.BuscarColaboradores(new ServiceContract.Areas.DCT.Data.ColaboradorFiltroData() { Seq = seqColaborador }).FirstOrDefault();
                model.LoginSolicitante = Convert.ToInt32(model.CodigoPessoa);
                model.NumeroMatricula = null;
                model.DescricaoOrigem = $"SGA.Professor - {colaborador.VinculosAtivos.FirstOrDefault()?.NomeEntidadeVinculo}";

                var modelBuscarServicos = new
                {
                    Aluno = false,
                    Token = "xX8N06mDVLzbQXJ65FxkKhqYxDVZoBFK"
                };

                var retornoServicos = APISuporteTecnicoChamados.Execute<object>("BuscarCatalogosServico", modelBuscarServicos);
                model.Tipos = JsonConvert.DeserializeObject<List<SMCDatasourceItem>>(retornoServicos.ToString());
            }
            else
            {
                var seqPessoaAtuacao = HttpContext.GetEntityLogOn(FILTER.ALUNO).Value;
                var dados = AlunoService.BuscarDadosSuporteTecnico(seqPessoaAtuacao);
                model.LoginSolicitante = null;
                model.NumeroMatricula = dados.NumeroRegistroAcademico;
                model.DescricaoOrigem = dados.DescricaoPessoaAtuacao;

                if (dados.CodigoUnidadeSeoLocalidade.HasValue)
                {
                    var unidade = EstruturaOrganizacionalService.BuscarUnidadePorId(dados.CodigoUnidadeSeoLocalidade.Value);
                    model.CodigoEstabelecimento = unidade.CodigoEstabelecimentoEms;

                    //if (model.DescricaoOrigem.ToUpper().Contains("GRADUAÇÃO") && model.CodigoEstabelecimento != "PPC" && model.CodigoEstabelecimento != "PUB")
                    //{
                    //    model.Tipos.Insert(2, new SMCDatasourceItem() { Seq = 4, Descricao = "Agendamento de Laboratório de Informática" });
                    //}
                }

                var modelBuscarServicos = new
                {
                    Aluno = true,
                    CodigoOrigem = dados.CodigoOrigemFinanceira,
                    model.CodigoEstabelecimento,
                    Token = "xX8N06mDVLzbQXJ65FxkKhqYxDVZoBFK"
                };

                var retornoServicos = APISuporteTecnicoChamados.Execute<object>("BuscarCatalogosServico", modelBuscarServicos);
                model.Tipos = JsonConvert.DeserializeObject<List<SMCDatasourceItem>>(retornoServicos.ToString());
            }

            var view = GetExternalView(AcademicoExternalViews.SUPORTE_TECNICO_INDEX);

            return View(view, model);
        }

        [SMCAllowAnonymous]
        [AllowAnonymous]
        public ActionResult IntegracaoCSC()
        {
            Tsl();

            var codigoPessoa = User.SMCGetCodigoPessoa();
            if (codigoPessoa == null)
                throw new SMCApplicationException("Código de pessoa não foi vinculado à este usuário.");

            var dataToken = DateTime.Now;

            //Cria o token de login assinado pelo certificado que o framework conhece
            string tokenAutenticacao = string.Empty;
            using (var cert = new X509Certificate2(Convert.FromBase64String(_certificadoAssinaturaToken)))
            {
                using (var provider = (RSACryptoServiceProvider)cert.PrivateKey)
                {
                    tokenAutenticacao = Convert.ToBase64String(provider.SignData(Encoding.UTF8.GetBytes(string.Format(_dataFormat, codigoPessoa, dataToken)), new SHA1CryptoServiceProvider()));
                }
            }

            var model = new IntegracaoCSCViewModel()
            {
                Token = tokenAutenticacao,
                CodigoPessoa = (long)codigoPessoa,
                DataAcesso = dataToken,
                IgnoreCache = true,
                UrlGestaoXChamados = ConfigurationManager.AppSettings["UrlGestaoXChamados"]
            };

            if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_PROFESSOR)
            {
                var modelBuscarServicos = new
                {
                    Aluno = false,
                    Token = TOKEN_CSC.TOKEN_CHAMADOS
                };

                var retornoServicos = APISuporteTecnicoChamados.Execute<object>("BuscarCatalogosServico", modelBuscarServicos);
                var listaServicos = JsonConvert.DeserializeObject<List<SMCDatasourceItem>>(retornoServicos.ToString());
                model.CodigosServicosUsuarios = listaServicos.Select(a => (int)a.Seq).ToList();
            }
            else
            {
                var seqPessoaAtuacao = HttpContext.GetEntityLogOn(FILTER.ALUNO).Value;
                var dados = AlunoService.BuscarDadosSuporteTecnico(seqPessoaAtuacao);

                if (dados.CodigoUnidadeSeoLocalidade.HasValue)
                {
                    var unidade = EstruturaOrganizacionalService.BuscarUnidadePorId(dados.CodigoUnidadeSeoLocalidade.Value);
                    model.CodigoEstabelecimento = unidade.CodigoEstabelecimentoEms;
                }

                model.NumeroRegistroAcademico = dados.NumeroRegistroAcademico;
                model.NomeCurso = dados.NomeCurso;
                model.DescricaoTurno = dados.DescricaoTurno;
                model.DescricaoUnidade = dados.DescricaoUnidade;

                var modelBuscarServicos = new
                {
                    Aluno = true,
                    Token = TOKEN_CSC.TOKEN_CHAMADOS
                };

                var retornoServicos = APISuporteTecnicoChamados.Execute<object>("BuscarCatalogosServico", modelBuscarServicos);
                var listaServicos = JsonConvert.DeserializeObject<List<SMCDatasourceItem>>(retornoServicos.ToString());
                model.CodigosServicosUsuarios = listaServicos.Select(a => (int)a.Seq).ToList();
            }

            var view = GetExternalView(AcademicoExternalViews.INTEGRACAO_CSC);

            return View(view, model);
        }

        [SMCAllowAnonymous]
        [AllowAnonymous]
        public ActionResult IndexMobile(string CodigoPessoa, string Nome, string Professor, string DescricaoOrigem, string Matricula, string Estabelecimento, string CodOrigem)
        {
            Tsl();

            var codigoPessoa = SMCSecTokenHelper.DecryptyForURL(CodigoPessoa);
            var professor = Convert.ToBoolean(Professor);

            var dataToken = DateTime.Now;

            //Cria o token de login assinado pelo certificado que o framework conhece
            string tokenAutenticacao = string.Empty;
            using (var cert = new X509Certificate2(Convert.FromBase64String(_certificadoAssinaturaToken)))
            {
                using (var provider = (RSACryptoServiceProvider)cert.PrivateKey)
                {
                    tokenAutenticacao = Convert.ToBase64String(provider.SignData(Encoding.UTF8.GetBytes(string.Format(_dataFormat, codigoPessoa, dataToken)), new SHA1CryptoServiceProvider()));
                }
            }

            var model = new IntegracaoCSCViewModel()
            {
                Token = tokenAutenticacao,
                CodigoPessoa = Convert.ToInt64(codigoPessoa),
                DataAcesso = dataToken,
                IgnoreCache = true,
                Mobile = true,
                UrlGestaoXChamados = ConfigurationManager.AppSettings["UrlGestaoXChamados"]
            };

            if (professor)
            {
                var modelBuscarServicos = new
                {
                    Aluno = false,
                    Token = TOKEN_CSC.TOKEN_CHAMADOS
                };

                var retornoServicos = APISuporteTecnicoChamados.Execute<object>("BuscarCatalogosServico", modelBuscarServicos);
                var listaServicos = JsonConvert.DeserializeObject<List<SMCDatasourceItem>>(retornoServicos.ToString());
                model.CodigosServicosUsuarios = listaServicos.Select(a => (int)a.Seq).ToList();
            }
            else
            {
                model.CodigoEstabelecimento = SMCSecTokenHelper.DecryptyForURL(Estabelecimento);

                var modelBuscarServicos = new
                {
                    Aluno = true,
                    Token = TOKEN_CSC.TOKEN_CHAMADOS
                };

                var retornoServicos = APISuporteTecnicoChamados.Execute<object>("BuscarCatalogosServico", modelBuscarServicos);
                var listaServicos = JsonConvert.DeserializeObject<List<SMCDatasourceItem>>(retornoServicos.ToString());
                model.CodigosServicosUsuarios = listaServicos.Select(a => (int)a.Seq).ToList();
            }

            var view = GetExternalView(AcademicoExternalViews.INTEGRACAO_CSC);

            return View(view, model);
        }

        [SMCAuthorize(UC_PES_001_10_01.CONSULTA_CREDENCIAIS_ACESSO)]
        public ActionResult IncluirSuporte(SuporteTecnicoViewModel model)
        {
            try
            {
                Tsl();

                model.UsuarioTeams = model.CodigoPessoa.ToString() + "@sga.pucminas.br";
                model.Token = "xX8N06mDVLzbQXJ65FxkKhqYxDVZoBFK";

                object retorno = APISuporteTecnicoChamados.Execute<object>("AbrirChamado", model);

                string mensagem = string.Format(UIResource.Chamado_Cadastrado, retorno.ToString());

                SetSuccessMessage(mensagem, UIResource.Chamado_Cadastrado_Titulo);

                return SMCRedirectToAction("Index", "SuporteTecnicoRoute", new { area = "" });
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                return ThrowOpenModalException(ex.Message);
            }
        }

        [SMCAllowAnonymous]
        [AllowAnonymous]
        public ActionResult IncluirSuporteMobile(SuporteTecnicoViewModel model)
        {
            try
            {
                Tsl();

                model.UsuarioTeams = model.CodigoPessoa.ToString() + "@sga.pucminas.br";
                model.Token = "xX8N06mDVLzbQXJ65FxkKhqYxDVZoBFK";

                object retorno = APISuporteTecnicoChamados.Execute<object>("AbrirChamado", model);

                string mensagem = string.Format(UIResource.Chamado_Cadastrado, retorno.ToString());

                SetSuccessMessage(mensagem, UIResource.Chamado_Cadastrado_Titulo, SMCMessagePlaceholders.Centro);

                string CodigoPessoa = SMCSecTokenHelper.EncryptForURL(model.CodigoPessoa);
                string Nome = SMCSecTokenHelper.EncryptForURL(model.Nome);
                string Professor = model.Professor.ToString();
                string DescricaoOrigem = SMCSecTokenHelper.EncryptForURL(model.DescricaoOrigem);

                if (model.Professor)
                {
                    return SMCRedirectToAction("IndexMobile", "SuporteTecnicoRoute", new { CodigoPessoa, Nome, Professor, DescricaoOrigem });
                }
                else
                {
                    string Matricula = SMCSecTokenHelper.EncryptForURL(model.NumeroMatricula.ToString());
                    string Estabelecimento = SMCSecTokenHelper.EncryptForURL(model.CodigoEstabelecimento.ToString());
                    string CodOrigem = SMCSecTokenHelper.EncryptForURL(model.CodigoOrigem.ToString());
                    return SMCRedirectToAction("IndexMobile", "SuporteTecnicoRoute", new { CodigoPessoa, Nome, Professor, DescricaoOrigem, Matricula, Estabelecimento, CodOrigem });
                }
            }
            catch (Exception ex)
            {
                ex = ex.SMCLastInnerException();
                return ThrowOpenModalException(ex.Message);
            }
        }

        [SMCAllowAnonymous]
        [AllowAnonymous]
        public ActionResult ListaDadosDescricao(long? CatalogoServicoId, string DescricaoCatalogo)
        {
            if (CatalogoServicoId == 4983 || DescricaoCatalogo.Contains("Agendamento"))
            {
                var descricao = new StringBuilder();
                descricao.AppendLine("Data:");
                descricao.AppendLine("Horário:");
                descricao.AppendLine("Em qual unidade quer reservar o laboratório:");
                descricao.AppendLine("Justificativa:");
                return Json(descricao.ToString());
            }
            else
            {
                return Json(String.Empty);
            }
        }
    }
}