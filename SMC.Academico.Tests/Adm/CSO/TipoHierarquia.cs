using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact


namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{
    public class TipoHierarquia : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {
        [Fact] //Deve ser declarada para teste unitario
        public void TipoHierarquia_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Hierarquia CAPES Automação");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("CAP");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoHierarquia_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Hierarquia CAPES Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                //driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                //driver.FindElement(By.Id("DynamicBotaoEdit3")).Click();
                driver.FindElement(By.CssSelector("[title='Alterar'][type='button']")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("B31");
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();
                //Assert.True(CheckMessage("Tipo de Oferta de curso alterado com sucesso."), "Era esperado sucesso e ocorreu um erro");
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoHierarquia_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Hierarquia CAPES Automação");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("CAP");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                
                Assert.True(CheckMessage("Já existe tipo de hierarquia de classificação cadastrada com esta descrição."), "Era esperado sucesso e ocorreu um erro");
                
                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }
               
        [Fact] //Deve ser declarada para teste unitario
        public void TipoHierarquia_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Hierarquia CAPES Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                //Assert.True(CheckMessage("Tipo de hierarquia de classificacao excluído com sucesso."), "Era esperado sucesso e ocorreu um erro");
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        [Fact]
        [Trait("Ordenado", "CRUD")]

        public void TesteOrdenadoCRUD()
        {
            TipoHierarquia_Inclusao();
            TipoHierarquia_Alteracao();
            TipoHierarquia_InclusaoDuplicada();
            TipoHierarquia_Exclusao();
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
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CSO/TipoHierarquiaClassificacao");

        }
    }
}

