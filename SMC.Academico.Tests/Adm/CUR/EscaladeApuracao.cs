using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;



namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{

    public class EscaladeApuracao : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void EscaladeApuracao_Inclusao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Conceito A/B e C - Automação");
                driver.FindElement(By.Id("select_TipoEscalaApuracao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoEscalaApuracao"))).SelectByText("Aprovado / Reprovado");
                driver.FindElement(By.Name("Itens[0].Descricao")).Click();
                driver.FindElement(By.Name("Itens[0].Descricao")).Clear();
                driver.FindElement(By.Name("Itens[0].Descricao")).SendKeys("AUTOMAÇÃO");
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Click();
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Click();
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Click();
                driver.FindElement(By.Name("Itens[1].Descricao")).Click();
                driver.FindElement(By.Name("Itens[1].Descricao")).Clear();
                driver.FindElement(By.Name("Itens[1].Descricao")).SendKeys("AUTOMAÇÃO II");
                driver.FindElement(By.Name("ApuracaoFinal")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();


                //Checando se apareceu um sucesso

                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");

                //Checando se apareceu um erro
                //Assert.False(CheckSuccess(), "Era esperado ERRO e ocorreu um SUCESSO");

                driver.Driver.Close();
            });

            // }
        }

        [Fact] //Deve ser declarada para teste unitario
        public void EscaladeApuracao_InclusaoDuplicada() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Conceito A/B e C - Automação");
                driver.FindElement(By.Id("select_TipoEscalaApuracao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoEscalaApuracao"))).SelectByText("Aprovado / Reprovado");
                driver.FindElement(By.Name("Itens[0].Descricao")).Click();
                driver.FindElement(By.Name("Itens[0].Descricao")).Clear();
                driver.FindElement(By.Name("Itens[0].Descricao")).SendKeys("AUTOMAÇÃO");
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Click();
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Click();
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Click();
                driver.FindElement(By.Name("Itens[1].Descricao")).Click();
                driver.FindElement(By.Name("Itens[1].Descricao")).Clear();
                driver.FindElement(By.Name("Itens[1].Descricao")).SendKeys("AUTOMAÇÃO II");
                driver.FindElement(By.Name("ApuracaoFinal")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();


                //Checando se apareceu um sucesso

                Assert.True(CheckMessage("Já existe escala de apuração cadastrada com esta descrição."), "Mensagem esperada não exibida");
                driver.Driver.Close();
            });

            // }
        }


        [Fact] //Deve ser declarada para teste unitario
        public void EscaladeApuracao_Alteracao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Conceito A/B e C - Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoEdit0']/i")).Click();
                driver.FindElement(By.Name("Itens[0].Descricao")).Click();
                driver.FindElement(By.Name("Itens[0].Descricao")).Clear();
                driver.FindElement(By.Name("Itens[0].Descricao")).SendKeys("AUTOMAÇÃO");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();

                //Checando se apareceu um sucesso

                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();
            });

            // }
        }

        [Fact] //Deve ser declarada para teste unitario
        public void EscaladeApuracao_Exclusao() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO
                                
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Conceito A/B e C - Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();


                //Checando se apareceu um sucesso

                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();
            });

            // }
        }


        [Fact] //Deve ser declarada para teste unitario
        public void EscaladeApuracao_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            // try
            // {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO

                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Aprovado/reprovado");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();

                //Checando se apareceu um sucesso

                Assert.True(CheckMessage("Exclusão não permitida. Escala de apuração já possui registros de apuração de nota/conceito."), "Mensagem esperada não exibida");
                driver.Driver.Close();
                
            });

            // }
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            EscaladeApuracao_Inclusao();
            EscaladeApuracao_InclusaoDuplicada();
            EscaladeApuracao_Alteracao();
            EscaladeApuracao_Exclusao();
            EscaladeApuracao_Exclusaonaopermitida();

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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "Apr/EscalaApuracao"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}
