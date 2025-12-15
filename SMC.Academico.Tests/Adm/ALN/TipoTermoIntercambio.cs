using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using System;
using Xunit; //Usado no Fact


namespace SMC.Academico.Tests.ADM.ALN//Arvore onde esta o arquivo
{

    public class TipoTermoIntercambio : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario

        public void TipoTermoIntercambio_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Cotutela II");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                driver.FindElement(By.Name("PermiteAssociarAluno")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Tipo de termo de intercâmbio incluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }
        [Fact] //Deve ser declarada para teste unitario

        public void TipoTermoIntercambio_InclusaoDuplicada() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Cotutela II");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();
                driver.FindElement(By.Name("PermiteAssociarAluno")).Click();
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Já existe tipo de termo de intercâmbio com esta descrição."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void TipoTermoIntercambio_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("cotutela II");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Cotutela I");
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Tipo de termo de intercâmbio alterado com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

   [Fact] //Deve ser declarada para teste unitario

        public void TipoTermoIntercambio_ExclusaoNaoPermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();

                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("cotutela");
                driver.FindElement(By.Id("fieldsetPesquisaViewPadrao")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)

                Assert.True(CheckMessage("Exclusão não permitida"), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void TipoTermoIntercambio_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Cotutela I");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();


                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Tipo de termo de intercâmbio excluído com sucesso."), "Mensagem esperada não exibida");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoTermoIntercambio_Inclusao();
            TipoTermoIntercambio_InclusaoDuplicada();
            TipoTermoIntercambio_Alteracao();
            TipoTermoIntercambio_Exclusao();
            TipoTermoIntercambio_ExclusaoNaoPermitida();
        }

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
                    driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "ALN/TipoTermoIntercambio");

        }
    }
}


