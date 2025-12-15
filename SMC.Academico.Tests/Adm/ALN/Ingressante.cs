using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test;
using System.Threading;
using System;
using Xunit;

namespace SMC.Academico.Tests.ADM.ALN
{
    public class Ingressante : SMCSeleniumUnitTest
    {
        // private bool phantomJS = false;

        [Fact]
        //[Trait("Ordenado", "CRUD")]
        public void Ingressante_1_InserirEscalonamentoPrazoEncerrado()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("IdentificacaoNome")).Click();
                driver.FindElement(By.Name("IdentificacaoNome")).Clear();
                //driver.FindElement(By.Name("IdentificacaoNome")).SendKeys("Sirley Freitas");
                driver.FindElement(By.Name("IdentificacaoNome")).SendKeys("Ingressante teste escalonamento");
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).Click();
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).Clear();
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).SendKeys("29/11/73");
                driver.FindElement(By.Name("IdentificacaoCpf")).Click();
                driver.FindElement(By.Name("IdentificacaoCpf")).Clear();
                //driver.FindElement(By.Name("IdentificacaoCpf")).SendKeys("857.029.196-53");
                //driver.FindElement(By.Name("IdentificacaoCpf")).SendKeys("053.631.730-50")
                driver.FindElement(By.Name("IdentificacaoCpf")).SendKeys("673.820.690-95");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Id("CadastrarNovaPessoa")).Click();
                driver.FindElement(By.Id("CadastrarNovaPessoa")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Id("select_Sexo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Sexo"))).SelectByText("Feminino");
                driver.FindElement(By.Id("select_Sexo")).Click();
                driver.FindElement(By.Id("select_RacaCor")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_RacaCor"))).SelectByText("Parda");
                driver.FindElement(By.Id("select_RacaCor")).Click();
                driver.FindElement(By.Id("Filiacao_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco"))).SelectByText("Mãe");
                driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco")).Click();
                driver.FindElement(By.Name("Filiacao[0].Nome")).Click();
                driver.FindElement(By.Name("Filiacao[0].Nome")).Clear();
                driver.FindElement(By.Name("Filiacao[0].Nome")).SendKeys("Gloria Braga da Cruz");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                
                //Cria endereço
                driver.FindElement(By.Id("Enderecos_botao_modal")).Click();
                driver.FindElement(By.Id("NovoEndereco")).Click();
                driver.FindElement(By.Id("select_Endereco_0__TipoEndereco")).Click();
                new SelectElement(driver.Driver.FindElement(By.Id("select_Endereco_0__TipoEndereco"))).SelectByText("Residencial");
                driver.FindElement(By.Id("select_Endereco_0__TipoEndereco")).Click();
                driver.FindElement(By.Id("select_Endereco_0__SeqPais")).Click();
                driver.FindElement(By.Id("select_Endereco_0__SeqPais")).Click();
                driver.FindElement(By.Name("Endereco[0].Cep")).Click();
                driver.FindElement(By.Name("Endereco[0].Cep")).Clear();
                driver.FindElement(By.Name("Endereco[0].Cep")).SendKeys("30642-190");
                driver.FindElement(By.Name("Endereco[0].Numero")).Click();
                driver.FindElement(By.Name("Endereco[0].Numero")).Click();
                driver.FindElement(By.Name("Endereco[0].Numero")).Clear();
                driver.FindElement(By.Name("Endereco[0].Numero")).SendKeys("60");
                driver.FindElement(By.Name("Endereco[0].Complemento")).Click();
                driver.FindElement(By.Name("Endereco[0].Complemento")).Clear();
                driver.FindElement(By.Name("Endereco[0].Complemento")).SendKeys("Bl.1 Ap.303");
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                                             
                driver.FindElement(By.Id("Enderecos_botao_modal")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-Enderecos")).Click();
                driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia"))).SelectByText("Acadêmica e Financeira");
                driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia")).Click();

                //Criar endereço eletrônico
                driver.FindElement(By.Id("EnderecosEletronicos_botao_modal")).Click();
                driver.FindElement(By.Id("NovoEnderecoEletronico")).Click();
                driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico")).Click();
                new SelectElement(driver.Driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico"))).SelectByText("E-mail");
                driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico")).Click();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).Click();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).Clear();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).SendKeys("testesirleycf@hotmail.com");
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-EnderecosEletronicos")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Preencher Dados Acadêmicos
                driver.FindElement(By.XPath("//button[@id='Campanha_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Campanha do PPG em Administração - 1/2020");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-Campanha")).Click();

                driver.FindElement(By.Name("Campanha_display_text")).Click();
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SelectElement(driver.Driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Doutorado Acadêmico");
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();

                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Regime Disciplina Isolada");

                driver.FindElement(By.Name("Ofertas_0__SeqCampanhaOferta_display_text")).Click();
                driver.FindElement(By.Name("Ofertas_0__SeqCampanhaOferta_display_text")).Clear();

                driver.FindElement(By.XPath("//button[@id='Ofertas_0__SeqCampanhaOferta_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("redes, cooperação e competitividade");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCampanhaOferta")).Click();
                


                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                                                            
                //Grupo de Escalonamento
                driver.FindElement(By.XPath("//button[@id='SeqGrupoEscalonamento_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.XPath("//button[@id='SeqGrupoEscalonamento_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Processo seletivo de disciplina isolada (1º grupo)");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqGrupoEscalonamento")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                
                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Não é possível associar um grupo de escalonamento que possui pelo menos um escalonamento com data fim expirada."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact]
        //[Trait("Ordenado", "CRUD")]
        public void Ingressante_1_InserirIngressante_comIntercambio_IngressoDireito()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
               //Depende do script de inclusão de termo intercâmbio - Chamada do script para inclusão de termo intercâmbio que por sua vez chama o de parceria de intercâmbio
                 var Termo = new TermoIntercambio();
                 Termo.InserirTermo();
                

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("IdentificacaoNome")).Click();
                driver.FindElement(By.Name("IdentificacaoNome")).Clear();
                driver.FindElement(By.Name("IdentificacaoNome")).SendKeys("Ingressante teste com intercâmbio e ingresso direto");
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).Click();
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).Clear();
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).SendKeys("29/11/73");
                driver.FindElement(By.Name("IdentificacaoCpf")).Click();
                driver.FindElement(By.Name("IdentificacaoCpf")).Clear();
                driver.FindElement(By.Name("IdentificacaoCpf")).SendKeys("857.029.196-53");
                             


                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("CadastrarNovaPessoa")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                driver.FindElement(By.Id("select_Sexo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Sexo"))).SelectByText("Feminino");
                driver.FindElement(By.Id("select_Sexo")).Click();
                driver.FindElement(By.Id("select_RacaCor")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_RacaCor"))).SelectByText("Parda");
                driver.FindElement(By.Id("select_RacaCor")).Click();
                driver.FindElement(By.Id("Filiacao_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco"))).SelectByText("Mãe");
                driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco")).Click();
                driver.FindElement(By.Name("Filiacao[0].Nome")).Click();
                driver.FindElement(By.Name("Filiacao[0].Nome")).Clear();
                driver.FindElement(By.Name("Filiacao[0].Nome")).SendKeys("Gloria Braga da Cruz");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Cria endereço
                driver.FindElement(By.Id("Enderecos_botao_modal")).Click();
                driver.FindElement(By.Id("NovoEndereco")).Click();
                driver.FindElement(By.Id("select_Endereco_0__TipoEndereco")).Click();
                new SelectElement(driver.Driver.FindElement(By.Id("select_Endereco_0__TipoEndereco"))).SelectByText("Residencial");
                driver.FindElement(By.Id("select_Endereco_0__TipoEndereco")).Click();
                driver.FindElement(By.Id("select_Endereco_0__SeqPais")).Click();
                driver.FindElement(By.Id("select_Endereco_0__SeqPais")).Click();
                driver.FindElement(By.Name("Endereco[0].Cep")).Click();
                driver.FindElement(By.Name("Endereco[0].Cep")).Clear();
                driver.FindElement(By.Name("Endereco[0].Cep")).SendKeys("30642-190");
                driver.FindElement(By.Name("Endereco[0].Numero")).Click();
                driver.FindElement(By.Name("Endereco[0].Numero")).Click();
                driver.FindElement(By.Name("Endereco[0].Numero")).Clear();
                driver.FindElement(By.Name("Endereco[0].Numero")).SendKeys("60");
                driver.FindElement(By.Name("Endereco[0].Complemento")).Click();
                driver.FindElement(By.Name("Endereco[0].Complemento")).Clear();
                driver.FindElement(By.Name("Endereco[0].Complemento")).SendKeys("Bl.1 Ap.303");
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();

                driver.FindElement(By.Id("Enderecos_botao_modal")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-Enderecos")).Click();
                driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia"))).SelectByText("Acadêmica e Financeira");
                driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia")).Click();

                //Criar endereço eletrônico
                driver.FindElement(By.Id("EnderecosEletronicos_botao_modal")).Click();
                driver.FindElement(By.Id("NovoEnderecoEletronico")).Click();
                driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico")).Click();
                new SelectElement(driver.Driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico"))).SelectByText("E-mail");
                driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico")).Click();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).Click();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).Clear();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).SendKeys("testesirleycf@hotmail.com");
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-EnderecosEletronicos")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Preencher Dados Acadêmicos
                driver.FindElement(By.XPath("//button[@id='Campanha_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                //driver.FindElement(By.Name("Descricao")).SendKeys("Campanha do ppg em direito - 1/2020");
                driver.FindElement(By.Name("Descricao")).SendKeys("Campanha do ppg em Relações internacionais - 1/2023");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-Campanha")).Click();

                driver.FindElement(By.Id("select_SeqProcessoSeletivo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqProcessoSeletivo"))).SelectByText("Processo de Ingresso direto");
                driver.FindElement(By.Id("select_SeqProcessoSeletivo")).Click();

                
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Intercâmbio");

                //Selecionando a oferta
                /*driver.FindElement(By.XPath("//button[@id='Ofertas_0__SeqCampanhaOferta_botao_modal']/i")).Click();
          
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();

                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCampanhaOferta")).Click();*/

                driver.FindElement(By.XPath("//button[@id='Ofertas_0__SeqCampanhaOferta_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("doutorado em relações internacionais");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCampanhaOferta")).Click();


                //Preenchendo dados do intercâmbio
                driver.FindElement(By.Id("TermosIntercambioDetailPartialInserir")).Click();
                driver.FindElement(By.Id("SeqTermoIntercambio_botao_modal")).Click();
                driver.FindElement(By.Name("DescricaoParceria")).Click();
                driver.FindElement(By.Name("DescricaoParceria")).Clear();
                driver.FindElement(By.Name("DescricaoParceria")).SendKeys("teste qa");
                
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Name("SelectedValues")).Click();
                Thread.Sleep(1600);
                driver.FindElement(By.Id("smc-dataselector-SeqTermoIntercambio")).Click();
                                
                //Tipo de orientação
                driver.FindElement(By.Id("select_SeqTipoOrientacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoOrientacao"))).SelectByText("Orientação em Intercâmbio");
                driver.FindElement(By.Id("select_SeqTipoOrientacao")).Click();
               // driver.FindElement(By.XPath("//button[@id='TermosIntercambio_MasterDetailBotaoEditModal2']/i")).Click();
                driver.FindElement(By.Id("select_SeqTipoOrientacao")).Click();

                // driver.FindElement(By.Id("TermosIntercambio_DetailFormBotaoFechar")).Click();

                //Orientador
                Thread.Sleep(4000);
                driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores_DetailBotaoInserirElemento")).Click();
                /*driver.FindElement(By.XPath("//div[@id='OrientacaoParticipacoesColaboradores_0__SeqColaborador']/div/a")).Click();
                /*driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores___:index____SeqColaboradorSearch")).Click();
                driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores___:index____SeqColaboradorSearch")).Clear();
                driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores___:index____SeqColaboradorSearch")).SendKeys("Cristiano Garcia Mendes");
                Thread.Sleep(4000);
                driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores___:index____SeqColaboradorSearch")).SendKeys("Cristiano Garcia Mendes");
                Thread.Sleep(4000);
                //driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores___:index____SeqColaboradorSearch")).SendKeys("Adriano Stanley");
                driver.FindElement(By.XPath("//div[@id='OrientacaoParticipacoesColaboradores_0__SeqColaborador']/div[2]/ul/li[2]/span")).Click();*/

                //Incluindo orientador
                driver.FindElement(By.XPath("//div[@id='OrientacaoParticipacoesColaboradores_0__SeqColaborador']/div/a")).Click();
                driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores___:index____SeqColaboradorSearch")).Click();
                driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores___:index____SeqColaboradorSearch")).Clear();
                driver.FindElement(By.Id("OrientacaoParticipacoesColaboradores___:index____SeqColaboradorSearch")).SendKeys("cristiano garcia mendes");
                driver.FindElement(By.XPath("//div[@id='OrientacaoParticipacoesColaboradores_0__SeqColaborador']/div[2]/ul/li[5]")).Click();

                Thread.Sleep(2000);

                driver.FindElement(By.Id("TermosIntercambio_DetailFormBotaoIncluir")).Click();
                
                Thread.Sleep(4000);
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                                    

                //Grupo de Escalonamento
                /* //driver.FindElement(By.XPath("//button[@id='SeqGrupoEscalonamento_botao_modal']/i")).Click();
                 driver.FindElement(By.Id("SeqGrupoEscalonamento_botao_modal")).Click();
                 driver.FindElement(By.Id("Descricao")).Click();
                 driver.FindElement(By.Id("SeqGrupoEscalonamento_botao_modal")).Click();
                 driver.FindElement(By.Id("Descricao")).Clear();
                 driver.FindElement(By.Id("Descricao")).SendKeys("Processo de ingresso direto");
                 driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                 driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                 driver.FindElement(By.Id("smc-dataselector-SeqGrupoEscalonamento")).Click();*/

                //driver.FindElement(By.XPath("//button[@id='SeqGrupoEscalonamento_botao_modal']/i")).Click();
                driver.FindElement(By.Id("SeqGrupoEscalonamento_botao_modal")).Click();

                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqGrupoEscalonamento")).Click();


                 /*driver.FindElement(By.XPath("//button[@id='SeqGrupoEscalonamento_botao_modal']/i")).Click();
                 driver.FindElement(By.Name("Descricao")).Click();
                 driver.FindElement(By.Name("Descricao")).Clear();
                 driver.FindElement(By.XPath("//button[@id='SeqGrupoEscalonamento_botao_modal']/i")).Click();
                 driver.FindElement(By.Name("Descricao")).Click();
                 driver.FindElement(By.Name("Descricao")).Clear();
                 driver.FindElement(By.Name("Descricao")).SendKeys("Processo de ingresso direto");
                 driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                 driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                 driver.FindElement(By.Id("smc-dataselector-SeqGrupoEscalonamento")).Click();*/
                 driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                   //Checando a mensagem de Sucesso
                   Assert.True(CheckMessage("Não é possível associar um grupo de escalonamento que possui pelo menos um escalonamento com data fim expirada."), "Mensagem esperada não exibida");

                   //Excluir o Termo de intercâmbio no teste desse ingressante e a parceria
                   var TermoIntercambioExcluir = new TermoIntercambio();
                   TermoIntercambioExcluir.ExcluirIntercambio();

                   //Depende do script de alteração e exclusão de parceria de intercâmbio para excluir da base
                   var ParceriaIntercambioAlterar = new ParceriaIntercambio();
                   ParceriaIntercambioAlterar.ParceriaIntercambio_2_Alterar();

                   var ParceriaIntercambioExcluir = new ParceriaIntercambio();
                   ParceriaIntercambioExcluir.ParceriaIntercambio_3_Excluir();

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();


            });
        }

        [Fact]
        //[Trait("Ordenado", "CRUD")]
        public void Ingressante_2_InserirValidandoMensagensObrigatoriedades()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //Validação de seleção de uma pessoa para vincular ao novo ingressante
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("IdentificacaoNome")).Click();
                driver.FindElement(By.Name("IdentificacaoNome")).Clear();
                driver.FindElement(By.Name("IdentificacaoNome")).SendKeys("Sirley Freitas");
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).Click();
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).Clear();
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).SendKeys("29/11/73");
                driver.FindElement(By.Name("IdentificacaoCpf")).Click();
                driver.FindElement(By.Name("IdentificacaoCpf")).Clear();
                driver.FindElement(By.Name("IdentificacaoCpf")).SendKeys("568.048.170-79");
                
                //driver.FindElement(By.Name("IdentificacaoCpf")).SendKeys("857.029.196-53");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                //var msgerro = driver.FindElement(CssSelector("div[class='smc-modal-contente']")).GetValue();
               // Assert.True(CheckMessage("É necessário selecionar uma pessoa ou a opção cadastrar nova."), "Mensagem esperada não exibida");
                
                //Validação de, pelo menos, 1 registro de filiação
                driver.FindElement(By.Id("CadastrarNovaPessoa")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Id("select_Sexo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Sexo"))).SelectByText("Feminino");
                driver.FindElement(By.Id("select_Sexo")).Click();
                driver.FindElement(By.Id("select_RacaCor")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_RacaCor"))).SelectByText("Parda");
                driver.FindElement(By.Id("select_RacaCor")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                Thread.Sleep(9000);
                Assert.True(CheckMessage("É necessário cadastrar ao menos 1 registro(s) de filiação."), "Mensagem esperada não exibida");

                //Validação de mais de 2 registros de filiação
                driver.FindElement(By.Id("Filiacao_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco"))).SelectByText("Mãe");
                driver.FindElement(By.Id("select_Filiacao_0__TipoParentesco")).Click();
                driver.FindElement(By.Name("Filiacao[0].Nome")).Click();
                driver.FindElement(By.Name("Filiacao[0].Nome")).Clear();
                driver.FindElement(By.Name("Filiacao[0].Nome")).SendKeys("Gloria Braga da Cruz");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("Filiacao_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("select_Filiacao_1__TipoParentesco")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Filiacao_1__TipoParentesco"))).SelectByText("Pai");
                driver.FindElement(By.Id("select_Filiacao_1__TipoParentesco")).Click();
                driver.FindElement(By.Name("Filiacao[1].Nome")).Click();
                driver.FindElement(By.Name("Filiacao[1].Nome")).Clear();
                driver.FindElement(By.Name("Filiacao[1].Nome")).SendKeys("José Vicente da Cruz");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("Filiacao_DetailBotaoInserirElemento")).Click();
                driver.FindElement(By.Id("select_Filiacao_2__TipoParentesco")).Click();
                new SMCSelectElement(driver. FindElement(By.Id("select_Filiacao_2__TipoParentesco"))).SelectByText("Mãe");
                driver.FindElement(By.Id("select_Filiacao_2__TipoParentesco")).Click();
                driver.FindElement(By.Name("Filiacao[2].Nome")).Clear();
                driver.FindElement(By.Name("Filiacao[2].Nome")).SendKeys("Glória Cruz");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                Assert.True(CheckMessage("Não é possível cadastrar mais que 2 registro(s) de filiação."), "Mensagem esperada não exibida");

                driver.FindElement(By.Id("Filiacao_DetailBotaoExcluirElemento")).Click();
                
                //Validação de não preenchimento de endereço e email
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();
                Assert.True(CheckMessage("Não é possível prosseguir. É obrigatório informar apenas um endereço com a correspondência \"Acadêmica e Financeira\" ou um endereço com a correspondência \"Acadêmica\" e outro com \"Financeira\"."), "Mensagem esperada não exibida");
                            

                driver.FindElement(By.Id("Enderecos_botao_modal")).Click();
                driver.FindElement(By.Id("NovoEndereco")).Click();
                driver.FindElement(By.Id("select_Endereco_0__TipoEndereco")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_Endereco_0__TipoEndereco"))).SelectByText("Residencial");
                driver.FindElement(By.Id("select_Endereco_0__TipoEndereco")).Click();
                driver.FindElement(By.Id("select_Endereco_0__SeqPais")).Click();
                driver.FindElement(By.Id("select_Endereco_0__SeqPais")).Click();
                driver.FindElement(By.Name("Endereco[0].Cep")).Click();
                driver.FindElement(By.Name("Endereco[0].Cep")).Clear();
                driver.FindElement(By.Name("Endereco[0].Cep")).SendKeys("30642-190");
                driver.FindElement(By.Name("Endereco[0].Numero")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Name("Endereco[0].Numero")).Clear();
                driver.FindElement(By.Name("Endereco[0].Numero")).SendKeys("60");
                driver.FindElement(By.Name("Endereco[0].Complemento")).Click();
                driver.FindElement(By.Name("Endereco[0].Complemento")).Clear();
                driver.FindElement(By.Name("Endereco[0].Complemento")).SendKeys("Bl.1 Ap.303");
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-Enderecos")).Click();
                driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia"))).SelectByText("Acadêmica e Financeira");
                driver.FindElement(By.Id("Enderecos0_EnderecoCorrespondencia")).Click();

                driver.FindElement(By.Id("EnderecosEletronicos_botao_modal")).Click();
                driver.FindElement(By.Id("NovoEnderecoEletronico")).Click();
                driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico"))).SelectByText("E-mail");
                driver.FindElement(By.Id("select_EnderecoEletronico_TipoEnderecoEletronico")).Click();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).Click();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).Clear();
                driver.FindElement(By.Name("EnderecoEletronico.Descricao")).SendKeys("testesirleycf@hotmail.com");
                driver.FindElement(By.CssSelector("[title='Salvar'][type='submit']")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-EnderecosEletronicos")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Preencher Dados Acadêmicos
                 driver.FindElement(By.XPath("//button[@id='Campanha_botao_modal']/i")).Click();
                 driver.FindElement(By.Name("Descricao")).Click();
                 driver.FindElement(By.Name("Descricao")).Clear();
                 driver.FindElement(By.Name("Descricao")).SendKeys("Campanha do PPG em Administração - 1/2020");
                 driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                 driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                 driver.FindElement(By.Id("smc-dataselector-Campanha")).Click();

                 driver.FindElement(By.Name("Campanha_display_text")).Click();
                 /*driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                 new SelectElement(driver.Driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Doutorado Acadêmico");
                 driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                 driver.FindElement(By.Name("Ofertas_0__SeqCampanhaOferta_display_text")).Click();
                 driver.FindElement(By.Name("Ofertas_0__SeqCampanhaOferta_display_text")).Clear();

                 /*driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                 new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Curso Regular");
                 driver.FindElement(By.Id("select_SeqFormaIngresso")).Click();
                 new SMCSelectElement(driver.FindElement(By.Id("select_SeqFormaIngresso"))).SelectByText("Vestibular");*/
                /*driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Clear();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Disciplina Isolada");
                /*driver.FindElement(By.Id("select_SeqFormaIngresso")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqFormaIngresso"))).SelectByText("VESTIBULAR");*/


               /* driver.FindElement(By.Name("Campanha_display_text")).Click();
                driver.FindElement(By.Id("ui-id-8")).Click();*/
                driver.FindElement(By.Id("select_SeqNivelEnsino")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqNivelEnsino"))).SelectByText("Doutorado Acadêmico");
                driver.FindElement(By.Id("select_SeqTipoVinculoAluno")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqTipoVinculoAluno"))).SelectByText("Regime Disciplina Isolada");

                driver.FindElement(By.XPath("//button[@id='Ofertas_0__SeqCampanhaOferta_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("redes, cooperação e competitividade");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqCampanhaOferta")).Click();



                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Grupo de Escalonamento
                driver.FindElement(By.XPath("//button[@id='SeqGrupoEscalonamento_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.XPath("//button[@id='SeqGrupoEscalonamento_botao_modal']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Processo seletivo de disciplina isolada (1º grupo)");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqGrupoEscalonamento")).Click();
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Não é possível associar um grupo de escalonamento que possui pelo menos um escalonamento com data fim expirada."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }



        [Fact]
        //[Trait("Ordenado", "CRUD")]
        public void Ingressante_3_InserirCpfInvalido()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //-----------------------------------------------------------------------------------------
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("IdentificacaoNome")).Click();
                driver.FindElement(By.Name("IdentificacaoNome")).Clear();
                driver.FindElement(By.Name("IdentificacaoNome")).SendKeys("Sirley Freitas");
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).Click();
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).Clear();
                driver.FindElement(By.Name("IdentificacaoDataNascimento")).SendKeys("29/11/73");
                driver.FindElement(By.Name("IdentificacaoCpf")).Click();
                driver.FindElement(By.Name("IdentificacaoCpf")).Clear();
                driver.FindElement(By.Name("IdentificacaoCpf")).SendKeys("887.029.196-53");
                driver.FindElement(By.Id("editorWizard-btnProximo")).Click();

                //Checando a mensagem de Sucesso
                  //Declara a variável teste q ira receber o texto do campo
                  string teste = driver.FindElement(By.CssSelector("span[id='parsley-id-17']")).GetValue();

                  //Compara a mensagem exibida com a esperada:
                  Assert.Equal("CPF inválido", teste);

                 //Para fechar o Chrome em segundo plano
                 driver.Driver.Close();

            });
        }



        [Fact]
        //[Trait("Ordenado", "CRUD")]
        public void Ingressante_4_Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //-----------------------------------------------------------------------------------------
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Alterar'])[1]/i[1]")).Click();
                driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Alteração não permitida. "),"Mensagem 1 esperada não exibida");
                Assert.True(CheckMessage("A situação do ingressante não permite alterar os dados."), "Mensagem 2 esperada não exibida");
                Assert.True(CheckMessage("Deseja visualizar os dados?"), "Mensagem 3 esperada não exibida");


                //Para clicar no botão Sim, foram necessárias as duas linhas abaixo
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                driver.FindElement(By.Name("Seq")).Click();
                              
                driver.FindElement(By.Id("Nome")).Click();

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact]
        public void Ingressante_5_Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //----------------------------------------------



                //Checando a mensagem de Sucesso
                //Assert.True(CheckMessage(""), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
       // [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            Ingressante_1_InserirEscalonamentoPrazoEncerrado();
            Ingressante_1_InserirIngressante_comIntercambio_IngressoDireito();
            Ingressante_2_InserirValidandoMensagensObrigatoriedades();
            Ingressante_3_InserirCpfInvalido();
            Ingressante_4_Alterar();                     
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("Login")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/ALN/Ingressante");

        }


    }
}
