using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;


namespace SMC.Academico.Tests.ADM.ORG
{
    public class TipoEntidade : SMCSeleniumUnitTest
    {
        // private bool phantomJS = false;

        [Fact]
        public void TipoEntidade_1_Inserir()
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Novo tipo de entidade");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("NOVO_TIPO_ENTIDADE");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                /*//Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();*/


                //Declara a variável teste q ira receber o texto do campo
                driver.FindElement(By.XPath("//div[@id='centro']/div/div/div")).Click();
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();
                
                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Tipo de entidade incluído com sucesso.", teste);
            });
        }


        [Fact]
        public void TipoEntidade_2_Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
              //-----------------------------------------------------------------------------------------
              driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Novo tipo de entidade");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.CssSelector("[title='Alterar'][type='button']")).Click();

                //driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Novo tipo de entidade TESTE");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();
                //Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoEntidade_3_Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //----------------------------------------------
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Novo tipo de entidade TESTE");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                
                //driver.FindElement(By.CssSelector("[title='Excluir'][data-control='button']")).Click();

                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                /*//Checando mensagem de sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");*/

                /*//Declara a variável teste q ira receber o texto do campo
                string
                teste = driver.FindElement(By.XPath("//div[@id='centro']/div/div/div[2]")).GetValue();

                //Compara a mensagem exibida com a esperada:
                Assert.Equal("Tipo de entidade excluído com sucesso.", teste);*/


                //Checando a mensagem apresentada (compara a mensagem exibida com a informada)
                Assert.True(CheckMessage("Tipo de entidade excluído com sucesso."), "Mensagem esperada não exibida");

                driver.Driver.Close();
            });
        }
        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoEntidade_1_Inserir();
            TipoEntidade_2_Alterar();
            TipoEntidade_3_Excluir();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //driver.GoToUrl(Consts.SERVIDOR_DESENVOLVIMENTO_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(900));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/ORG/TipoEntidade");
            //driver.GoToUrl2(Consts.SERVIDOR_DESENVOLVIMENTO_ADM + "/ORG/TipoEntidade");
            //----------------------------------------------------
        }
       



    }
}
