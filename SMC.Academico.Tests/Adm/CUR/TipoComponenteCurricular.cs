using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test; //Framework de testes
using System.Threading;
using Xunit; //Usado no Fact
using System;

namespace SMC.Academico.Tests.ADM.CUR
{
    public class TipoComponenteCurricular : SMCSeleniumUnitTest
    {
        // private bool phantomJS = false;

        [Fact]
        public void TipoComponenteCurricular_1_Inserir()
        {
            base.ExecuteTest((driver) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                Login(driver);
                //-------------------------------------------------------------------------------------------------
                //Criado para permitir o teste do TipoComponenteCurricularXInstituicaoNivelEnsino
                driver.FindElement(By.Id("BotaoNovoVP")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Teste");
                driver.FindElement(By.Name("Sigla")).Clear();
                driver.FindElement(By.Name("Sigla")).SendKeys("TST");
                driver.FindElement(By.Name("TiposDivisao[0].Descricao")).Click();
                driver.FindElement(By.Name("TiposDivisao[0].Descricao")).Clear();
                driver.FindElement(By.Name("TiposDivisao[0].Descricao")).SendKeys("TESTE");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("select_TiposDivisao_0__TipoGestaoDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposDivisao_0__TipoGestaoDivisaoComponente"))).SelectByText("Trabalho");
                driver.FindElement(By.Id("select_TiposDivisao_0__TipoGestaoDivisaoComponente")).Click();                                               
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();
            });
        }


        [Fact]
        public void TipoComponenteCurricular_2_Alterar()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //-----------------------------------------------------------------------------------------
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("teste");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("DetailListBotaoAlterar0")).Click();
                driver.FindElement(By.Id("divConteudoPrincipalEstrutura")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Novo Tipo de Componente curricular Automação");
                driver.FindElement(By.Name("Sigla")).Click();
                driver.FindElement(By.Name("Sigla")).Clear();
                driver.FindElement(By.Name("Sigla")).SendKeys("NTCC");
                driver.FindElement(By.Name("TiposDivisao[0].Descricao")).Click();
                //driver.FindElement(By.XPath("//div[@id='TiposDivisao']/div[3]/div/div/div")).Click();
                driver.FindElement(By.Name("TiposDivisao[0].Descricao")).Clear();
                driver.FindElement(By.Name("TiposDivisao[0].Descricao")).SendKeys("Tipo divisão componente");
                driver.FindElement(By.Id("select_TiposDivisao_0__TipoGestaoDivisaoComponente")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposDivisao_0__TipoGestaoDivisaoComponente"))).SelectByText("Exame");
                driver.FindElement(By.Id("select_TiposDivisao_0__TipoGestaoDivisaoComponente")).Click();
                driver.FindElement(By.Id("BotaoSalvarTemplate")).Click();
                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();
            });
        }

        [Fact]
        public void TipoComponenteCurricular_3_Excluir()
        {
            base.ExecuteTest((driver) =>
            {
                Login(driver);
                //----------------------------------------------
                //Criado para excluir o registro criado para o teste do script TipoComponenteCurricularXInstituicaoNivelEnsino
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Novo Tipo de Componente curricular Automação");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("DetailListBotaoExcluir0")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                //Checando a mensagem de Sucesso
                Assert.True(CheckSuccess(), "Era esperado sucesso e ocorreu um erro");
                driver.Driver.Close();
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoComponenteCurricular_1_Inserir();
            TipoComponenteCurricular_2_Alterar();
            TipoComponenteCurricular_3_Excluir();
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
            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "/CUR/TipoComponenteCurricular");
            //----------------------------------------------------
        }


    }
}
