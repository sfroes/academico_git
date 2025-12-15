using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SMC.Framework.Test;
using System;
using System.Threading;
using Xunit;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace SMC.Academico.Tests.ADM.DCT
{
    public class Colaborador : SMCSeleniumUnitTest
    {
        private string _dadosVinculo = "Programa de Pós-graduação em Ciências da Religião";

        [Fact]
        [Trait("Cenário", "Administrativo - Pesquisar Colaborador")]

        public string Pesquisar()
        {
            string nomeProfessor = string.Empty;

            base.ExecuteTest((driver) =>
            {

                Login(driver);

                driver.FindElement(By.Id("select_SeqEntidadeVinculo")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SeqEntidadeVinculo"))).SelectByText(_dadosVinculo);
                driver.FindElement(By.Id("select_SeqEntidadeVinculo")).Click();
                driver.FindElement(By.Id("SeqInstituicaoExterna_botao_modal")).Click();
                driver.FindElement(By.Name("Nome")).Click();
                driver.FindElement(By.Name("Nome")).Clear();
                driver.FindElement(By.Name("Nome")).SendKeys("PONTIFÍCIA UNIVERSIDADE CATÓLICA DE MINAS GERAIS");
                driver.FindElement(By.Id("DataSelectorPesquisar0")).Click();
                driver.FindElement(By.Id("DataSelector_gridDataSelector0")).Click();
                driver.FindElement(By.Id("smc-dataselector-SeqInstituicaoExterna")).Click();
                driver.FindElement(By.Id("select_SituacaoColaboradorNaInstituicao")).Click();
                new SMCSelectElement(driver.FindElement(By.Id("select_SituacaoColaboradorNaInstituicao"))).SelectByText("Ativo");
                Thread.Sleep(1500);
                driver.FindElement(By.Id("select_SituacaoColaboradorNaInstituicao")).Click();
                driver.FindElement(By.Id("BotaoPesquisarVP")).Click();
                Thread.Sleep(1500);
                nomeProfessor = driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Nome'])[2]/following::p[1]")).GetValue();

                Thread.Sleep(1500);
                driver.Driver.Close();

            });
            //
            return nomeProfessor;

        }

        [Fact]
        [Trait("Cenário", "Administrativo - Pesquisar Colaborador com Pesquisa Aluno")]



        public void AlunoPesquisarPesquisar()
        {
            //Maximiza o Browser
            // driverNavigator.Manage().Window.Maximize();

            //Pesquisa Aluno
            var admSolicitacao = new SMC.Academico.Tests.ADM.ALN.Aluno();
            (_, _, _, _, _dadosVinculo, _, _) = admSolicitacao.Pesquisar();

            base.ExecuteTest((driver) =>
            {
                // Login(driver);

                driver.Driver.Close();

            });
        }
       

        //-------------------------------------------------------------------------------------------------
        //Pesquisando o Colaborador passando os dados da pesquisa do aluno.
        /* ********************************************************************************************************
   * Comentei o script abaixo pois estava buscando o primeiro aluno da pesquisa, que no caso, 
      cursava mestrado em ciencia da religiao, porem na base de dados há apenas programa de Pós-Graduação, fazendo
      com que o teste fique errado.*/
        // Pesquisar();


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

            driver.GoToUrl2(Consts.SERVIDOR_QUALIDADE_ADM + "DCT/Colaborador");
            //-------------------------------------------------------------------------------------------------
        }
    }
}
