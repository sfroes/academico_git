using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System;
using Xunit; //Usado no Fact
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class MotivoNumeroSerieInvalidoSerie : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {

        [Fact] //Deve ser declarada para teste unitario

        public void MotivoNumeroSerieInvalido_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Impressão errada");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                Assert.True(CheckMessage("Motivo de Número de Série Inválido incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void MotivoNumeroSerieInvalido_InclusaoDuplicadanaopermitida() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Impressão errada");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                Assert.True(CheckMessage("Já existe motivo de número de série invalido cadastrado com esta descrição para esta instituição de ensino."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario

        public void MotivoNumeroSerieInvalido_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("impressao errada");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//button[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Impressão incorreta");
                driver.FindElement(By.Id("btnSalvarVPIE")).Click();
           
                Assert.True(CheckMessage("Motivo de Número de Série Inválido alterado com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }


        [Fact] //Deve ser declarada para teste unitario

        public void MotivoNumeroSerieInvalido_Exclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("impressão incorreta");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                Assert.True(CheckMessage("Motivo de Número de Série Inválido excluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        /* Opção de menu desativada
        [Fact]
        // [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()

        {
            MotivoNumeroSerieInvalido_Inclusao();
            MotivoNumeroSerieInvalido_InclusaoDuplicadanaopermitida();
            MotivoNumeroSerieInvalido_Alteracao();
            MotivoNumeroSerieInvalido_Exclusao();


        }*/

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
                    driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/MotivoNumeroSerieInvalido");
                }
            }
}
