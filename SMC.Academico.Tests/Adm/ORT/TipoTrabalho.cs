using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; 
using System.Threading;
using Xunit; //Usado no Fact
using System;

namespace SMC.Academico.Tests.ADM.ORT 
{

    public class TipoTrabalho : SMCSeleniumUnitTest 
    {

        [Fact]
        public void TipoTrabalho_1_Inserir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Tipo de trabalho cadastrado pela automação");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                                              
                //Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoTrabalho_2_InserirVerificaCampoObrigatorio()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando mensagem de preenchimento obrigatório
                driver.FindElement(By.Name("Descricao")).CheckErrorMessage("Preenchimento obrigatório");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoTrabalho_3_Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Tipo de trabalho cadastrado pela automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Tipo de trabalho alterado pela automação");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();
                
                //Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact]
        public void TipoTrabalho_4_ExclusaoNaoPermitida()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Dissertação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando mensagem 
                //Assert.True(CheckMessage("Exclusão não permitida. TipoTrabalho já foi associado(a) a outra entidade. FK_instituicao_nivel_tipo_divisao_componente_03"), "Era esperado sucesso e ocorreu um erro");
                Assert.True(CheckMessage("Exclusão não permitida."), "Era esperado sucesso e ocorreu um erro");
                //Assert.True(CheckMessage("TipoTrabalho já foi associado"), "Era esperado sucesso e ocorreu um erro");
                Assert.True(CheckMessage("a outra entidade. FK_instituicao_nivel_tipo_divisao_componente_03"), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact]
        public void TipoTrabalho_5_Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Tipo de trabalho alterado pela automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();        
                            
                driver.FindElement(By.Id("gridPadrao_Grid")).Click(); //Para o foco ficar na tela onde está o botão
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();

                // driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click(); //Dessa forma não localizou o botãp Sim
                driver.FindElement(By.CssSelector("[title='Sim'][type='button']")).Click();

                //Checando mensagem 
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoTrabalho_1_Inserir();
            TipoTrabalho_2_InserirVerificaCampoObrigatorio();
            TipoTrabalho_3_Alterar();
            TipoTrabalho_4_ExclusaoNaoPermitida();
            TipoTrabalho_5_Excluir();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ORT/TipoTrabalho"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}

