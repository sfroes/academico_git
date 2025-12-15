using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;

namespace SMC.Academico.Tests.ADM.CSO
{
    public class TipoClassificacao : SMCSeleniumUnitTest
    {
        // private bool phantomJS = false;

        [Fact]

        public void TipoClassificacao_1_Inserir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------

                driver.FindElement(By.Id("BotaoNovoModalVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("TESTE");
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Salvar'])[1]/i[1]")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                
                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        [Fact]
        public void TipoClassificacao_2_Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
              //-----------------------------------------------------------------------------------------

              driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Configurar'])[5]/i[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Novo item folha'])[5]/following::a[1]")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste1");
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Salvar'])[1]/i[1]")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                
                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoClassificacao_3_Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //----------------------------------------------

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Configurar'])[5]/i[1]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Alterar'])[6]/following::a[1]")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                
                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoClassificacao_1_Inserir();
            TipoClassificacao_2_Alterar();
            TipoClassificacao_3_Excluir();
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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/CSO/TipoClassificacao?SeqTipoHierarquiaClassificacao=54B44C94F46C1EC7");
            //----------------------------------------------------
        }


    }
}
