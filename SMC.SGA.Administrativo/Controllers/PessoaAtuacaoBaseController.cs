using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.DCT.Models;
using SMC.SGA.Administrativo.Models;
using SMC.SGA.Administrativo.Views.Shared.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Controllers
{
    public abstract class PessoaAtuacaoBaseController<T> : SMCDynamicControllerBase
        where T : PessoaAtuacaoViewModel, ISMCWizardViewModel, new()
    {
        #region [ Services ]

        private IIngressanteService IngressanteService { get => Create<IIngressanteService>(); }

        protected IPessoaService PessoaService
        {
            get { return this.Create<IPessoaService>(); }
        }

        #endregion [ Services ]

        protected const int PASSO_SELECAO = 0;

        protected const int PASSO_DADOS_PESSOAIS = 1;

        protected const int PASSO_CONTATOS = 2;

        public virtual ActionResult Selecao(T model)
        {
            this.ConfigureDynamic(model);

            if (model.UtilizarMesmaPessoa.HasValue || model.CadastrarNovaPessoa)
            {
                model.PessoasExistentes = new SMCPagerModel<PessoaExistenteListaViewModel>(this.BuscarPessoasExistentes(model, true));
            }
            if (!model.CadastrarNovaPessoa && model.SelectedValues.HasValue)
            {
                model.PessoasExistentes.SelectedValues = new List<object>() { model.SelectedValues };
            }

            model.PessoaSelecionada = false;

            return ViewWizard(model);
        }

        public virtual ActionResult BuscarPessoaExistente(T model)
        {
            this.ConfigureDynamic(model);

            var pessoas = this.BuscarPessoasExistentes(model, true);
            model.PessoasExistentes = new SMCPagerModel<PessoaExistenteListaViewModel>(pessoas)
            {
                SelectedValues = pessoas.Where(w => w.Selecionado).Select(s => s.Seq).TransformList<object>()
            };
            // Se não houver um nome no banco para a pessoa, utiliza a da pesquisa. Acontece apenas quando não há uma pessoa atuação.
            // (Cadastros ainda não finalizados)
            foreach (var pessoa in model.PessoasExistentes)
            {
                if (string.IsNullOrWhiteSpace(pessoa.Nome))
                    pessoa.Nome = model.Nome;
            }
            model.UtilizarMesmaPessoa = model.PessoasExistentes.SelectedValues.Count > 0;
            model.PessoaLocalizadaComMesmoDocumento = !string.IsNullOrEmpty(model.IdentificacaoCpf)
                && model.PessoasExistentes.Any(a => a.Cpf?.SMCRemoveNonDigits() == model.IdentificacaoCpf.SMCRemoveNonDigits()
                                                 && (a.Nome != model.IdentificacaoNome || a.DataNascimento != model.IdentificacaoDataNascimento));
            this.ConfigureDynamic(model);

            return PartialView("_IdentificacaoPessoaExistenteResultado", model);
        }

        public virtual ActionResult DadosPessoais(T model)
        {
            var tipoAtuacaoModel = model.TipoAtuacao;

            if (!model.PessoaSelecionada)
            {
                // Recupera os valores atuais do form para não ser afetado pelo merge do wizard caso o usuário volte para o passo de identificação
                model.SelectedValues = Int64.TryParse(Request.Form[nameof(model.SelectedValues)], out long selectedValue) ? new long?(selectedValue) : null;
                model.CadastrarNovaPessoa = Boolean.TryParse(Request.Form[nameof(model.CadastrarNovaPessoa)], out bool cadastrarNovaPessoa) ? cadastrarNovaPessoa : false;
            }

            this.ConfigureDynamic(model);

            if (model.CadastrarNovaPessoa)
            {
                // Instância um novo modelo para não manter nenhum dado caso seja para cadastar uma nova pessoa
                var novaPessoa = BuscarConfiguracao();

                novaPessoa.Cpf = novaPessoa.IdentificacaoCpf = model.IdentificacaoCpf;
                novaPessoa.DataNascimento = novaPessoa.IdentificacaoDataNascimento = model.IdentificacaoDataNascimento;
                novaPessoa.Nome = novaPessoa.IdentificacaoNome = model.IdentificacaoNome;
                novaPessoa.NumeroPassaporte = novaPessoa.IdentificacaoNumeroPassaporte = model.IdentificacaoNumeroPassaporte;
                novaPessoa.TipoNacionalidade = novaPessoa.IdentificacaoTipoNacionalidade = model.IdentificacaoTipoNacionalidade;
                novaPessoa.SeqInstituicaoEnsino = model.SeqInstituicaoEnsino;
                novaPessoa.Step = model.Step;
                novaPessoa.CadastrarNovaPessoa = model.CadastrarNovaPessoa;
                novaPessoa.PermitirAlterarDadosPessoaAtuacao = model.PermitirAlterarDadosPessoaAtuacao;
                novaPessoa.PermitirAlterarDadosPessoaAtuacaoNomeSocial = model.PermitirAlterarDadosPessoaAtuacaoNomeSocial;
                model = novaPessoa;

                //Validar pessoa desligada no CAD
                IngressanteService.ValidarBloqueioPessoa(model.Transform<IngressanteData>());
                
                // Caso exista um cadastro sem dados pessoais, aproveita este para não duplicar cpf ou passaporte.
                // Ao avançar para a aba de contado já é criado um registro de pessoa sem dados pessoais.
                var cadastroSemDadosPessoaisExistente = this.BuscarPessoasExistentes(model, false).FirstOrDefault();
                if (cadastroSemDadosPessoaisExistente != null)
                    model.SeqPessoa = cadastroSemDadosPessoaisExistente.Seq;
                model.PessoaSelecionada = true;
                model.TipoAtuacaoAuxiliar = tipoAtuacaoModel;
            }
            else if (model.SelectedValues.HasValue)
            {
                if (model.SelectedValues != model.SeqPessoa)
                {                   

                    var pessoa = this.PessoaService.BuscarPessoa(new PessoaFiltroData { Seq = model.SelectedValues.Value });
                                        
                    // Preserva apenas os campos de controle de seleção
                    var oldModel = model;
                    model = pessoa.Transform<T>(BuscarConfiguracao());
                    model.Step = oldModel.Step;
                    model.UtilizarMesmaPessoa = oldModel.UtilizarMesmaPessoa;
                    model.PessoaLocalizadaComMesmoDocumento = oldModel.PessoaLocalizadaComMesmoDocumento;
                    model.CadastrarNovaPessoa = oldModel.CadastrarNovaPessoa;
                    model.SelectedValues = oldModel.SelectedValues;
                    model.PermitirAlterarDadosPessoaAtuacaoNomeSocial = oldModel.PermitirAlterarDadosPessoaAtuacaoNomeSocial;
                    model.PermitirAlterarDadosPessoaAtuacao = oldModel.PermitirAlterarDadosPessoaAtuacao;
                    model.IdentificacaoCpf = oldModel.IdentificacaoCpf;
                    model.IdentificacaoDataNascimento = oldModel.IdentificacaoDataNascimento;
                    model.IdentificacaoNome = oldModel.IdentificacaoNome;
                    model.IdentificacaoNumeroPassaporte = oldModel.IdentificacaoNumeroPassaporte;
                    model.IdentificacaoTipoNacionalidade = oldModel.IdentificacaoTipoNacionalidade;
                    if (model is ColaboradorDynamicModel)
                    {
                        (model as ColaboradorDynamicModel).SeqHierarquiaClassificacao = (oldModel as ColaboradorDynamicModel).SeqHierarquiaClassificacao;
                    }

                    //FIX: Verificar falha transform enuns
                    model.Sexo = pessoa.Sexo;
                    model.RacaCor = pessoa.RacaCor;
                    model.TipoNacionalidade = pessoa.TipoNacionalidade;
                    // Passa o SeqPessoa para o campo adequado
                    model.SeqPessoa = model.Seq;
                    model.Seq = 0;

                    //Validar pessoa desligada no CAD
                    IngressanteService.ValidarBloqueioPessoa(model.Transform<IngressanteData>());
                }

                model.TipoAtuacaoAuxiliar = tipoAtuacaoModel;
                model.PessoaSelecionada = true;
            }
            else if (!model.PessoaSelecionada)
            {
                // Caso não seja selecionada nenhuma opção ou sejam selecionadas as duas
                SetErrorMessage(UIResource.ERR_SelecaoPessoaObrigatoria, target: SMCMessagePlaceholders.Centro);
                model.CadastrarNovaPessoa = false;
                model.SelectedValues = null;
                model.Step = PASSO_SELECAO;
                BuscarPessoaExistente(model);

                model.TipoAtuacaoAuxiliar = tipoAtuacaoModel;

                // Retorna direto para não executar o clear message
                return SMCViewWizard(model, null);
            }

            return ViewWizard(model);
        }

        public virtual ActionResult Contatos(T model)
        {
            this.ConfigureDynamic(model);

            PessoaService.ValidarQuantidadesFiliacao(model.Transform<PessoaData>(), model.TipoAtuacao);

            // Caso ainda não tenha um registro de pessoa,
            // cria um novo registro sem dados pessoais antes mesmo de gravar o colaborador
            // para possibilitar associar os contatos a um registro de pessoa
            if (model.SeqPessoa == 0)
            {
                try
                {
                    model.SeqPessoa = this.PessoaService.SalvarPessoa(model.Transform<PessoaData>());
                }
                catch (Exception ex)
                {
                    model.Step = PASSO_DADOS_PESSOAIS;
                    throw ex;
                }
            }

            return ViewWizard(model);
        }

        /// <summary>
        /// Por padrão retorna uma nova instância de T.
        /// Pode ser sobrescrito para buscar as configurações iniciais no serviço.
        /// </summary>
        /// <returns>Novo modelo configurado</returns>
        public virtual T BuscarConfiguracao()
        {
            return new T();
        }

        protected ActionResult ViewWizard(T model)
        {
            ClearMessages(SMCMessagePlaceholders.Centro);
            return SMCViewWizard(model, null);
        }

        protected List<PessoaExistenteListaViewModel> BuscarPessoasExistentes(T model, bool dadosPessoaisCadastrados)
        {
            PessoaFiltroData filtroPessoa = new PessoaFiltroData()
            {
                TipoNacionalidade = model.IdentificacaoTipoNacionalidade,
                Nome = model.IdentificacaoNome,
                DataNascimento = model.IdentificacaoDataNascimento,
                Cpf = model.IdentificacaoCpf,
                NumeroPassaporte = model.IdentificacaoNumeroPassaporte,
                DadosPessoaisCadastrados = dadosPessoaisCadastrados,
                TipoAtuacao = model.TipoAtuacao
            };

            return this.PessoaService.BuscarPessoaExistente(filtroPessoa).TransformList<PessoaExistenteListaViewModel>();
        }
    }
}