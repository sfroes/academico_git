using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;

namespace SMC.Academico.Tests.ADM.DCT //Arvore onde esta o arquivo
{
    public class Periodico : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {
        [Fact] //Deve ser declarada para teste unitario
        public void Periodico_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui


                driver.FindElement(By.CssSelector("[title='Novo'][type='button']")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Name("QualisPeriodico[0].DescricaoAreaAvaliacao")).Click();
                driver.FindElement(By.Name("QualisPeriodico[0].DescricaoAreaAvaliacao")).Clear();
                driver.FindElement(By.Name("QualisPeriodico[0].DescricaoAreaAvaliacao")).SendKeys("Automação área I");
                driver.FindElement(By.Name("QualisPeriodico[0].CodigoISSN")).Click();
                driver.FindElement(By.Name("QualisPeriodico[0].CodigoISSN")).Clear();
                driver.FindElement(By.Name("QualisPeriodico[0].CodigoISSN")).SendKeys("123");
                driver.FindElement(By.Id("select_QualisPeriodico_0__QualisCapes")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_QualisPeriodico_0__QualisCapes"))).SelectByText("A1");
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();                               
                Assert.True(CheckMessage("Periódico CAPES incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });

        }

        [Fact] //Deve ser declarada para teste unitario
        public void Periodico_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver); // realiza o login como administrador

                //Colar o script aqui


                driver.FindElement(By.CssSelector("[title='Novo'][type='button']")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Name("QualisPeriodico[0].DescricaoAreaAvaliacao")).Click();
                driver.FindElement(By.Name("QualisPeriodico[0].DescricaoAreaAvaliacao")).Clear();
                driver.FindElement(By.Name("QualisPeriodico[0].DescricaoAreaAvaliacao")).SendKeys("Automação área I");
                driver.FindElement(By.Name("QualisPeriodico[0].CodigoISSN")).Click();
                driver.FindElement(By.Name("QualisPeriodico[0].CodigoISSN")).Clear();
                driver.FindElement(By.Name("QualisPeriodico[0].CodigoISSN")).SendKeys("123");
                driver.FindElement(By.Id("select_QualisPeriodico_0__QualisCapes")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_QualisPeriodico_0__QualisCapes"))).SelectByText("A1");
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();
                Assert.True(CheckMessage("Operação não permitida. Já existe um períódico com esta descrição."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });

        }
        /*
        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            Periodico_Inclusao();
            Periodico_InclusaoDuplicada();
            
        }
        */
        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM);
            //força o sistema a esperar 15 minutos ou até que apareça o campo para login
            WebDriverWait wait = new WebDriverWait((OpenQA.Selenium.IWebDriver)driver, TimeSpan.FromSeconds(90));
            wait.Until(e => e.FindElement(By.Name("LoginCpf")));
            driver.SMCLoginCpf();
            //força o sistema a esperar 15 minutos ou até que apareça a home do SGA
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("//div[@id='divConteudoPrincipal']/section/h2"), "SGA.Administrativo"));
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CUR/Periodico");
        }

    }
}

