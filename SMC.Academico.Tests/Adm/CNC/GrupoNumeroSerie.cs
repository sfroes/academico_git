using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test; //Framework de testes
using System;
using System.Threading;
using Xunit; //Usado no Fact
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SMC.Academico.Tests.ADM.CNC //Arvore onde esta o arquivo
{

    public class GrupoNumeroSerie : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
     {


        [Fact] //Deve ser declarada para teste unitario

        public void GrupoNumeroSerie_Inclusao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("A45");
                driver.FindElement(By.Id("divConteudoPrincipalEstrutura")).Click();
                driver.FindElement(By.Name("Lotes[0].NumeroSerieInicial")).Clear();
                driver.FindElement(By.Name("Lotes[0].NumeroSerieInicial")).SendKeys("001");
                driver.FindElement(By.XPath("//div[@id='Lotes']/div[3]")).Click();
                driver.FindElement(By.Name("Lotes[0].NumeroSerieFinal")).Clear();
                driver.FindElement(By.Name("Lotes[0].NumeroSerieFinal")).SendKeys("100");
                driver.FindElement(By.Name("Lotes[0].Observacao")).Click();
                driver.FindElement(By.Name("Lotes[0].Observacao")).Clear();
                driver.FindElement(By.Name("Lotes[0].Observacao")).SendKeys("Automação de Teste");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                Assert.True(CheckMessage("Grupo de Número de Série incluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario


        public void GrupoNumeroSerie_InclusaoDuplicadaNaopermitidaDescricao() //Colocar o nome da tela a ser testada
    {
        base.ExecuteTest((driver) => //chama o browser e coloca o link correto
        {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();
            Login(driver); // realiza o login como administrador

            //Colar o script aqui

            driver.FindElement(By.Id("BotaoNovoVP")).Click();
            driver.FindElement(By.Name("Descricao")).Clear();
            driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
            driver.FindElement(By.Name("Token")).Click();
            driver.FindElement(By.Name("Token")).Clear();
            driver.FindElement(By.Name("Token")).SendKeys("A46");
            driver.FindElement(By.Id("divConteudoPrincipalEstrutura")).Click();
            driver.FindElement(By.Name("Lotes[0].NumeroSerieInicial")).Clear();
            driver.FindElement(By.Name("Lotes[0].NumeroSerieInicial")).SendKeys("001");
            driver.FindElement(By.XPath("//div[@id='Lotes']/div[3]")).Click();
            driver.FindElement(By.Name("Lotes[0].NumeroSerieFinal")).Clear();
            driver.FindElement(By.Name("Lotes[0].NumeroSerieFinal")).SendKeys("100");
            driver.FindElement(By.Name("Lotes[0].Observacao")).Click();
            driver.FindElement(By.Name("Lotes[0].Observacao")).Clear();
            driver.FindElement(By.Name("Lotes[0].Observacao")).SendKeys("Automação de Teste");
            driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

            //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
            Assert.True(CheckMessage("Já existe grupo de número de série cadastrada com esta descrição para esta instituição de ensino."), "Mensagem esperada não exibida");
            driver.Driver.Close();
        });
    }

        [Fact] //Deve ser declarada para teste unitario


        public void GrupoNumeroSerie_InclusaoDuplicadaNaopermitidaToken() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("A45");
                driver.FindElement(By.Id("divConteudoPrincipalEstrutura")).Click();
                driver.FindElement(By.Name("Lotes[0].NumeroSerieInicial")).Clear();
                driver.FindElement(By.Name("Lotes[0].NumeroSerieInicial")).SendKeys("001");
                driver.FindElement(By.XPath("//div[@id='Lotes']/div[3]")).Click();
                driver.FindElement(By.Name("Lotes[0].NumeroSerieFinal")).Clear();
                driver.FindElement(By.Name("Lotes[0].NumeroSerieFinal")).SendKeys("100");
                driver.FindElement(By.Name("Lotes[0].Observacao")).Click();
                driver.FindElement(By.Name("Lotes[0].Observacao")).Clear();
                driver.FindElement(By.Name("Lotes[0].Observacao")).SendKeys("Automação de Teste");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando a mensagem de Sucesso (compara a mensagem que está descrita com a que vai aparecer no aplicativo)
                //Assert.True(CheckMessage("Já existe grupo de número de série cadastrada com esta descrição para esta instituição de ensino."), "Mensagem esperada não exibida");
                Assert.True(CheckMessage("Já existe grupo de número de série cadastrado com este token para esta instituição de ensino."), "Mensagem esperada não exibida");
                driver.Driver.Close();

            });
        }



        [Fact] //Deve ser declarada para teste unitario


        public void GrupoNumeroSerie_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação II");
                driver.FindElement(By.XPath("//button[@id='BotaoSalvarTemplate']/i")).Click();

                Assert.True(CheckMessage("Grupo de Número de Série alterado com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        [Fact] //Deve ser declarada para teste unitario

                public void GrupoNumeroSerie_Exclusão() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Automação");

                //driver.FindElement(By.XPath("//button[@id='BotaoPesquisarVP']/i")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();

                //driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                              
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                Assert.True(CheckMessage("Grupo de Número de Série excluído com sucesso."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });
        }

        /*Opção de menu desativada
        [Fact]
        // [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {

            GrupoNumeroSerie_Inclusao();
            GrupoNumeroSerie_InclusaoDuplicadaNaopermitidaDescricao();
            GrupoNumeroSerie_InclusaoDuplicadaNaopermitidaToken();
            GrupoNumeroSerie_Alteracao();
            GrupoNumeroSerie_Exclusão();
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
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CNC/GrupoNumeroSerie");
        }
    }
}


