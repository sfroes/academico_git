using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.ServiceContract.Areas.CSO.Data.InstituicaoTipoEntidadeHierarquiaClassificacao;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.UI.Mvc.Models;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class EntidadeController : SMCDynamicControllerBase
    {
        #region Serviços

        private IInstituicaoTipoEntidadeService InstituicaoTipoEntidadeService
        {
            get { return this.Create<IInstituicaoTipoEntidadeService>(); }
        }

        private IInstituicaoTipoEntidadeHierarquiaClassificacaoService InstituicaoTipoEntidadeHierarquiaClassificacaoService
        {
            get { return this.Create<IInstituicaoTipoEntidadeHierarquiaClassificacaoService>(); }
        }

        #endregion Serviços

        #region Constantes

        /// <summary>
        /// Passo 4 (lembrar que o índice dos passos começa no 0)
        /// </summary>
        private const short passoClassificacao = 3;

        /// <summary>
        /// Passo 5 (lembrar que o índice dos passos começa no 0)
        /// </summary>
        private const short passoConfirmarDados = 4;

        #endregion Constantes

        [SMCAuthorize(UC_ORG_001_06_02.MANTER_ENTIDADE)]
        public ActionResult Step1(EntidadeDynamicModel model)
        {
            this.ConfigureDynamic(model);

            // Busca do TempData para ver se existe algum datasource definido com este nome
            var tipoEntidade = TempData.Peek(EntidadeDynamicModel.KEY_DATASOURCE_TIPOENTIDADE);

            // Caso não tenha definido os tipos de entidade, dispara exception e redireciona para index
            if (tipoEntidade == null || (tipoEntidade as ICollection).Count == 0)
                ThrowRedirect<TipoEntidadeNaoAssociadoInstituicaoException>("index");

            // Cria as opções para view padrão
            SMCDefaultViewWizardOptions options = new SMCDefaultViewWizardOptions();
            options.Title = "Entidade";
            options.ActionSave = "Salvar";

            // Retorna o passo do wizard
            return SMCViewWizard(model, options);
        }

        [SMCAuthorize(UC_ORG_001_06_02.MANTER_ENTIDADE)]
        public ActionResult Step2(EntidadeDynamicModel model)
        {
            this.ConfigureDynamic(model);

            // Recupera o tipo de entidade selecionado
            var tipoEntidade = InstituicaoTipoEntidadeService.BuscarTipoEntidadeDaInstituicao(model.SeqTipoEntidade);
            var isNovo = false;

            // Atualiza as informações de obrigatoriedade e visibilidade da entidade
            model.NomeReduzidoObrigatorio = tipoEntidade.NomeReduzidoObrigatorio.GetValueOrDefault();
            model.NomeReduzidoVisivel = tipoEntidade.NomeReduzidoVisivel;
            model.SiglaObrigatoria = tipoEntidade.SiglaObrigatoria.GetValueOrDefault();
            model.SiglaVisivel = tipoEntidade.SiglaVisivel;
            model.UnidadeSeoVisivel = tipoEntidade.UnidadeSeoVisivel;
            model.UnidadeSeoObrigatorio = tipoEntidade.UnidadeSeoObrigatorio.GetValueOrDefault();
            model.LogotipoObrigatorio = tipoEntidade.LogotipoObrigatorio.GetValueOrDefault();
            model.LogotipoVisivel = tipoEntidade.LogotipoVisivel;
            model.NomeComplementarObrigatorio = tipoEntidade.NomeComplementarObrigatorio.GetValueOrDefault();
            model.NomeComplementarVisivel = tipoEntidade.NomeComplementarVisivel;
            model.UnidadeAgdVisivel = tipoEntidade.UnidadeAgdVisivel;
            model.UnidadeAgdObrigatorio = tipoEntidade.UnidadeAgdObrigatorio.GetValueOrDefault();
            model.UnidadeGpiVisivel = tipoEntidade.UnidadeGpiVisivel;
            model.UnidadeGpiObrigatorio = tipoEntidade.UnidadeGpiObrigatorio.GetValueOrDefault();
            model.UnidadeNotificacaoVisivel = tipoEntidade.UnidadeNotificacaoVisivel;
            model.UnidadeNotificacaoObrigatorio = tipoEntidade.UnidadeNotificacaoObrigatorio.GetValueOrDefault();
            model.UnidadeFormularioVisivel = tipoEntidade.UnidadeFormularioVisivel;
            model.UnidadeFormularioObrigatorio = tipoEntidade.UnidadeFormularioObrigatorio.GetValueOrDefault();

            // Carrega a hierarquia de entidades apenas se não estiver inicializado para não sobrescrever o estado do modelo
            if (model.Hierarquias.SMCCount() == 0)
            {
                model.Hierarquias = this.CarregarClassificacoes(tipoEntidade.Seq);
            }

            // Verifica obrigatoriedade de endereço
            if (tipoEntidade.TiposEndereco.SMCCount() > 0)
            {
                isNovo = false;
                model.HabilitaEnderecos = true;

                if (model.TiposEnderecos == null)
                    model.TiposEnderecos = new List<TipoEndereco>();

                model.TiposEnderecos.Clear();

                if (model.Enderecos == null)
                {
                    isNovo = true;
                    model.Enderecos = new AddressList();
                }

                foreach (var tipoEndereco in tipoEntidade.TiposEndereco)
                {
                    if (isNovo && tipoEndereco.Obrigatorio)
                    {
                        model.Enderecos.Add(new InformacoesEnderecoViewModel
                        {
                            TipoEndereco = tipoEndereco.TipoEndereco.SMCTo<short>()
                        });
                    }

                    model.TiposEnderecos.Add(tipoEndereco.TipoEndereco);
                }
            }
            else
            {
                model.HabilitaEnderecos = false;
            }

            // Verifica obrigatoriedade de telefone
            if (tipoEntidade.TiposTelefone.SMCCount() > 0)
            {
                isNovo = false;
                model.HabilitaTelefones = true;

                if (model.TiposTelefone == null)
                    model.TiposTelefone = new List<SMCDatasourceItem<string>>();

                model.TiposTelefone.Clear();

                if (model.Telefones == null)
                {
                    isNovo = true;
                    model.Telefones = new SMCMasterDetailList<TelefoneCategoriaViewModel>();
                }

                foreach (var tipoTelefone in tipoEntidade.TiposTelefone)
                {
                    var descricao = tipoTelefone.CategoriaTelefone.HasValue && tipoTelefone.CategoriaTelefone != Academico.Common.Enums.CategoriaTelefone.Nenhum
                                  ? $"{tipoTelefone.TipoTelefone.SMCGetDescription()} - {tipoTelefone.CategoriaTelefone.SMCGetDescription()}"
                                  : tipoTelefone.TipoTelefone.SMCGetDescription();

                    if (isNovo && tipoTelefone.Obrigatorio)
                    {
                        model.Telefones.Add(new TelefoneCategoriaViewModel
                        {
                            DescricaoTipoTelefone = descricao
                        });
                    }

                    SMCDatasourceItem<string> novoTipoTelefone = new SMCDatasourceItem<string>()
                    {
                        Descricao = descricao,
                        Seq = descricao
                    };
                    model.TiposTelefone.Add(novoTipoTelefone);
                }
                model.TiposTelefone = model.TiposTelefone.OrderBy(t => t.Descricao).ToList();
            }
            else
            {
                model.HabilitaTelefones = false;
            }

            // Verifica obrigatoriedade de endereço eletronico
            if (tipoEntidade.TiposEnderecoEletronico.SMCCount() > 0)
            {
                isNovo = false;
                model.HabilitaEnderecosEletronicos = true;

                if (model.TiposEnderecoEletronico == null)
                    model.TiposEnderecoEletronico = new List<SMCDatasourceItem<string>>();

                model.TiposEnderecoEletronico.Clear();

                if (model.EnderecosEletronicos == null)
                {
                    isNovo = true;
                    model.EnderecosEletronicos = new SMCMasterDetailList<EnderecoEletronicoCategoriaViewModel>();
                }

                foreach (var tipoEndEletronico in tipoEntidade.TiposEnderecoEletronico)
                {
                    var descricao = tipoEndEletronico.CategoriaEnderecoEletronico.HasValue && tipoEndEletronico.CategoriaEnderecoEletronico != Academico.Common.Enums.CategoriaEnderecoEletronico.Nenhum
                                  ? $"{tipoEndEletronico.TipoEnderecoEletronico.SMCGetDescription()} - {tipoEndEletronico.CategoriaEnderecoEletronico.SMCGetDescription()}"
                                  : tipoEndEletronico.TipoEnderecoEletronico.SMCGetDescription();

                    if (isNovo && tipoEndEletronico.Obrigatorio)
                    {
                        model.EnderecosEletronicos.Add(new EnderecoEletronicoCategoriaViewModel()
                        {
                            DescricaoTipoEnderecoEletronico = descricao
                        });
                    }

                    SMCDatasourceItem<string> novoTipoEndEletronico = new SMCDatasourceItem<string>()
                    {
                        Descricao = descricao,
                        Seq = descricao
                    };
                    model.TiposEnderecoEletronico.Add(novoTipoEndEletronico);
                }
                model.TiposEnderecoEletronico = model.TiposEnderecoEletronico.OrderBy(t => t.Descricao).ToList();
            }
            else
            {
                model.HabilitaEnderecosEletronicos = false;
            }

            // Cria as opções para view padrão
            SMCDefaultViewWizardOptions options = new SMCDefaultViewWizardOptions();
            options.Title = "Entidade";
            options.ActionSave = "Salvar";
            //options.Steps = this._dynamicOptions.Steps;

            // Retorna o passo do wizard
            return SMCViewWizard(model, options);
        }

        /// <summary>
        /// Passo 3 - Endereço
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORG_001_06_02.MANTER_ENTIDADE)]
        public ActionResult Step3(EntidadeDynamicModel model)
        {
            this.ConfigureDynamic(model);

            //verifica se será necessário pular este passo
            if (PularDadosContato(model, passoClassificacao))
            {
                //verifica se será necessário pular o passo seguinte a este passo
                PularDadosClassificacao(model, passoConfirmarDados);
            }

            // Retorna o passo do wizard
            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Passo 4 - classificações
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORG_001_06_02.MANTER_ENTIDADE)]
        public ActionResult Step4(EntidadeDynamicModel model)
        {
            this.ConfigureDynamic(model);

            //Verifica se será necessário pular este passo
            PularDadosClassificacao(model, passoConfirmarDados);

            // Retorna o passo do wizard
            return SMCViewWizard(model, null);
        }

        /// <summary>
        /// Passo 5 - confirmação dos dados
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORG_001_06_02.MANTER_ENTIDADE)]
        public ActionResult Step5(EntidadeDynamicModel model)
        {
            this.ConfigureDynamic(model);

            // Retorna o passo do wizard
            return SMCViewWizard(model, null);
        }

        #region steps

        /// <summary>
        /// Pula este passo caso não seja necessário informar os dados referentes ao endereçoo, enviando o usuário direto ao passo fornecido
        /// </summary>
        /// <param name="model"></param>
        /// <param name="irParaPasso">passo</param>
        /// <remarks>Setado caso pule o passo</remarks>
        public bool PularDadosContato(EntidadeDynamicModel model, int irParaPasso)
        {
            if (!model.HabilitaEnderecos && !model.HabilitaEnderecosEletronicos && !model.HabilitaTelefones)
                this.PularPasso(model, irParaPasso);
            return model.Step == irParaPasso;
        }

        /// <summary>
        /// Pula este passo caso não exista classificação, enviando o usuário direto ao passo fornecido
        /// </summary>
        /// <param name="model"></param>
        /// <param name="irParaPasso">passo</param>
        public void PularDadosClassificacao(EntidadeDynamicModel model, int irParaPasso)
        {
            if (model.Hierarquias == null || model.Hierarquias.Count == 0)
                this.PularPasso(model, irParaPasso);
        }

        /// <summary>
        /// Envia o usuário para o passo fornecido como parâmetro
        /// </summary>
        /// <param name="model"></param>
        /// <param name="irParaPasso">passo</param>
        private void PularPasso(EntidadeDynamicModel model, int irParaPasso)
        {
            model.Step = irParaPasso;
        }

        #endregion steps

        #region classificacao

        /// <summary>
        /// Busca as Classificações possíveis para a InstituicaoTipoEntidade de acordo com seu Seq, carregando a lista de classificações fornecida como parâmetro
        /// </summary>
        /// <param name="SeqInstituicaoTipoEntidade"></param>
        /// <param name="Classificacoes">Modelo a ter a lista de classificações carregada</param>
        private List<EntidadeClassificacoesViewModel> CarregarClassificacoes(long SeqInstituicaoTipoEntidade)
        {
            List<InstituicaoTipoEntidadeHierarquiaClassificacaoData> listaInstituicaoTipoEntidadeHierarquiaClassificacao = InstituicaoTipoEntidadeHierarquiaClassificacaoService.BuscarInstituicaoTipoEntidadeHierarquiasClassificacao(SeqInstituicaoTipoEntidade);

            //Retorno
            List<EntidadeClassificacoesViewModel> listaClassificacoes = new List<EntidadeClassificacoesViewModel>();

            //Carregando as possíveis classificacoes
            if (listaInstituicaoTipoEntidadeHierarquiaClassificacao != null)
                listaInstituicaoTipoEntidadeHierarquiaClassificacao.ForEach(item => listaClassificacoes.Add(new EntidadeClassificacoesViewModel()
                {
                    SeqHierarquiaClassificacao = item.SeqHierarquiaClassificacao,
                    Descricao = item.HierarquiaClassificacaoData.Descricao,
                    QuantidadeMinima = item.QuantidadeMinima,
                    QuantidadeMaxima = item.QuantidadeMaxima,
                    SeqTipoClassificacao = item.SeqTipoClassificacao
                }));

            return listaClassificacoes;
        }

        #endregion classificacao
    }
}