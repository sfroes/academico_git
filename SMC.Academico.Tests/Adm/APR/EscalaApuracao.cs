using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;

namespace SMC.Academico.Tests.ADM.APR //Arvore onde esta o arquivo
{

    public class Escala_Apuracao : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {


        [Fact] //Deve ser declarada para teste unitario
        public void Inclusao_Escala_Apuracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //COLAR AQUI O SCRIPT GRAVADO
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("DESCRICAO DE NOVA ESCALA");
                //driver.FindElement(By.XPath("(//input[@id='ApuracaoFinal'])[2]")).Click();
                driver.FindElement(By.Name("ApuracaoFinal")).Click();
                driver.FindElement(By.Name("ApuracaoAvaliacao")).Click();
                driver.FindElement(By.Id("select_TipoEscalaApuracao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoEscalaApuracao"))).SelectByText("Aprovado / Reprovado");
                driver.FindElement(By.Id("select_TipoEscalaApuracao")).Click();
                driver.FindElement(By.Name("Itens[0].Descricao")).Click();
                driver.FindElement(By.Name("Itens[0].Descricao")).Clear();
                driver.FindElement(By.Name("Itens[0].Descricao")).SendKeys("DESCRICAO DE TESTES I");
                driver.FindElement(By.Name("Itens[0].Aprovado")).Click();
                driver.FindElement(By.Name("Itens[1].Descricao")).Click();
                driver.FindElement(By.Name("Itens[1].Descricao")).Clear();
                driver.FindElement(By.Name("Itens[1].Descricao")).SendKeys("DESCRICAO DE TESTES II");
                driver.FindElement(By.Id("Itens_1__Aprovado")).Click();
             //   driver.FindElement(By.XPath("(//input[@id='Itens_1__Aprovado'])[2]")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                Assert.True(CheckMessage("Escala de apuração incluída com sucesso."), "Mensagem de sucesso não exibida");
                               
            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void Alteracao_Escala_Apuracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("DESCRIÇÃO");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("NOVA ESCALA");
                driver.FindElement(By.Name("ApuracaoAvaliacao")).Click();
                driver.FindElement(By.CssSelector("[name='ApuracaoAvaliacao'][title='Não']")).Click();
                driver.FindElement(By.Id("select_TipoEscalaApuracao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TipoEscalaApuracao"))).SelectByText("Conceito");
                           
                driver.FindElement(By.Name("Itens[0].Descricao")).Clear();
                driver.FindElement(By.Name("Itens[0].Descricao")).SendKeys("DESCRIÇÃO ITENS");
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Click();
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Click();

                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).Clear();
                driver.FindElement(By.Name("Itens[0].PercentualMinimo")).SendKeys("1");
                driver.FindElement(By.Name("Itens[0].PercentualMaximo")).Click();

                driver.FindElement(By.Name("Itens[0].PercentualMaximo")).Clear();
                driver.FindElement(By.Name("Itens[0].PercentualMaximo")).SendKeys("50");
                driver.FindElement(By.Name("Itens[1].PercentualMinimo")).Click();

                driver.FindElement(By.Name("Itens[1].PercentualMinimo")).Clear();
                driver.FindElement(By.Name("Itens[1].PercentualMinimo")).SendKeys("51");
                driver.FindElement(By.Name("Itens[1].PercentualMaximo")).Click();

                driver.FindElement(By.Name("Itens[1].PercentualMaximo")).Clear();
                driver.FindElement(By.Name("Itens[1].PercentualMaximo")).SendKeys("99");
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                Assert.True(CheckSuccess(), "Mensagem de sucesso não exibida");

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void Exclusao_Escala_Apuracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("NOVA ESCALA");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaNao")).Click();
                driver.FindElement(By.Id("DynamicBotaoExcluir1")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                Assert.True(CheckSuccess(), "Mensagem de sucesso não exibida");
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            Inclusao_Escala_Apuracao();
            Alteracao_Escala_Apuracao();
            Exclusao_Escala_Apuracao();

        }

        [Fact]
        [Trait("Ordenado", "CRUDConsole")]
        public void TesteOrdenadoCRUDConsole()
        {
            TesteOrdenadoCRUD();
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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "APR/EscalaApuracao"); //coloca o resto do endereço para acessar a pagina do teste

        }
    }
}
