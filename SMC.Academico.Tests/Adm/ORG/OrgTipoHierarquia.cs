using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;


namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{

    public class OrgTipoHierarquia : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void OrgTipoHierarquia_Inclusao() //Colocar o nome da tela a ser testada
        {
          
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Id("select_TipoVisao")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Id("select_TipoVisao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoVisao"))).SelectByText("Visão de localidades");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso (Verifica se tem a palavra sucesso na mensagem)
             
                Assert.True(CheckMessage("Tipo de hierarquia de entidade incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
               


            });

        }

        [Fact]

        [Trait("Ordenado", "CRUD")]

        public void TesteOrdenadoCRUD()
        {
            OrgTipoHierarquia_Inclusao();
         

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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ORG/TipoHierarquiaEntidade"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}



