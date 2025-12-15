using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using SMC.Framework.Test;
using System;
using System.Threading;
using Xunit;

namespace SMC.Academico.Tests.ADM.CSO //Arvore onde esta o arquivo
{
    public class TipoFormacaoEspecifica : SMCSeleniumUnitTest //Declara o nome da classe e recebe a SMCSeleniumUnitTest
    {
        [Fact] //Deve ser declarada para teste unitario
        public void TipoFormacaoEspecifica_Inclusao() //Colocar o nome da tela a ser testada
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
                driver.FindElement(By.Name("Descricao")).SendKeys("Habilitação/Especialização");
                driver.FindElement(By.Name("Token")).Click();
                driver.FindElement(By.Name("Token")).Clear();
                driver.FindElement(By.Name("Token")).SendKeys("HABESP");
                driver.FindElement(By.Id("select_ClasseTipoFormacao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_ClasseTipoFormacao"))).SelectByText("Curso");
                driver.FindElement(By.Name("ExigeGrau")).Click();
                driver.FindElement(By.Name("ExibeGrauDescricaoFormacao")).Click();
                driver.FindElement(By.Name("PermiteEmitirDocumentoConclusao")).Click();
                driver.FindElement(By.Name("GeraCarimbo")).Click();
                driver.FindElement(By.Id("select_TiposCurso_0__TipoCurso")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_TiposCurso_0__TipoCurso"))).SelectByText("Normal");
                driver.FindElement(By.Id("btnSalvarVPIN")).Click();

                //Checando a mensagem de Sucesso 
                Assert.True(CheckMessage("Tipo de formação específica incluído com sucesso."), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

                //Actions
                //InserirSucessoSteps(driver);

                //Assert.True(CheckMessage("Tipo de Formação Específica incluída com Sucesso"), "Mensagem esperada não exibida");

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoFormacaoEspecifica_Alteracao() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Habilitacao/ESpecialização");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.Id("DynamicBotaoEdit0")).Click();
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Habilitação/Especialização/Aprofundamento");
                driver.FindElement(By.XPath("//button[@id='btnSalvarVPIE']/i")).Click();
                //Checando a mensagem de Sucesso 
                Assert.True(CheckMessage("Tipo de formação específica alterado com sucesso."), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }


        [Fact] //Deve ser declarada para teste unitario
        public void TipoFormacaoEspecifica_Exclusaonaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Curso");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                Assert.True(CheckMessage("Exclusão não permitida. Tipo de formação específica já foi associado a uma instituição de ensino x nível de ensino."), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();

            });
        }

        [Fact] //Deve ser declarada para teste unitario
        public void TipoFormacaoEspecifica_Exclusaopermitida() //Colocar o nome da tela a ser testada
        {
            base.ExecuteTest((driver) => //chama o browser e coloca o link correto
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                Login(driver); // realiza o login como administrador

                //Colar o script aqui
                driver.FindElement(By.Name("Descricao")).Click();
                driver.FindElement(By.Name("Descricao")).Clear();
                driver.FindElement(By.Name("Descricao")).SendKeys("Habilitação/Especialização/Aprofundamento");
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                driver.FindElement(By.XPath("//a[@id='DynamicBotaoExcluir1']/i")).Click();
                driver.FindElement(By.Id("BotaoPadraoPerguntaSim")).Click();
                //Checando a mensagem de Sucesso
                Assert.True(CheckMessage("Tipo de formação específica excluído com sucesso."), "Era esperado sucesso e ocorreu um erro");

                //Para fechar o Chrome em segundo plano
                driver.Driver.Close();
            });
        }

        [Fact]
        [Trait("Ordenado", "CRUD")]
        public void TesteOrdenadoCRUD()
        {
            TipoFormacaoEspecifica_Inclusao();
            TipoFormacaoEspecifica_Alteracao();
            TipoFormacaoEspecifica_Exclusaopermitida();
            TipoFormacaoEspecifica_Exclusaonaopermitida();
        }

        private static void Login(ISMCWebDriver driver)
        {
            //----------------------------------------------------
            driver.GoToUrl(Consts.SERVIDOR_QUALIDADE_ADM); //recebe o link para acesso

            //Login
            driver.SMCLoginCpf(); //insere login e senha

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "CSO/TipoFormacaoEspecifica"); //coloca o resto do endereço para acessar a pagina do teste
        }
    }
}