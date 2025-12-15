using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test;
using SMC.Framework.Test.Selenium;
using System.Threading;

namespace SMC.Academico.Tests
{
    [TestClass]
    public class TipoFormacaoEspecifica : SMCSeleniumUnitTest
    {
        /// <summary>
        /// URL de onde será efetuado o teste
        /// </summary>
        private const string SGA = "http://localhost/Dev/SGA.Administrativo/";
        // private bool phantomJS = false;

        [TestMethod]
        public void InserirSucesso()
        {
            base.ExecuteTest((browser) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();

                browser.Navigate().GoToUrl(SGA);

                //Actions
                InserirSucessoSteps(browser);
            });
        }

        [TestMethod]
        public void InserirErrorToken()
        {
            base.ExecuteTest((browser) =>
            {
                //Maximiza o Browser
                // driverNavigator.Manage().Window.Maximize();
                browser.Navigate().GoToUrl(SGA);

                //Actions
                InserirErroTokenSteps(browser);
            });
        }


        /// <summary>
        /// Executa as ações do teste
        /// </summary>
        /// <param name="browser"></param>
        private void InserirSucessoSteps(IWebDriver browser)
        {
            //Login e seleção da paróquia
            browser.SMCLogarCpf();

            // driver.Navigate().GoToUrl("http://localhost/Dev/SGA.Administrativo/CSO/TipoFormacaoEspecifica");
            browser.GoToUrl2("http://localhost/Dev/SGA.Administrativo/CSO/TipoFormacaoEspecifica");

            browser.FindElement(By.Id("BotaoNovoVP")).Click();
            Thread.Sleep(300);
            browser.FindElement(By.Name("Descricao")).Click();
            Thread.Sleep(300);
            browser.FindElement(By.Name("Descricao")).Clear();
            Thread.Sleep(300);
            browser.FindElement(By.Name("Descricao")).SendKeys("Teste10");
            browser.FindElement(By.Name("Token")).Clear();
            browser.FindElement(By.Name("Token")).SendKeys("TESTE");
            browser.FindElement(By.Name("TipoCurso")).Click();
            new SelectElement(browser.FindElement(By.Name("TipoCurso"))).SelectByText("ABI");
            browser.FindElement(By.Name("TipoCurso")).Click();
            browser.FindElement(By.Name("ClasseTipoFormacao")).Click();
            new SelectElement(browser.FindElement(By.Name("ClasseTipoFormacao"))).SelectByText("Curso");
            browser.FindElement(By.Name("ClasseTipoFormacao")).Click();
            browser.FindElement(By.Name("PermiteEmitirDocumentoConclusao")).Click();
            browser.FindElement(By.Id("btnSalvarVPIN")).Click();

            var r = CheckSuccess();
            Assert.IsTrue(r);

            //CheckError();
            //CheckMessage("problema financeiro");
        }

        private void InserirErroTokenSteps(IWebDriver browser)
        {
            //Login e seleção da paróquia
            browser.SMCLogarCpf();

            // driver.Navigate().GoToUrl("http://localhost/Dev/SGA.Administrativo/CSO/TipoFormacaoEspecifica");
            browser.GoToUrl2("http://localhost/Dev/SGA.Administrativo/CSO/TipoFormacaoEspecifica");

            browser.FindElement(By.Id("BotaoNovoVP")).Click();
            Thread.Sleep(300);
            browser.FindElement(By.Name("Descricao")).Click();
            Thread.Sleep(300);
            browser.FindElement(By.Name("Descricao")).Clear();
            Thread.Sleep(300);
            browser.FindElement(By.Name("Descricao")).SendKeys("Teste10");
            browser.FindElement(By.Name("Token")).Clear();
            browser.FindElement(By.Name("Token")).SendKeys("TESTE");
            browser.FindElement(By.Name("TipoCurso")).Click();
            new SelectElement(browser.FindElement(By.Name("TipoCurso"))).SelectByText("ABI");
            browser.FindElement(By.Name("TipoCurso")).Click();
            browser.FindElement(By.Name("ClasseTipoFormacao")).Click();
            new SelectElement(browser.FindElement(By.Name("ClasseTipoFormacao"))).SelectByText("Curso");
            browser.FindElement(By.Name("ClasseTipoFormacao")).Click();
            browser.FindElement(By.Name("PermiteEmitirDocumentoConclusao")).Click();
            browser.FindElement(By.Id("btnSalvarVPIN")).Click();

            Thread.Sleep(1000);
            var r = CheckMessage("com este token");

            Assert.IsTrue(r);

        }
    }
}

//Selecionando entidade
/*
driver.Click_FindElementsBySelector("a.smc-seta-selector", phantomJS);
driver.Click_FindElementsBySelector("li[data-value='1']", phantomJS);

driver.Click_FindElementsBySelector("a.smc-seta-selector", 1, phantomJS);
driver.Click_FindElementsBySelector($"li[data-value='{codParoquiaSelecionada}']", phantomJS);

driver.Click_FindElementsBySelector("input.smc-btn-custom", phantomJS);
 */

//Navegação
// driver.GoToUrl2("http://localhost/dev/SGA.Administrativo/CSO/GrauAcademico");

//Click no botão Novo
// driver.Click_FindElementsBySelector("[data-behavior='smc-behavior-novo']", phantomJS);

//#region Novo registro

////Preenchimento dos campos

//string random = new Random().Next(0, 99999).ToString();
//string guid = Guid.NewGuid().ToString();
//string nomePopular = "Nome Popular " + guid;

//driver.SendKeys_FindBySelector("[name='NomePopular']", nomePopular, phantomJS);
//driver.SendKeys_FindBySelector("[name='NomeFamilia']", "Nome Familia " + random, phantomJS);
//driver.SendKeys_FindBySelector("[name='Sigla']", "TST " + random, phantomJS);
//driver.SendKeys_FindBySelector("[name='NomeResponsavel']", "Nome Responsavel " + random, phantomJS);

//new SelectElement(driver.FindByName("TipoGenero", phantomJS)).SelectByText("Feminina");
//new SelectElement(driver.FindByName("TipoFamilia", phantomJS)).SelectByText("Instituto");

////Click no botão Salvar - Se o Save der certo você será redirecionado para a tela de edição com  Seq preenchido
//driver.Click_FindElementsBySelector("[data-behavior='smc-behavior-salvar']", phantomJS);

//#endregion

//#region Tela de alteração - volta do post do novo registro

////Seq do novo registro cadastrado
////string SeqNovoRegistro = driver.FindByName("[name='Seq']",phantomJS).GetAttribute("value");

////Click do botão voltar
//driver.Click_FindElementsBySelector("[data-behavior='smc-behavior-voltar']", phantomJS);

//#endregion

//#region Pesquisando o registro cadastrado

////Preenchendo o filtro
//driver.SendKeys_FindBySelector("input.smc-horizontal-align-left[name='NomePopular']", nomePopular, phantomJS);

////Click no botão pesquisar
//driver.Click_FindElementsBySelector("[data-behavior='smc-behavior-pesquisar']", phantomJS);

//Thread.Sleep(1000);

////Click no botão alterar
//driver.Click_FindElementsBySelector("[data-behavior='smc-behavior-alterar']", phantomJS);

//#endregion

//#region Alterando o registro e voltando para a tela de pesquisa

//driver.SendKeys_FindBySelector("[name='NomeFamilia']", "Nome Familia Alt " + random, phantomJS);
//driver.SendKeys_FindBySelector("[name='Sigla']", "ALT " + random, phantomJS);
//driver.SendKeys_FindBySelector("[name='NomeResponsavel']", "Nome Responsavel Alt" + random, phantomJS);

//new SelectElement(driver.FindByName("TipoGenero", phantomJS)).SelectByText("Masculina");
//new SelectElement(driver.FindByName("TipoFamilia", phantomJS)).SelectByText("Ordem");

////Click do botão salvar
//driver.Click_FindElementsBySelector("[data-behavior='smc-behavior-salvar']", phantomJS);

////Click do botão voltar
//driver.Click_FindElementsBySelector("[data-behavior='smc-behavior-voltar']", phantomJS);
//#endregion

//#region Pesquisando o registro cadastrado

////Click no botão excluir
//driver.Click_FindElementsBySelector("[data-behavior='smc-behavior-excluir']", phantomJS);

////Click no botão alterar
//driver.Click_FindElementsBySelector("[data-behavior='smc-confirm-yes']", phantomJS);

//#endregion